# ObjLibrary 项目结构

## 📁 项目布局

```
ObjLibrary/
├── MathLibrary/                    # 基础数学库 (可独立使用)
│   ├── MathLibrary.csproj         # 项目文件
│   ├── Vector2.cs                 # 2D 向量类
│   ├── Vector3.cs                 # 3D 向量类
│   ├── Vector4.cs                 # 4D 向量类
│   ├── Quaternion.cs              # 四元数类
│   ├── Matrix4x4.cs               # 4x4 矩阵类
│   ├── Mathf.cs                   # 数学函数辅助类
│   ├── Color.cs                   # 颜色结构体
│   └── Random.cs                  # 随机数工具
│
├── ObjGenerator/                  # OBJ 文件生成器 (可执行程序)
│   ├── ObjGenerator.csproj
│   └── ObjGeneratorMain.cs        # 主程序入口
│
├── MathLibraryTest/               # 单位测试项目
│   ├── MathLibraryTest.csproj
│   └── ...                        # 测试代码
│
├── ObjLibrary/                    # 旧代码存档 (可选)
│   └── ...
│
├── src/                           # 旧数学库源文件 (已过时，使用 MathLibrary/ 替代)
│   └── ...
│
├── UnityLikeTools.sln             # 解决方案文件
├── MathLibrary.csproj             # 旧项目文件 (已过时，使用 MathLibrary/ 替代)
├── README.md
└── OPTIMIZATION_REPORT.md
```

## 🏗️ 依赖关系

```
MathLibrary (库)
    ↑
    ├── ObjGenerator (可执行程序)
    └── MathLibraryTest (测试程序)
```

## 🔨 编译与运行

### 编译整个解决方案
```bash
cd D:\github\ObjLibrary
dotnet build UnityLikeTools.sln
```

### 运行 ObjGenerator
```bash
cd D:\github\ObjLibrary
dotnet run --project ObjGenerator/ObjGenerator.csproj
```

或直接运行编译后的 exe：
```bash
.\ObjGenerator\bin\Debug\net8.0\ObjGenerator.exe
```

### 运行测试
```bash
dotnet run --project MathLibraryTest/MathLibraryTest.csproj
```

## 📦 MathLibrary 的主要类型

### 向量类
- `Vector2` - 2D 向量 (x, y)
- `Vector3` - 3D 向量 (x, y, z)
- `Vector4` - 4D 向量 (x, y, z, w)

### 旋转和矩阵
- `Quaternion` - 四元数，用于表示旋转
- `Matrix4x4` - 4x4 变换矩阵

### 数学工具
- `Mathf` - 常用数学函数 (Sin, Cos, Clamp 等)
- `Color` - RGB/RGBA 颜色结构体
- `Random` - 随机数生成工具

## 🎯 关键特性

### ObjGenerator 功能
- 生成基础 3D 网格：立方体、球体、圆柱体、圆锥体、四棱锥、平面
- 生成草叶网格 (通过分段条形)
- 网格变换 (移动、旋转、缩放)
- 导出到 OBJ 文件格式

### 代码质量
- ✅ 完整的 XML 文档注释
- ✅ 无编译警告
- ✅ 参数验证和错误处理
- ✅ 性能优化

## 📝 最近的改进

- [x] 将数学库组织到独立文件夹 `MathLibrary/`
- [x] 更新项目引用路径
- [x] 全面代码优化和文档改进
- [x] 添加异常处理和进度反馈

## 🚀 快速开始

1. **克隆/打开项目**
   ```bash
   cd d:\github\ObjLibrary
   ```

2. **编译项目**
   ```bash
   dotnet build UnityLikeTools.sln
   ```

3. **运行示例**
   ```bash
   dotnet run --project ObjGenerator/ObjGenerator.csproj
   ```

输出文件将在当前目录的 `mergedGrass.obj`

## 📚 使用示例

### 创建向量
```csharp
Vector3 position = new Vector3(1, 2, 3);
Vector3 direction = new Vector3(0, 0, 1);
```

### 生成网格
```csharp
// 生成球体
Mesh sphere = MeshLibrary.CreateSphere(radius: 1f, widthSegments: 32, heightSegments: 16);

// 生成圆柱体
Mesh cylinder = MeshLibrary.CreateCylinder(radius: 1f, height: 2f, segments: 32);
```

### 导出 OBJ
```csharp
MeshTools.WriteMeshToObj(mesh, "output.obj");
```

---

**项目状态**：✅ 整理完成，编译成功，功能验证通过
