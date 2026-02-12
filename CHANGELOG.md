# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.2] - 2026-02-12

### Changed
- 📝 Updated README with new installation recommendations
- ⭐ Prioritized GALE Mod Manager as recommended installation method
- 🔧 Added r2modman as secondary installation option
- 📦 Added AutoHookGenPatcher as a dependency

---

## [0.1.1] - 2025-12-26

### Changed
- 🎨 Updated project logo/icon

---

## [0.1.0] - 2025-12-26

### Added
- ✨ Initial release of PerspectiveSwitcher
- 🎮 Support for three camera perspectives: First Person, Second Person, and Third Person
- 🔄 Cycle through perspectives with a single key press (default: V)
- 🖱️ Mouse wheel zoom support for Second and Third person modes
- 🛡️ Collision detection to prevent camera clipping through walls
- ⚙️ Configurable toggle key via BepInEx configuration
- 🎯 Proper handling of wall climbing mechanics in all perspectives
- 📊 Respects game's mouse and controller sensitivity settings
- 🏗️ Refactored code architecture using adapter pattern for better maintainability

### Enhanced
- 🚀 Based on [EvaisaDev/peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson) with significant improvements:
  - Added Second Person perspective support
  - Improved code organization and structure
  - Better separation of concerns with adapter pattern
  - Enhanced camera interpolation and smoothing

### Technical
- 📦 Built on BepInEx framework
- 🔌 Compatible with PEAK game
- 🎨 Clean, modular codebase for easy extension
- 🤖 AI-assisted code implementation with architecture design provided by author

---

## 中文

### [0.1.2] - 2026-02-12

#### 变更
- 📝 更新了安装推荐说明
- ⭐ 优先推荐 GALE Mod Manager
- 🔧 添加 r2modman 作为次选安装方式
- 📦 添加 AutoHookGenPatcher 依赖项

---

### [0.1.1] - 2025-12-26

#### 变更
- 🎨 更新项目 logo/图标

---

### [0.1.0] - 2025-12-26

#### 新增
- ✨ PerspectiveSwitcher 首次发布
- 🎮 支持三种相机视角：第一人称、第二人称和第三人称
- 🔄 单键循环切换视角（默认：V）
- 🖱️ 第二人称和第三人称模式下支持鼠标滚轮缩放
- 🛡️ 碰撞检测防止相机穿墙
- ⚙️ 通过 BepInEx 配置可自定义切换按键
- 🎯 所有视角下正确处理攀爬机制
- 📊 尊重游戏的鼠标和手柄灵敏度设置
- 🏗️ 使用适配器模式重构代码架构，提高可维护性

#### 增强
- 🚀 基于 [EvaisaDev/peak-thirdperson](https://github.com/EvaisaDev/peak-thirdperson) 的重大改进：
  - 新增第二人称视角支持
  - 改进代码组织和结构
  - 使用适配器模式实现更好的关注点分离
  - 增强相机插值和平滑效果

#### 技术
- 📦 基于 BepInEx 框架构建
- 🔌 兼容 PEAK 游戏
- 🎨 清晰、模块化的代码库，易于扩展
- 🤖 AI 辅助代码实现，架构设计由作者提供
