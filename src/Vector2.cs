using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个2D向量
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        public float x;
        public float y;

        public Vector2(float x = 0f, float y = 0f)
        {
            this.x = x;
            this.y = y;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y);
        public float SqrMagnitude => x * x + y * y;

        public Vector2 Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return Vector2.zero;
                return new Vector2(x / mag, y / mag);
            }
        }

        public void Normalize()
        {
            float mag = Magnitude;
            if (mag == 0f) return;
            x /= mag;
            y /= mag;
        }

        public static float Dot(Vector2 a, Vector2 b) => a.x * b.x + a.y * b.y;
        public static float Distance(Vector2 a, Vector2 b) => (a - b).Magnitude;
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => a + (b - a) * Mathf.Clamp01(t);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b);
        public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.x / b, a.y / b);
        public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        public override bool Equals(object? obj) => obj is Vector2 v && Equals(v);
        public bool Equals(Vector2 other) => x == other.x && y == other.y;
        public override int GetHashCode() => HashCode.Combine(x, y);
        public override string ToString() => $"({x}, {y})";

        public static Vector2 zero = new Vector2(0f, 0f);
        public static Vector2 one = new Vector2(1f, 1f);
        public static Vector2 right = new Vector2(1f, 0f);
        public static Vector2 up = new Vector2(0f, 1f);
    }
}
