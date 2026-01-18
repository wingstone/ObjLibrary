using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个3D整数向量
    /// </summary>
    public struct VectorInt3 : IEquatable<VectorInt3>
    {
        public int x;
        public int y;
        public int z;

        public VectorInt3(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z);
        public int SqrMagnitude => x * x + y * y + z * z;

        public VectorInt3 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return VectorInt3.zero;
                return new VectorInt3((int)(x / mag), (int)(y / mag), (int)(z / mag));
            }
        }

        public static int Dot(VectorInt3 a, VectorInt3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        
        public static VectorInt3 Cross(VectorInt3 a, VectorInt3 b) => new VectorInt3(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x
        );

        public static float Distance(VectorInt3 a, VectorInt3 b) => MathF.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        public static VectorInt3 Lerp(VectorInt3 a, VectorInt3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new VectorInt3(
                (int)(a.x + (b.x - a.x) * t),
                (int)(a.y + (b.y - a.y) * t),
                (int)(a.z + (b.z - a.z) * t)
            );
        }

        public static VectorInt3 operator +(VectorInt3 a, VectorInt3 b) => new VectorInt3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static VectorInt3 operator -(VectorInt3 a, VectorInt3 b) => new VectorInt3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static VectorInt3 operator *(VectorInt3 a, int b) => new VectorInt3(a.x * b, a.y * b, a.z * b);
        public static VectorInt3 operator /(VectorInt3 a, int b) => new VectorInt3(a.x / b, a.y / b, a.z / b);
        public static bool operator ==(VectorInt3 a, VectorInt3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(VectorInt3 a, VectorInt3 b) => !(a == b);

        public override bool Equals(object? obj) => obj is VectorInt3 v && Equals(v);
        public bool Equals(VectorInt3 other) => x == other.x && y == other.y && z == other.z;
        public override int GetHashCode() => HashCode.Combine(x, y, z);
        public override string ToString() => $"({x}, {y}, {z})";

        public static VectorInt3 zero = new VectorInt3(0, 0, 0);
        public static VectorInt3 one = new VectorInt3(1, 1, 1);
        public static VectorInt3 right = new VectorInt3(1, 0, 0);
        public static VectorInt3 up = new VectorInt3(0, 1, 0);
        public static VectorInt3 forward = new VectorInt3(0, 0, 1);
    }
}
