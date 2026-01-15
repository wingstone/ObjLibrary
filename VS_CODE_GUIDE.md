# VS Code C# 开发项目设置说明

## 项目结构

本项目是一个完整的Unity风格数学库C#项目，包含以下主要部分：

```
Test/
├── MathLibrary.csproj           # 核心数学库项目（类库）
├── MathLibraryTest/
│   ├── MathLibraryTest.csproj   # 测试应用项目（控制台应用）
│   └── Program.cs               # 完整的功能测试代码
├── Test.sln                     # 解决方案文件
├── src/                         # 数学库源代码
│   ├── Vector2.cs
│   ├── Vector3.cs
│   ├── Vector4.cs
│   ├── Color.cs
│   ├── Quaternion.cs
│   ├── Matrix4x4.cs
│   └── Mathf.cs
├── .vscode/                     # VS Code 配置
│   ├── launch.json              # 调试配置
│   ├── tasks.json               # 编译任务
│   └── settings.json            # 编辑器设置
└── README.md                    # 项目文档
```

## 项目说明

### MathLibrary 项目
- **类型**: 类库 (DLL)
- **用途**: 提供Unity风格的数学工具
- **内容**: Vector2、Vector3、Vector4、Color、Quaternion、Matrix4x4、Mathf

### MathLibraryTest 项目
- **类型**: 控制台应用 (EXE)
- **用途**: 测试和演示MathLibrary的所有功能
- **内容**: 完整的功能测试代码，展示如何使用库中的各个类

## 编译和运行

### 编译整个解决方案
```bash
dotnet build Test.sln
```

### 编译单个项目
```bash
# 编译MathLibrary
dotnet build MathLibrary.csproj

# 编译MathLibraryTest
dotnet build MathLibraryTest/MathLibraryTest.csproj
```

### 运行测试程序
```bash
dotnet run --project MathLibraryTest/MathLibraryTest.csproj
```

### 在VS Code中快速编译
按 `Ctrl+Shift+B` 执行build任务（需要在VS Code中打开项目）

## 在VS Code中使用

### 必需扩展
- **C#** (ms-dotnettools.csharp) - 提供C#编辑和调试支持

### 快捷键
- `Ctrl+Shift+B` - 编译项目
- `Ctrl+Shift+D` - 进入调试模式
- `F5` - 启动调试
- `Ctrl+F5` - 以非调试模式运行

### 调试
项目配置了调试支持，按F5可以启动MathLibraryTest并在VS Code调试器中运行。

## 项目特点

✅ **完整的数学库**: 包含2D/3D/4D向量、四元数、矩阵、颜色等
✅ **生产级代码**: 所有类型都实现了必要的接口和操作符重载
✅ **详细的测试**: MathLibraryTest项目演示了所有功能
✅ **VS Code优化**: 包含完整的调试和编译配置
✅ **无外部依赖**: 仅依赖.NET 8.0标准库
✅ **易于扩展**: 清晰的代码结构，便于添加新功能

## 常见操作

### 引用MathLibrary到其他项目
```bash
dotnet add reference ../MathLibrary.csproj
```

### 发布库
```bash
dotnet publish MathLibrary.csproj -c Release
```

### 创建NuGet包
```bash
dotnet pack MathLibrary.csproj -c Release
```

## 故障排除

### 编译出现"特性重复"错误
这通常是由于obj文件夹中的缓存导致的。解决方法：
```bash
dotnet clean
dotnet build
```

### 调试无法启动
确保已安装C#扩展，并且项目已成功编译。

## 关于作者

这是一个完整的VS Code C#开发示例，展示了如何在VS Code中高效地开发.NET项目。

## 许可证

MIT License
