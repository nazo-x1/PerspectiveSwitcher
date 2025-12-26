# PerspectiveSwitcher / 视角切换器

<div align="center">

![Version](https://img.shields.io/badge/version-0.1.1-blue.svg)
![License](https://img.shields.io/badge/license-GPLv3-green.svg)
![PEAK](https://img.shields.io/badge/game-PEAK-orange.svg)

**A powerful camera perspective mod for PEAK that supports First, Second, and Third person views**  
**为 PEAK 游戏打造的强大视角切换模组，支持第一人称、第二人称和第三人称视角**

[English](#english) | [中文](#中文)

</div>

---

## English

### 📖 Overview

**PerspectiveSwitcher** is an enhanced camera mod for PEAK that allows you to seamlessly switch between three different camera perspectives: **First Person**, **Second Person**, and **Third Person**. This mod is based on the excellent work from [peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson) by EvaisaDev, with significant enhancements including second-person view support and improved code architecture.

> **Note**: This project was developed with AI assistance. The architecture design was provided by the author, and the code implementation was AI-assisted.

### ✨ Features

- 🔄 **Three Perspective Modes**
  - **First Person**: Classic immersive view (default game camera)
  - **Second Person**: Unique front-facing view looking at your character
  - **Third Person**: Traditional behind-the-character view

- 🎮 **Easy Controls**
  - Press `V` (configurable) to cycle through perspectives: First → Second → Third → First
  - Mouse wheel to zoom in/out in Second and Third person modes
  - Smooth camera transitions with configurable sensitivity

- 🛡️ **Other Features**
  - Collision detection prevents camera clipping through walls
  - Proper handling during wall climbing
  - Respects game's mouse and controller sensitivity settings
  - Smooth interpolation for camera movement

### 🎯 Perspective Comparison

| Perspective | Camera Position | View Direction | Use Case |
|------------|----------------|----------------|----------|
| **First Person** | Inside character | Forward | Immersive gameplay |
| **Second Person** | In front of character | Looking at character | Unique cinematic shots |
| **Third Person** | Behind character | Forward (through character) | Better spatial awareness |

### 📸 Screenshots

> **TODO**: Add screenshots here showing each perspective mode
> 
> ![First Person View](screenshots/first-person.png)
> *First Person - Classic immersive view*
> 
> ![Second Person View](screenshots/second-person.png)
> *Second Person - Unique front-facing perspective*
> 
> ![Third Person View](screenshots/third-person.png)
> *Third Person - Traditional behind-character view*

### 🚀 Installation

1. **Prerequisites**
   - [BepInEx Pack for PEAK](https://thunderstore.io/c/peak/p/BepInEx-BepInExPack_PEAK/) (v5.4.2403 or later)

2. **Install via Thunderstore Mod Manager** (Recommended)
   - Open Thunderstore Mod Manager
   - Search for "PerspectiveSwitcher"
   - Click "Install with dependencies"

3. **Manual Installation**
   - Download the latest release from [Thunderstore](https://thunderstore.io)
   - Extract the zip file
   - Copy the `plugins` folder contents to `PEAK/BepInEx/plugins/`
   - Launch the game

### ⚙️ Configuration

The mod creates a configuration file at `BepInEx/config/com.github.nazo-x1.PerspectiveSwitcher.cfg`:

```ini
[Camera.Toggles]
## Switch the camera perspective (First -> Second -> Third)
# Setting type: KeyboardShortcut
# Default value: V
SwitchPerspective = V
```

You can change the toggle key by editing this file or using BepInEx Configuration Manager.

### 🎮 Usage

1. **Switching Perspectives**
   - Press `V` (default) to cycle: First Person → Second Person → Third Person → First Person
   - The current mode is logged in the BepInEx console

2. **Adjusting Camera Distance** (Second/Third Person only)
   - Scroll mouse wheel up to zoom out
   - Scroll mouse wheel down to zoom in
   - Distance range: 2.0 to 4.0 units

3. **Tips**
   - The camera automatically adjusts when near walls to prevent clipping
   - Wall climbing works correctly in all perspectives
   - Camera sensitivity matches your game settings

### 🔧 Technical Details

- **Architecture**: Uses adapter pattern for clean code organization
  - Architecture design provided by the author
  - Code implementation assisted by AI
- **Performance**: Minimal overhead, efficient camera calculations
- **Compatibility**: Not guaranteed to work with other camera and gameplay mods
- **Code Quality**: Refactored from original third-person mod with improved structure

### 📝 Credits & Acknowledgments

- **Original Third-Person Mod**: [EvaisaDev/peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson)
  - This mod is based on the excellent work by EvaisaDev
  - Enhanced with second-person view and improved architecture

- **Development**
  - **Architecture Design**: Provided by nazo (author)
  - **Code Implementation**: AI-assisted development
  - **Original Third-Person Implementation**: EvaisaDev
  - **Second-Person View & Enhancements**: nazo

### 🐛 Known Issues

- None currently reported. Please report issues on [GitHub Issues](https://github.com/nazo-x1/PerspectiveSwitcher/issues)

### 📄 License

This project is licensed under the GNU General Public License v3.0 (GPLv3) - see the [LICENSE](LICENSE) file for details.

### 🔗 Links

- [Thunderstore Page](https://thunderstore.io/c/peak/p/nazo-PerspectiveSwitcher/)
- [GitHub Repository](https://github.com/nazo-x1/PerspectiveSwitcher)
- [Original Mod](https://github.com/EvaisaDev/peak-thirdperson)

---

## 中文

### 📖 简介

**视角切换器 (PerspectiveSwitcher)** 是一款为 PEAK 游戏打造的增强型相机模组，允许你在三种不同的相机视角之间无缝切换：**第一人称**、**第二人称**和**第三人称**。本模组基于 EvaisaDev 的优秀作品 [peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson)，并进行了重大增强，包括第二人称视角支持和改进的代码架构。

> **说明**：本项目采用 AI 辅助开发。架构设计由作者提供，代码实现由 AI 辅助生成。

### ✨ 功能特性

- 🔄 **三种视角模式**
  - **第一人称**：经典的沉浸式视角（游戏默认相机）
  - **第二人称**：独特的正面视角，从角色前方看向角色
  - **第三人称**：传统的角色后方视角

- 🎮 **简单易用的控制**
  - 按 `V` 键（可配置）循环切换视角：第一人称 → 第二人称 → 第三人称 → 第一人称
  - 在第二人称和第三人称模式下使用鼠标滚轮缩放
  - 平滑的相机过渡，支持可配置的灵敏度

- 🛡️ **其他特性**
  - 碰撞检测防止相机穿墙
  - 攀爬时的正确处理
  - 兼容游戏的鼠标和手柄灵敏度设置
  - 平滑的相机移动插值

### 🎯 视角对比

| 视角模式 | 相机位置 | 视角方向 | 使用场景 |
|---------|---------|---------|---------|
| **第一人称** | 角色内部 | 向前 | 沉浸式游戏体验 |
| **第二人称** | 角色前方 | 看向角色 | 独特的电影式镜头 |
| **第三人称** | 角色后方 | 向前（通过角色） | 更好的空间感知 |

### 📸 截图

> **TODO**：待添加显示各视角模式的截图
> 
> ![第一人称视角](screenshots/first-person.png)
> *第一人称 - 经典的沉浸式视角*
> 
> ![第二人称视角](screenshots/second-person.png)
> *第二人称 - 独特的正面视角*
> 
> ![第三人称视角](screenshots/third-person.png)
> *第三人称 - 传统的角色后方视角*

### 🚀 安装方法

1. **前置要求**
   - [PEAK 的 BepInEx 包](https://thunderstore.io/c/peak/p/BepInEx-BepInExPack_PEAK/) (v5.4.2403 或更高版本)

2. **通过 Thunderstore Mod Manager 安装**（推荐）
   - 打开 Thunderstore Mod Manager
   - 搜索 "PerspectiveSwitcher"
   - 点击 "Install with dependencies"

3. **手动安装**
   - 从 [Thunderstore](https://thunderstore.io) 下载最新版本
   - 解压 zip 文件
   - 将 `plugins` 文件夹内容复制到 `PEAK/BepInEx/plugins/`
   - 启动游戏

### ⚙️ 配置

模组会在 `BepInEx/config/com.github.nazo-x1.PerspectiveSwitcher.cfg` 创建配置文件：

```ini
[Camera.Toggles]
## Switch the camera perspective (First -> Second -> Third)
# Setting type: KeyboardShortcut
# Default value: V
SwitchPerspective = V
```

你可以通过编辑此文件或使用 BepInEx Configuration Manager 来更改切换按键。

### 🎮 使用方法

1. **切换视角**
   - 按 `V` 键（默认）循环切换：第一人称 → 第二人称 → 第三人称 → 第一人称
   - 当前模式会在 BepInEx 控制台中记录

2. **调整相机距离**（仅第二/第三人称）
   - 向上滚动鼠标滚轮以拉远
   - 向下滚动鼠标滚轮以拉近
   - 距离范围：2.0 到 4.0 单位

3. **提示**
   - 相机在靠近墙壁时会自动调整以防止穿墙
   - 所有视角下的攀爬功能都能正常工作
   - 相机灵敏度与你的游戏设置匹配

### 🔧 技术细节

- **架构**：使用适配器模式实现清晰的代码组织
  - 架构设计由作者提供
  - 代码实现由 AI 辅助生成
- **性能**：开销极小，高效的相机计算
- **兼容性**：**不保证**与其他相机和游戏玩法模组兼容
- **代码质量**：从原始第三人称模组重构，改进了结构

### 📝 致谢

- **原始第三人称模组**：[EvaisaDev/peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson)
  - 本模组基于 EvaisaDev 的优秀作品
  - 增强了第二人称视角并改进了架构

- **开发说明**
  - **架构设计**：由 nazo（作者）提供
  - **代码实现**：AI 辅助开发
  - **原始第三人称实现**：EvaisaDev
  - **第二人称视角和增强功能**：nazo

### 🐛 已知问题

- 目前暂无报告的问题。请在 [GitHub Issues](https://github.com/nazo-x1/PerspectiveSwitcher/issues) 上报告问题

### 📄 许可证

本项目采用 GNU 通用公共许可证 v3.0 (GPLv3) - 详见 [LICENSE](LICENSE) 文件

### 🔗 相关链接

- [Thunderstore 页面](https://thunderstore.io/c/peak/p/nazo-PerspectiveSwitcher/)
- [GitHub 仓库](https://github.com/nazo-x1/PerspectiveSwitcher)
- [原始模组](https://github.com/EvaisaDev/peak-thirdperson)

---

<div align="center">

**Enjoy your enhanced PEAK experience! 🎮**  
**享受增强的 PEAK 游戏体验！🎮**

Made with ❤️ for the PEAK community

</div>
