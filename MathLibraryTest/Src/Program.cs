using System;
using MathLibrary;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         MathLibrary 完整功能测试和验证程序                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        // Vector2 测试
        TestVector2();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Vector3 测试
        TestVector3();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Vector4 测试
        TestVector4();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Color 测试
        TestColor();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Quaternion 测试
        TestQuaternion();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Matrix4x4 测试
        TestMatrix4x4();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        // Mathf 测试
        TestMathf();
        Console.WriteLine("\n" + new string('=', 60) + "\n");

        Console.WriteLine("✓ 所有测试完成！");
    }

    static void TestVector2()
    {
        Console.WriteLine("【Vector2 测试】");
        
        Vector2 v1 = new Vector2(3f, 4f);
        Vector2 v2 = new Vector2(1f, 2f);
        
        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 - v2 = {v1 - v2}");
        Console.WriteLine($"v1 * 2 = {v1 * 2f}");
        Console.WriteLine($"v1 的模长 = {v1.Magnitude:F2}");
        Console.WriteLine($"v1 的平方和 = {v1.SqrMagnitude:F2}");
        Console.WriteLine($"v1 的归一化向量 = {v1.Normalized}");
        Console.WriteLine($"点积(v1, v2) = {Vector2.Dot(v1, v2):F2}");
        Console.WriteLine($"距离(v1, v2) = {Vector2.Distance(v1, v2):F2}");
        Console.WriteLine($"线性插值(v1, v2, 0.5) = {Vector2.Lerp(v1, v2, 0.5f)}");
        Console.WriteLine($"Vector2.right = {Vector2.right}");
        Console.WriteLine($"Vector2.up = {Vector2.up}");
        Vector2 v3 = new Vector2(3f, 4f);
        Console.WriteLine($"相等性检查: v1 == v3? {v1 == v3}");
    }

    static void TestVector3()
    {
        Console.WriteLine("【Vector3 测试】");
        
        Vector3 v1 = new Vector3(1f, 2f, 3f);
        Vector3 v2 = new Vector3(4f, 5f, 6f);
        
        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 - v2 = {v1 - v2}");
        Console.WriteLine($"v1 * 2 = {v1 * 2f}");
        Console.WriteLine($"v1 / 2 = {v1 / 2f}");
        Console.WriteLine($"v1 的模长 = {v1.Magnitude:F2}");
        Console.WriteLine($"v1 的平方和 = {v1.SqrMagnitude:F2}");
        Console.WriteLine($"v1 的归一化向量 = {v1.Normalized}");
        Console.WriteLine($"点积(v1, v2) = {Vector3.Dot(v1, v2):F2}");
        Console.WriteLine($"叉积(v1, v2) = {Vector3.Cross(v1, v2)}");
        Console.WriteLine($"距离(v1, v2) = {Vector3.Distance(v1, v2):F2}");
        Console.WriteLine($"线性插值(v1, v2, 0.5) = {Vector3.Lerp(v1, v2, 0.5f)}");
        Console.WriteLine($"Vector3.right = {Vector3.right}");
        Console.WriteLine($"Vector3.up = {Vector3.up}");
        Console.WriteLine($"Vector3.forward = {Vector3.forward}");
    }

    static void TestVector4()
    {
        Console.WriteLine("【Vector4 测试】");
        
        Vector4 v1 = new Vector4(1f, 2f, 3f, 4f);
        Vector4 v2 = new Vector4(5f, 6f, 7f, 8f);
        
        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 的模长 = {v1.Magnitude:F2}");
        Console.WriteLine($"v1 的归一化向量 = {v1.Normalized}");
        Console.WriteLine($"点积(v1, v2) = {Vector4.Dot(v1, v2):F2}");
        Console.WriteLine($"距离(v1, v2) = {Vector4.Distance(v1, v2):F2}");
        Console.WriteLine($"线性插值(v1, v2, 0.3) = {Vector4.Lerp(v1, v2, 0.3f)}");
        Console.WriteLine($"Vector4.one = {Vector4.one}");
    }

    static void TestColor()
    {
        Console.WriteLine("【Color 测试】");
        
        Color red = Color.red;
        Color blue = Color.blue;
        Color custom = new Color(0.5f, 0.2f, 0.8f, 1f);
        
        Console.WriteLine($"红色 = {red}");
        Console.WriteLine($"蓝色 = {blue}");
        Console.WriteLine($"自定义颜色 = {custom}");
        Console.WriteLine($"红 + 蓝 = {red + blue}");
        Console.WriteLine($"红 * 0.5 = {red * 0.5f}");
        Console.WriteLine($"线性插值(红, 蓝, 0.5) = {Color.Lerp(red, blue, 0.5f)}");
        
        uint colorInt = red.ToInt32();
        Console.WriteLine($"红色转为整数: {colorInt:X8}");
        Color fromInt = Color.FromInt32(0xFF0000FF);
        Console.WriteLine($"从整数转回: {fromInt}");
        
        Console.WriteLine($"预定义颜色 - 绿: {Color.green}");
        Console.WriteLine($"预定义颜色 - 黄: {Color.yellow}");
        Console.WriteLine($"预定义颜色 - 青: {Color.cyan}");
        Console.WriteLine($"预定义颜色 - 白: {Color.white}");
        Console.WriteLine($"预定义颜色 - 黑: {Color.black}");
        Console.WriteLine($"预定义颜色 - 灰: {Color.gray}");
    }

    static void TestQuaternion()
    {
        Console.WriteLine("【Quaternion 测试】");
        
        // 从欧拉角创建
        Quaternion rot1 = Quaternion.Euler(45f, 0f, 0f);
        Quaternion rot2 = Quaternion.Euler(0f, 90f, 0f);
        Quaternion rot3 = Quaternion.Euler(0f, 0f, 30f);
        
        Console.WriteLine($"欧拉角(45, 0, 0)转四元数 = {rot1}");
        Console.WriteLine($"欧拉角(0, 90, 0)转四元数 = {rot2}");
        Console.WriteLine($"欧拉角(0, 0, 30)转四元数 = {rot3}");
        
        // 四元数乘法组合旋转
        Quaternion combined = rot2 * rot1;
        Console.WriteLine($"rot2 * rot1 = {combined}");
        
        // 旋转向量
        Vector3 forward = Vector3.forward;
        Vector3 rotated = rot2.RotateVector(forward);
        Console.WriteLine($"原向量(0, 0, 1) 绕Y轴旋转90° = {rotated}");
        
        // 球形插值
        Quaternion slerp = Quaternion.Slerp(rot1, rot2, 0.5f);
        Console.WriteLine($"Slerp(rot1, rot2, 0.5) = {slerp}");
        
        // 线性插值
        Quaternion lerp = Quaternion.Lerp(rot1, rot2, 0.5f);
        Console.WriteLine($"Lerp(rot1, rot2, 0.5) = {lerp}");
        
        // 四元数属性
        Console.WriteLine($"四元数模长 = {rot1.Magnitude:F2}");
        Console.WriteLine($"四元数共轭 = {rot1.Conjugate}");
        Console.WriteLine($"恒等四元数 = {Quaternion.identity}");
    }

    static void TestMatrix4x4()
    {
        Console.WriteLine("【Matrix4x4 测试】");
        
        // 平移矩阵
        Vector3 translation = new Vector3(5f, 0f, 0f);
        Matrix4x4 translateMat = Matrix4x4.Translate(translation);
        Console.WriteLine($"平移矩阵(5, 0, 0):");
        PrintMatrix(translateMat);
        
        // 缩放矩阵
        Vector3 scale = new Vector3(2f, 3f, 1f);
        Matrix4x4 scaleMat = Matrix4x4.Scale(scale);
        Console.WriteLine($"缩放矩阵(2, 3, 1):");
        PrintMatrix(scaleMat);
        
        // 旋转矩阵
        Quaternion rotation = Quaternion.Euler(0f, 45f, 0f);
        Matrix4x4 rotateMat = Matrix4x4.Rotate(rotation);
        Console.WriteLine($"旋转矩阵(0, 45, 0):");
        PrintMatrix(rotateMat);
        
        // TRS矩阵
        Matrix4x4 transform = Matrix4x4.TRS(translation, rotation, scale);
        Console.WriteLine($"TRS矩阵 (T:{translation}, R:45°, S:{scale}):");
        PrintMatrix(transform);
        
        // 向量变换
        Vector3 point = Vector3.one;
        Vector3 transformed = transform * point;
        Console.WriteLine($"点(1, 1, 1)变换后 = {transformed}");
        
        // 矩阵乘法
        Matrix4x4 combined = translateMat * scaleMat;
        Console.WriteLine($"平移矩阵 * 缩放矩阵:");
        PrintMatrix(combined);
        
        // 矩阵属性
        Console.WriteLine($"行列式 = {transform.Determinant:F2}");
        Console.WriteLine($"逆矩阵:");
        PrintMatrix(transform.Inverse);
        
        // 恒等矩阵
        Console.WriteLine($"恒等矩阵:");
        PrintMatrix(Matrix4x4.identity);
    }

    static void PrintMatrix(Matrix4x4 m)
    {
        Console.WriteLine($"  [{m.m00:F2} {m.m01:F2} {m.m02:F2} {m.m03:F2}]");
        Console.WriteLine($"  [{m.m10:F2} {m.m11:F2} {m.m12:F2} {m.m13:F2}]");
        Console.WriteLine($"  [{m.m20:F2} {m.m21:F2} {m.m22:F2} {m.m23:F2}]");
        Console.WriteLine($"  [{m.m30:F2} {m.m31:F2} {m.m32:F2} {m.m33:F2}]");
    }

    static void TestMathf()
    {
        Console.WriteLine("【Mathf 测试】");
        
        // 常数
        Console.WriteLine($"PI = {Mathf.PI:F5}");
        Console.WriteLine($"Tau = {Mathf.Tau:F5}");
        Console.WriteLine($"Epsilon = {Mathf.Epsilon}");
        
        // 基础函数
        Console.WriteLine($"Abs(-5) = {Mathf.Abs(-5f)}");
        Console.WriteLine($"Min(3, 7) = {Mathf.Min(3f, 7f)}");
        Console.WriteLine($"Max(3, 7) = {Mathf.Max(3f, 7f)}");
        Console.WriteLine($"Sqrt(9) = {Mathf.Sqrt(9f)}");
        
        // 限制函数
        Console.WriteLine($"Clamp(150, 0, 100) = {Mathf.Clamp(150f, 0f, 100f)}");
        Console.WriteLine($"Clamp01(1.5) = {Mathf.Clamp01(1.5f)}");
        
        // 插值函数
        Console.WriteLine($"Lerp(0, 100, 0.3) = {Mathf.Lerp(0f, 100f, 0.3f):F1}");
        Console.WriteLine($"LerpUnclamped(0, 100, 1.5) = {Mathf.LerpUnclamped(0f, 100f, 1.5f):F1}");
        Console.WriteLine($"InverseLerp(0, 100, 30) = {Mathf.InverseLerp(0f, 100f, 30f):F2}");
        Console.WriteLine($"SmoothStep(0, 100, 50) = {Mathf.SmoothStep(0f, 100f, 50f):F2}");
        
        // 舍入函数
        Console.WriteLine($"Round(3.7) = {Mathf.Round(3.7f)}");
        Console.WriteLine($"Floor(3.7) = {Mathf.Floor(3.7f)}");
        Console.WriteLine($"Ceil(3.2) = {Mathf.Ceil(3.2f)}");
        
        // 三角函数
        Console.WriteLine($"Sin(PI/2) = {Mathf.Sin(Mathf.PI / 2f):F2}");
        Console.WriteLine($"Cos(0) = {Mathf.Cos(0f):F2}");
        Console.WriteLine($"Tan(PI/4) = {Mathf.Tan(Mathf.PI / 4f):F2}");
        
        // 角度转换
        float degrees = 180f;
        float radians = Mathf.Deg2Rad * degrees;
        Console.WriteLine($"{degrees}° 转弧度 = {radians:F5}");
        Console.WriteLine($"弧度 * Rad2Deg = {radians * Mathf.Rad2Deg:F1}°");
        
        // 其他函数
        Console.WriteLine($"Sign(-10) = {Mathf.Sign(-10f)}");
        Console.WriteLine($"Sign(0) = {Mathf.Sign(0f)}");
        Console.WriteLine($"Sign(5) = {Mathf.Sign(5f)}");
        Console.WriteLine($"Pow(2, 3) = {Mathf.Pow(2f, 3f):F0}");
        Console.WriteLine($"Exp(1) = {Mathf.Exp(1f):F2}");
        Console.WriteLine($"Log(10) = {Mathf.Log(10f):F2}");
        
        // PingPong函数
        Console.WriteLine($"PingPong(5, 10) = {Mathf.PingPong(5f, 10f):F2}");
        Console.WriteLine($"PingPong(15, 10) = {Mathf.PingPong(15f, 10f):F2}");
        
        // Approximately 函数
        Console.WriteLine($"Approximately(1.0, 1.0000001) = {Mathf.Approximately(1.0f, 1.0000001f)}");
        
        // 平方和立方
        Console.WriteLine($"Sqr(5) = {Mathf.Sqr(5f):F0}");
        Console.WriteLine($"Cube(3) = {Mathf.Cube(3f):F0}");
    }
}
