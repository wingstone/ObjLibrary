using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个4维整数向量
    /// </summary>
    public struct VectorInt4 : IEquatable<VectorInt4>
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public VectorInt4(int x = 0, int y = 0, int z = 0, int w = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);
        public int SqrMagnitude => x * x + y * y + z * z + w * w;

        public VectorInt4 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return VectorInt4.zero;
                return new VectorInt4((int)(x / mag), (int)(y / mag), (int)(z / mag), (int)(w / mag));
            }
        }

        public static int Dot(VectorInt4 a, VectorInt4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static float Distance(VectorInt4 a, VectorInt4 b) => MathF.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z) + (a.w - b.w) * (a.w - b.w));
        public static VectorInt4 Lerp(VectorInt4 a, VectorInt4 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new VectorInt4(
                (int)(a.x + (b.x - a.x) * t),
                (int)(a.y + (b.y - a.y) * t),
                (int)(a.z + (b.z - a.z) * t),
                (int)(a.w + (b.w - a.w) * t)
            );
        }

        public static VectorInt4 operator +(VectorInt4 a, VectorInt4 b) => new VectorInt4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static VectorInt4 operator -(VectorInt4 a, VectorInt4 b) => new VectorInt4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static VectorInt4 operator *(VectorInt4 a, int b) => new VectorInt4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static VectorInt4 operator /(VectorInt4 a, int b) => new VectorInt4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static bool operator ==(VectorInt4 a, VectorInt4 b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(VectorInt4 a, VectorInt4 b) => !(a == b);

        public override bool Equals(object? obj) => obj is VectorInt4 v && Equals(v);
        public bool Equals(VectorInt4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
        public override int GetHashCode() => HashCode.Combine(x, y, z, w);
        public override string ToString() => $"({x}, {y}, {z}, {w})";

        public static VectorInt4 zero = new VectorInt4(0, 0, 0, 0);
        public static VectorInt4 one = new VectorInt4(1, 1, 1, 1);
    }
}
