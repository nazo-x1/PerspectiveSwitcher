using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using Zorro.ControllerSupport;
using Zorro.Core;
using Zorro.Settings;

namespace PerspectiveSwitcher;

internal enum PerspectiveMode
{
    FirstPerson = 0,
    SecondPerson = 1,
    ThirdPerson = 2
}

internal interface IPerspectiveAdapter
{
    PerspectiveMode Mode { get; }

    /// <summary>
    /// 统一 LateUpdate 入口，返回后会继续执行原始 LateUpdate。
    /// </summary>
    void OnLateUpdate(MainCameraMovement self, float scrollDelta);

    /// <summary>
    /// 统一角色视角渲染入口。
    /// 返回 true 表示已完全处理（不再调用原始 CharacterCam）。
    /// </summary>
    bool OnCharacterCam(On.MainCameraMovement.orig_CharacterCam orig, MainCameraMovement self);

    /// <summary>
    /// 统一攀爬判定前后的相机处理。
    /// 返回 true 表示适配器内部已经调用 orig。
    /// </summary>
    bool OnTryToStartWallClimb(
        On.CharacterClimbing.orig_TryToStartWallClimb orig,
        CharacterClimbing self,
        bool forceAttempt,
        Vector3 overide,
        bool botGrab,
        float raycastDistance);
}

/// <summary>
/// 一人称：完全透传游戏原逻辑。
/// </summary>
internal sealed class FirstPersonAdapter : IPerspectiveAdapter
{
    public PerspectiveMode Mode => PerspectiveMode.FirstPerson;

    public void OnLateUpdate(MainCameraMovement self, float scrollDelta)
    {
        // 一人称暂不做特殊 LateUpdate 处理。
    }

    public bool OnCharacterCam(On.MainCameraMovement.orig_CharacterCam orig, MainCameraMovement self)
    {
        // 不拦截，相当于原版视角。
        return false;
    }

    public bool OnTryToStartWallClimb(
        On.CharacterClimbing.orig_TryToStartWallClimb orig,
        CharacterClimbing self,
        bool forceAttempt,
        Vector3 overide,
        bool botGrab,
        float raycastDistance)
    {
        // 不拦截，让统一入口调用原始逻辑。
        return false;
    }
}

/// <summary>
/// 二、三人称公用的“围绕角色旋转”的相机适配器。
/// 通过方向与注视目标的差异来区分二人称（前方看向角色）与三人称（后方看向前方）。
/// </summary>
internal sealed class OrbitPerspectiveAdapter : IPerspectiveAdapter
{
    private readonly Plugin _plugin;
    private readonly PerspectiveMode _mode;
    private readonly int _directionSign; // +1: 角色前方（二人称），-1: 角色后方（三人称）

    // 仅二、三人称使用的视角配置
    private readonly float _height = 1.5f;
    private readonly float _defaultDistance = 3f;
    private float _currentDistance;
    private readonly float _minDistance = 2f;
    private readonly float _maxDistance = 4f;
    private readonly float _zoomSpeed = 1f;
    private readonly float _lerpRate = 5f;
    private readonly float _turnSpeed = 720f;
    private readonly float _clipRadius = 0.06f;
    private readonly float _clipBuffer = 0.03f;
    private readonly LayerMask _clipMask = LayerMask.GetMask("Terrain", "Map");

    public OrbitPerspectiveAdapter(Plugin plugin, PerspectiveMode mode, int directionSign)
    {
        _plugin = plugin;
        _mode = mode;
        _directionSign = directionSign;
        _currentDistance = _defaultDistance;
    }

    public PerspectiveMode Mode => _mode;

    public void OnLateUpdate(MainCameraMovement self, float scrollDelta)
    {
        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            _currentDistance = Mathf.Clamp(
                _currentDistance - scrollDelta * _zoomSpeed,
                _minDistance,
                _maxDistance);
        }
    }

    public bool OnCharacterCam(On.MainCameraMovement.orig_CharacterCam orig, MainCameraMovement self)
    {
        if (Character.localCharacter == null)
        {
            return false;
        }

        _plugin.EnsureSettings();

        var camComp = self.GetComponent<MainCamera>();
        camComp.cam.fieldOfView = self.GetFov();

        Transform torso = Character.localCharacter.GetBodypart(BodypartType.Torso).transform;
        Vector3 lookDir = Character.localCharacter.data.lookDirection;
        if (lookDir == Vector3.zero)
        {
            lookDir = torso.forward;
        }

        // 统一位置计算：角色上方 + 朝向 * 距离 * 方向符号
        Vector3 desiredPosition = torso.position
                                  + Vector3.up * _height
                                  + lookDir.normalized * _currentDistance * _directionSign;

        // 统一遮挡判定
        Vector3 dir = (desiredPosition - torso.position).normalized;
        float maxDist = Vector3.Distance(desiredPosition, torso.position);
        if (Physics.SphereCast(torso.position, _clipRadius, dir, out RaycastHit hit, maxDist, _clipMask))
        {
            desiredPosition = hit.point - dir * _clipBuffer;
        }

        self.transform.position = Vector3.Lerp(
            self.transform.position,
            desiredPosition,
            Time.deltaTime * _lerpRate);

        // 注视点：三人称看向角色中心，二人称从前方看向角色
        Vector3 lookAtPoint = _mode == PerspectiveMode.SecondPerson
            ? torso.position + Vector3.up * 0.5f
            : torso.position;

        Quaternion desiredRotation = Quaternion.LookRotation(lookAtPoint - self.transform.position, Vector3.up);

        float sens = _plugin.GetSensitivityMultiplier();
        self.transform.rotation = Quaternion.RotateTowards(
            self.transform.rotation,
            desiredRotation,
            _turnSpeed * sens * Time.deltaTime);

        // 我们已经完全处理好相机，不再调用原逻辑。
        return true;
    }

    public bool OnTryToStartWallClimb(
        On.CharacterClimbing.orig_TryToStartWallClimb orig,
        CharacterClimbing self,
        bool forceAttempt,
        Vector3 overide,
        bool botGrab,
        float raycastDistance)
    {
        Transform torso = self.character.GetBodypart(BodypartType.Torso).transform;
        Vector3 lookDir = self.character.data.lookDirection;
        if (lookDir == Vector3.zero)
        {
            lookDir = torso.forward;
        }

        // 三人称原实现：把相机暂时放在躯干；二人称则暂时放在身后一点，防止判定异常。
        Vector3 tempPos = _mode == PerspectiveMode.SecondPerson
            ? torso.position - lookDir.normalized * 1f
            : torso.position;

        _plugin.WithTemporaryCameraPosition(tempPos, () =>
        {
            orig(self, forceAttempt, overide, botGrab, raycastDistance);
        });

        // 适配器内部已经调用过 orig。
        return true;
    }
}

// 视角切换插件：统一前处理 / 入口 / 适配器分发 / 后处理。
[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private ConfigEntry<KeyCode> configSwitchPerspective = null!;

    private bool _settingsInitialized;
    private MouseSensitivitySetting _mouseSensSetting = null!;
    private ControllerSensitivitySetting _controllerSensSetting = null!;

    private PerspectiveMode _currentMode = PerspectiveMode.FirstPerson;
    private readonly Dictionary<PerspectiveMode, IPerspectiveAdapter> _adapters = new();

    private void Awake()
    {
        // 统一日志
        Log = Logger;

        // 统一配置：一个按键在一/二/三人称之间循环
        configSwitchPerspective = Config.Bind(
            "Camera.Toggles",
            "SwitchPerspective",
            KeyCode.V,
            "Switch the camera perspective (First -> Second -> Third)");

        // 初始化适配器（策略 / 适配器模式）
        _adapters[PerspectiveMode.FirstPerson] = new FirstPersonAdapter();
        _adapters[PerspectiveMode.SecondPerson] = new OrbitPerspectiveAdapter(this, PerspectiveMode.SecondPerson, +1);
        _adapters[PerspectiveMode.ThirdPerson] = new OrbitPerspectiveAdapter(this, PerspectiveMode.ThirdPerson, -1);

        // 统一入口：在比较早期挂接
        On.MainCameraMovement.LateUpdate += MainCameraMovement_LateUpdate;
        On.MainCameraMovement.CharacterCam += MainCameraMovement_CharacterCam;
        On.CharacterClimbing.TryToStartWallClimb += CharacterClimbing_TryStartWallClimb;

        Log.LogInfo($"Plugin {Name} is loaded! Current mode: {_currentMode}");
    }

    private IPerspectiveAdapter CurrentAdapter => _adapters[_currentMode];

    #region 统一前处理 / 后处理

    internal void EnsureSettings()
    {
        if (_settingsInitialized)
        {
            return;
        }

        if (GameHandler.Instance != null && GameHandler.Instance.SettingsHandler != null)
        {
            _mouseSensSetting = GameHandler.Instance.SettingsHandler.GetSetting<MouseSensitivitySetting>();
            _controllerSensSetting = GameHandler.Instance.SettingsHandler.GetSetting<ControllerSensitivitySetting>();
            _settingsInitialized = true;
        }
    }

    internal float GetSensitivityMultiplier()
    {
        if (!_settingsInitialized)
        {
            return 1f;
        }

        return InputHandler.GetCurrentUsedInputScheme() == InputScheme.Gamepad
            ? _controllerSensSetting.Value
            : _mouseSensSetting.Value;
    }

    internal void WithTemporaryCameraPosition(Vector3 tempPosition, Action action)
    {
        var cam = MainCamera.instance.transform;
        Vector3 oldPos = cam.position;
        try
        {
            cam.position = tempPosition;
            action();
        }
        finally
        {
            cam.position = oldPos;
        }
    }

    private void SwitchToNextMode()
    {
        _currentMode = _currentMode switch
        {
            PerspectiveMode.FirstPerson => PerspectiveMode.SecondPerson,
            PerspectiveMode.SecondPerson => PerspectiveMode.ThirdPerson,
            _ => PerspectiveMode.FirstPerson
        };

        Log.LogInfo($"Switched perspective to: {_currentMode}");
    }

    #endregion

    #region Hooks

    private void MainCameraMovement_LateUpdate(On.MainCameraMovement.orig_LateUpdate orig, MainCameraMovement self)
    {
        // 统一前处理：按键切换、灵敏度初始化等
        if (Input.GetKeyDown(configSwitchPerspective.Value))
        {
            SwitchToNextMode();
        }

        EnsureSettings();

        float scroll = Input.mouseScrollDelta.y;
        CurrentAdapter.OnLateUpdate(self, scroll);

        // 统一后处理：始终调用原始 LateUpdate
        orig(self);
    }

    private void MainCameraMovement_CharacterCam(On.MainCameraMovement.orig_CharacterCam orig, MainCameraMovement self)
    {
        // 统一入口：根据模式分发到不同适配器
        bool handled = CurrentAdapter.OnCharacterCam(orig, self);

        // 如果当前模式没有完全接管，则回落到原始视角（一般是一人称）
        if (!handled)
        {
            orig(self);
        }
    }

    private void CharacterClimbing_TryStartWallClimb(
        On.CharacterClimbing.orig_TryToStartWallClimb orig,
        CharacterClimbing self,
        bool forceAttempt,
        Vector3 overide,
        bool botGrab,
        float raycastDistance)
    {
        // 统一入口：交给当前视角适配器决定是否改写相机
        bool handled = CurrentAdapter.OnTryToStartWallClimb(
            orig,
            self,
            forceAttempt,
            overide,
            botGrab,
            raycastDistance);

        if (!handled)
        {
            // 默认行为：保留原版攀爬逻辑
            orig(self, forceAttempt, overide, botGrab, raycastDistance);
        }
    }

    #endregion
}
