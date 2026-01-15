# MathLibrary - Unity风格数学库

一个为C#开发者提供的轻量级数学库，提供与Unity引擎相似的数学工具。

## 功能特性

### 向量类型
- **Vector2** - 2D向量，包含基本运算和点积
- **Vector3** - 3D向量，支持点积和叉积
- **Vector4** - 4D向量
- 支持标量乘法、向量加减、插值等操作

### 颜色
- **Color** - RGBA颜色，支持浮点范围[0,1]
- 内置常见颜色常量（红、绿、蓝、白、黑等）
- 支持与32位整数的相互转换

### 四元数
- **Quaternion** - 用于3D旋转表示
- 支持欧拉角和轴角转换
- 球形插值（SLERP）支持平滑旋转
- 向量旋转功能

### 矩阵
- **Matrix4x4** - 4x4变换矩阵
- 支持TRS变换（平移、旋转、缩放）
- 矩阵乘法和向量变换
- 行列式和逆矩阵计算
- 矩阵转置

### 数学函数
- **Mathf** - 数学工具类
- 三角函数（Sin, Cos, Tan, Asin, Acos, Atan）
- 插值函数（Lerp, SmoothStep, InverseLerp）
- 常用函数（Clamp, Min, Max, Abs, Sqrt等）
- 角度转换（度数↔弧度）
- 其他工具函数

## 项目结构

```
MathLibrary/
├── MathLibrary.csproj      # 项目配置文件
├── .vscode/
│   ├── launch.json         # 调试配置
│   ├── tasks.json          # 构建任务
│   └── settings.json       # VS Code设置
├── src/
│   ├── Vector2.cs          # 2D向量
│   ├── Vector3.cs          # 3D向量
│   ├── Vector4.cs          # 4D向量
│   ├── Color.cs            # 颜色
│   ├── Quaternion.cs       # 四元数
│   ├── Matrix4x4.cs        # 4x4矩阵
│   └── Mathf.cs            # 数学函数
└── README.md               # 本文档
```

## 使用方法

### 向量操作
```csharp
// 创建向量
Vector3 position = new Vector3(1f, 2f, 3f);
Vector3 forward = Vector3.forward;

// 基本运算
Vector3 result = position + forward;
float distance = Vector3.Distance(position, forward);
Vector3 normalized = position.Normalized;

// 点积和叉积
float dot = Vector3.Dot(position, forward);
Vector3 cross = Vector3.Cross(position, forward);
```

### 颜色操作
```csharp
// 创建颜色
Color red = new Color(1f, 0f, 0f, 1f);
Color blue = Color.blue;

// 颜色插值
Color blended = Color.Lerp(red, blue, 0.5f);

// 颜色转换
uint colorInt = red.ToInt32();
Color fromInt = Color.FromInt32(0xFF0000FF);
```

### 四元数旋转
```csharp
// 从欧拉角创建旋转
Quaternion rotation = Quaternion.Euler(45f, 60f, 30f);

// 旋转向量
Vector3 rotated = rotation.RotateVector(Vector3.forward);

// 球形插值
Quaternion blended = Quaternion.Slerp(rotation1, rotation2, 0.5f);
```

### 矩阵变换
```csharp
// 创建TRS矩阵（平移、旋转、缩放）
Vector3 position = new Vector3(5f, 0f, 0f);
Quaternion rotation = Quaternion.Euler(0f, 45f, 0f);
Vector3 scale = new Vector3(2f, 2f, 2f);

Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

// 变换向量
Vector3 transformed = transform * Vector3.forward;
```

### 数学函数
```csharp
// 插值
float value = Mathf.Lerp(0f, 100f, 0.5f);

// 限制值
float clamped = Mathf.Clamp(value, 0f, 1f);

// 三角函数
float sinValue = Mathf.Sin(Mathf.PI / 2f);

// 角度转换
float radians = Mathf.Deg2Rad * 180f;
float degrees = Mathf.Rad2Deg * radians;
```

## 编译和运行

### 编译项目
```bash
dotnet build
```

### 创建单元测试项目（可选）
```bash
dotnet new xunit -n MathLibrary.Tests
cd MathLibrary.Tests
dotnet add reference ../MathLibrary.csproj
```

### 运行测试
```bash
dotnet test
```

## 系统要求

- .NET 8.0 或更高版本
- Visual Studio Code
- C# 扩展（推荐）

## 推荐的VS Code扩展

- **C#** (ms-dotnettools.csharp) - 提供C#支持
- **.NET Core Test Explorer** - 测试浏览器
- **Omnisharp** - 代码智能提示

## 许可证

MIT License

## 常见问题

### Q: 如何在我的项目中使用这个库？
A: 可以通过dotnet add reference引用这个项目，或者将编译后的DLL复制到你的项目中。

### Q: 为什么四元数的旋转和Unity有所不同？
A: 这个库使用标准的四元数数学公式。如果你发现差异，请检查四元数的初始化顺序（x,y,z,w）。

### Q: 支持其他类型的矩阵吗（如3x3）？
A: 当前版本只提供4x4矩阵。如需3x3矩阵，可以在Matrix4x4中提取左上角的3x3部分。
