using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个2D整数向量
    /// </summary>
    public struct VectorInt2 : IEquatable<VectorInt2>
    {
        public int x;
        public int y;

        public VectorInt2(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y);
        public int SqrMagnitude => x * x + y * y;

        public VectorInt2 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return VectorInt2.zero;
                return new VectorInt2((int)(x / mag), (int)(y / mag));
            }
        }

        public static int Dot(VectorInt2 a, VectorInt2 b) => a.x * b.x + a.y * b.y;
        public static float Distance(VectorInt2 a, VectorInt2 b) => MathF.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        public static VectorInt2 Lerp(VectorInt2 a, VectorInt2 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new VectorInt2(
                (int)(a.x + (b.x - a.x) * t),
                (int)(a.y + (b.y - a.y) * t)
            );
        }

        public static VectorInt2 operator +(VectorInt2 a, VectorInt2 b) => new VectorInt2(a.x + b.x, a.y + b.y);
        public static VectorInt2 operator -(VectorInt2 a, VectorInt2 b) => new VectorInt2(a.x - b.x, a.y - b.y);
        public static VectorInt2 operator *(VectorInt2 a, int b) => new VectorInt2(a.x * b, a.y * b);
        public static VectorInt2 operator /(VectorInt2 a, int b) => new VectorInt2(a.x / b, a.y / b);
        public static bool operator ==(VectorInt2 a, VectorInt2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(VectorInt2 a, VectorInt2 b) => !(a == b);

        public override bool Equals(object? obj) => obj is VectorInt2 v && Equals(v);
        public bool Equals(VectorInt2 other) => x == other.x && y == other.y;
        public override int GetHashCode() => HashCode.Combine(x, y);
        public override string ToString() => $"({x}, {y})";

        public static VectorInt2 zero = new VectorInt2(0, 0);
        public static VectorInt2 one = new VectorInt2(1, 1);
        public static VectorInt2 right = new VectorInt2(1, 0);
        public static VectorInt2 up = new VectorInt2(0, 1);
    }
}
