# PerspectiveSwitcher 开发指南

本文档为 PerspectiveSwitcher 项目的开发指南，包含开发环境设置、构建配置和打包流程。

## 📋 开发环境要求

- .NET SDK（项目所需版本请查看 `.csproj` 文件）
- Visual Studio 或 Visual Studio Code（推荐）
- PEAK 游戏安装路径
- BepInEx 已安装在游戏目录中

## 🛠️ 初始设置

### 1. 配置构建路径

1. 复制 `Config.Build.user.props.template` 文件并重命名为 `Config.Build.user.props`
   - 这将自动将插件程序集复制到 `BepInEx/plugins/` 目录
   - 配置路径指向你的游戏路径和 `BepInEx/plugins/` 目录
   - 如果游戏路径有效，游戏程序集引用应该可以正常工作

2. 编辑 `Config.Build.user.props` 文件，设置以下路径：
   ```xml
   <GamePath>你的游戏安装路径</GamePath>
   <BepInExPluginsPath>你的 BepInEx/plugins 路径</BepInExPluginsPath>
   ```

### 2. 检查待办事项

在项目中搜索 `TODO` 标记，查看需要配置或修改的内容：
```sh
# 使用 grep 或 IDE 的搜索功能
grep -r "TODO" .
```

## 🔨 构建项目

### 开发构建

```sh
dotnet build
```

### 发布构建

```sh
dotnet build -c Release
```

构建完成后，插件会自动复制到配置的 `BepInEx/plugins/` 目录（如果已配置 `Config.Build.user.props`）。

### 构建选项说明

```sh
# 查看所有构建选项
dotnet build --help

# 常用参数说明：
# -c, --configuration    构建配置（Debug/Release）
# -v, --verbosity        详细程度（q=quiet, m=minimal, n=normal, d=detailed, diag=diagnostic）
```

## 📦 Thunderstore 打包

项目内置了 Thunderstore 打包支持，使用 [TCLI](https://github.com/thunderstore-io/thunderstore-cli)。

### 构建 Thunderstore 包

```sh
dotnet build -c Release -target:PackTS -v d
```

> [!NOTE]  
> 你可以使用 `dotnet build --help` 了解不同的构建选项。  
> `-c` 是 `--configuration` 的简写，`-v d` 是 `--verbosity detailed` 的简写。

构建完成后，打包文件将位于 `artifacts/thunderstore/` 目录。

### 打包配置

Thunderstore 包的配置信息在 `thunderstore.toml` 文件中。打包前请确保：
- 版本号已更新
- 依赖项配置正确
- 描述和元数据准确

## 🧪 测试

1. 构建项目后，插件会自动复制到游戏目录
2. 启动游戏并测试功能
3. 查看 BepInEx 日志文件以调试问题

## 📝 开发注意事项

- 代码架构使用适配器模式，保持代码组织清晰
- 修改代码后需要重新构建并复制到游戏目录
- 建议在开发时使用 Debug 配置以便调试
- 发布前使用 Release 配置进行最终构建

## 🔗 相关资源

- [BepInEx 文档](https://docs.bepinex.dev/)
- [Thunderstore CLI 文档](https://github.com/thunderstore-io/thunderstore-cli)
- [.NET 文档](https://docs.microsoft.com/dotnet/)
- [modding guide](https://peakmodding.github.io/)
