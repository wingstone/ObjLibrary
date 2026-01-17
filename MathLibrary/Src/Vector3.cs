using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个3D向量
    /// </summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x = 0f, float y = 0f, float z = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z);
        public float SqrMagnitude => x * x + y * y + z * z;

        public Vector3 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return Vector3.zero;
                return new Vector3(x / mag, y / mag, z / mag);
            }
        }

        public void Normalize()
        {
            float mag = Magnitude;
            if (mag == 0f) return;
            x /= mag;
            y /= mag;
            z /= mag;
        }

        public static float Dot(Vector3 a, Vector3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        
        public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x
        );

        public static float Distance(Vector3 a, Vector3 b) => (a - b).Magnitude;
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) => a + (b - a) * Mathf.Clamp01(t);

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.x * b, a.y * b, a.z * b);
        public static Vector3 operator /(Vector3 a, float b) => new Vector3(a.x / b, a.y / b, a.z / b);
        public static bool operator ==(Vector3 a, Vector3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);

        public override bool Equals(object? obj) => obj is Vector3 v && Equals(v);
        public bool Equals(Vector3 other) => x == other.x && y == other.y && z == other.z;
        public override int GetHashCode() => HashCode.Combine(x, y, z);
        public override string ToString() => $"({x}, {y}, {z})";

        public static Vector3 zero = new Vector3(0f, 0f, 0f);
        public static Vector3 one = new Vector3(1f, 1f, 1f);
        public static Vector3 right = new Vector3(1f, 0f, 0f);
        public static Vector3 up = new Vector3(0f, 1f, 0f);
        public static Vector3 forward = new Vector3(0f, 0f, 1f);
    }
}
