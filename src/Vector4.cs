using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个4维向量
    /// </summary>
    public struct Vector4 : IEquatable<Vector4>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);
        public float SqrMagnitude => x * x + y * y + z * z + w * w;

        public Vector4 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return Vector4.zero;
                return new Vector4(x / mag, y / mag, z / mag, w / mag);
            }
        }

        public void Normalize()
        {
            float mag = Magnitude;
            if (mag == 0f) return;
            x /= mag;
            y /= mag;
            z /= mag;
            w /= mag;
        }

        public static float Dot(Vector4 a, Vector4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static float Distance(Vector4 a, Vector4 b) => (a - b).Magnitude;
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t) => a + (b - a) * Mathf.Clamp01(t);

        public static Vector4 operator +(Vector4 a, Vector4 b) => new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Vector4 operator -(Vector4 a, Vector4 b) => new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Vector4 operator *(Vector4 a, float b) => new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static Vector4 operator /(Vector4 a, float b) => new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static bool operator ==(Vector4 a, Vector4 b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(Vector4 a, Vector4 b) => !(a == b);

        public override bool Equals(object? obj) => obj is Vector4 v && Equals(v);
        public bool Equals(Vector4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
        public override int GetHashCode() => HashCode.Combine(x, y, z, w);
        public override string ToString() => $"({x}, {y}, {z}, {w})";

        public static Vector4 zero = new Vector4(0f, 0f, 0f, 0f);
        public static Vector4 one = new Vector4(1f, 1f, 1f, 1f);
    }
}
