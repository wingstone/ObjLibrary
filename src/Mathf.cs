using System;

namespace MathLibrary
{
    /// <summary>
    /// 数学函数辅助类
    /// </summary>
    public static class Mathf
    {
        public const float PI = 3.14159265358979323846f;
        public const float Tau = 2f * PI;
        public const float Epsilon = 0.00000001f;
        public const float Deg2Rad = PI / 180f;
        public const float Rad2Deg = 180f / PI;

        /// <summary>
        /// 返回绝对值
        /// </summary>
        public static float Abs(float value) => MathF.Abs(value);

        /// <summary>
        /// 返回平方根
        /// </summary>
        public static float Sqrt(float value) => MathF.Sqrt(value);

        /// <summary>
        /// 返回绝对值
        /// </summary>
        public static int Abs(int value) => Math.Abs(value);

        /// <summary>
        /// 返回最小值
        /// </summary>
        public static float Min(float a, float b) => a < b ? a : b;
        public static int Min(int a, int b) => a < b ? a : b;

        /// <summary>
        /// 返回最大值
        /// </summary>
        public static float Max(float a, float b) => a > b ? a : b;
        public static int Max(int a, int b) => a > b ? a : b;

        /// <summary>
        /// 将值限制在min和max之间
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// 将值限制在0和1之间
        /// </summary>
        public static float Clamp01(float value) => Clamp(value, 0f, 1f);

        /// <summary>
        /// 线性插值
        /// </summary>
        public static float Lerp(float a, float b, float t) => a + (b - a) * Clamp01(t);

        /// <summary>
        /// 无限制的线性插值
        /// </summary>
        public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;

        /// <summary>
        /// 反向线性插值
        /// </summary>
        public static float InverseLerp(float a, float b, float value)
        {
            if (a == b) return 0f;
            return (value - a) / (b - a);
        }

        /// <summary>
        /// 平滑阶跃插值 (Smoothstep)
        /// </summary>
        public static float SmoothStep(float a, float b, float t)
        {
            float x = Clamp01((t - a) / (b - a));
            return x * x * (3f - 2f * x);
        }

        /// <summary>
        /// 舍入到最近的整数
        /// </summary>
        public static float Round(float value) => MathF.Round(value);
        public static int RoundToInt(float value) => (int)MathF.Round(value);

        /// <summary>
        /// 向下取整
        /// </summary>
        public static float Floor(float value) => MathF.Floor(value);
        public static int FloorToInt(float value) => (int)MathF.Floor(value);

        /// <summary>
        /// 向上取整
        /// </summary>
        public static float Ceil(float value) => MathF.Ceiling(value);
        public static int CeilToInt(float value) => (int)MathF.Ceiling(value);

        /// <summary>
        /// 返回remainder / divisor的余数
        /// </summary>
        public static float Repeat(float t, float length) => t - MathF.Floor(t / length) * length;

        /// <summary>
        /// 返回pingpong值（在0和length之间来回摆动）
        /// </summary>
        public static float PingPong(float t, float length)
        {
            float mod = Repeat(t, length * 2f);
            return length - MathF.Abs(mod - length);
        }

        /// <summary>
        /// 正弦函数（弧度）
        /// </summary>
        public static float Sin(float value) => MathF.Sin(value);

        /// <summary>
        /// 余弦函数（弧度）
        /// </summary>
        public static float Cos(float value) => MathF.Cos(value);

        /// <summary>
        /// 正切函数（弧度）
        /// </summary>
        public static float Tan(float value) => MathF.Tan(value);

        /// <summary>
        /// 反正弦函数（弧度）
        /// </summary>
        public static float Asin(float value) => MathF.Asin(value);

        /// <summary>
        /// 反余弦函数（弧度）
        /// </summary>
        public static float Acos(float value) => MathF.Acos(value);

        /// <summary>
        /// 反正切函数（弧度）
        /// </summary>
        public static float Atan(float value) => MathF.Atan(value);

        /// <summary>
        /// Atan2函数（弧度）
        /// </summary>
        public static float Atan2(float y, float x) => MathF.Atan2(y, x);

        /// <summary>
        /// 返回value的幂
        /// </summary>
        public static float Pow(float value, float power) => MathF.Pow(value, power);

        /// <summary>
        /// 返回e的power次方
        /// </summary>
        public static float Exp(float power) => MathF.Exp(power);

        /// <summary>
        /// 返回自然对数
        /// </summary>
        public static float Log(float value) => MathF.Log(value);

        /// <summary>
        /// 返回以10为底的对数
        /// </summary>
        public static float Log10(float value) => MathF.Log10(value);

        /// <summary>
        /// 返回以base为底的对数
        /// </summary>
        public static float Log(float value, float baseValue) => MathF.Log(value, baseValue);

        /// <summary>
        /// 检查两个浮点数是否近似相等
        /// </summary>
        public static bool Approximately(float a, float b) => Abs(b - a) <= Max(0.000001f * Max(Abs(a), Abs(b)), Epsilon * 8f);

        /// <summary>
        /// 将角度转换为弧度
        /// </summary>
        public static float DegreesToRadians(float degrees) => degrees * Deg2Rad;

        /// <summary>
        /// 将弧度转换为角度
        /// </summary>
        public static float RadiansToDegrees(float radians) => radians * Rad2Deg;

        /// <summary>
        /// 符号函数（返回-1, 0或1）
        /// </summary>
        public static float Sign(float value)
        {
            if (value > 0) return 1f;
            if (value < 0) return -1f;
            return 0f;
        }

        public static int Sign(int value)
        {
            if (value > 0) return 1;
            if (value < 0) return -1;
            return 0;
        }

        /// <summary>
        /// 平方
        /// </summary>
        public static float Sqr(float value) => value * value;

        /// <summary>
        /// 立方
        /// </summary>
        public static float Cube(float value) => value * value * value;
    }
}
