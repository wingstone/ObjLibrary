using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示四元数，用于3D旋转
    /// </summary>
    public struct Quaternion : IEquatable<Quaternion>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 1f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);
        public float SqrMagnitude => x * x + y * y + z * z + w * w;

        public Quaternion Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f) return Quaternion.identity;
                return new Quaternion(x / mag, y / mag, z / mag, w / mag);
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

        public Quaternion Conjugate => new Quaternion(-x, -y, -z, w);
        public Quaternion Inverse => Conjugate / SqrMagnitude;

        /// <summary>
        /// 从欧拉角创建四元数 (角度制)
        /// </summary>
        public static Quaternion Euler(float x, float y, float z)
        {
            float cx = MathF.Cos(Mathf.Deg2Rad * x / 2f);
            float sx = MathF.Sin(Mathf.Deg2Rad * x / 2f);
            float cy = MathF.Cos(Mathf.Deg2Rad * y / 2f);
            float sy = MathF.Sin(Mathf.Deg2Rad * y / 2f);
            float cz = MathF.Cos(Mathf.Deg2Rad * z / 2f);
            float sz = MathF.Sin(Mathf.Deg2Rad * z / 2f);

            return new Quaternion(
                sx * cy * cz - cx * sy * sz,
                cx * sy * cz + sx * cy * sz,
                cx * cy * sz - sx * sy * cz,
                cx * cy * cz + sx * sy * sz
            ).Normalized;
        }

        /// <summary>
        /// 从轴和角度创建四元数
        /// </summary>
        public static Quaternion AxisAngle(Vector3 axis, float angle)
        {
            axis = axis.Normalized;
            float halfAngle = Mathf.Deg2Rad * angle / 2f;
            float sin = MathF.Sin(halfAngle);
            return new Quaternion(
                axis.x * sin,
                axis.y * sin,
                axis.z * sin,
                MathF.Cos(halfAngle)
            );
        }

        /// <summary>
        /// 旋转向量
        /// </summary>
        public Vector3 RotateVector(Vector3 v)
        {
            // v' = q * v * q^(-1)
            Quaternion qv = new Quaternion(v.x, v.y, v.z, 0f);
            Quaternion result = this * qv * Inverse;
            return new Vector3(result.x, result.y, result.z);
        }

        /// <summary>
        /// 球形插值
        /// </summary>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            t = Mathf.Clamp01(t);
            float dot = Dot(a, b);
            
            if (dot < 0f)
            {
                b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
                dot = -dot;
            }

            dot = Mathf.Clamp(dot, -1f, 1f);
            float theta = MathF.Acos(dot);
            float sinTheta = MathF.Sin(theta);

            if (sinTheta < 0.001f)
                return Lerp(a, b, t);

            float w0 = MathF.Sin((1f - t) * theta) / sinTheta;
            float w1 = MathF.Sin(t * theta) / sinTheta;

            return new Quaternion(
                a.x * w0 + b.x * w1,
                a.y * w0 + b.y * w1,
                a.z * w0 + b.z * w1,
                a.w * w0 + b.w * w1
            ).Normalized;
        }

        public static float Dot(Quaternion a, Quaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t) 
            => (a * (1f - t) + b * t).Normalized;

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return new Quaternion(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
                a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
            );
        }

        public static Quaternion operator *(Quaternion q, float s) => new Quaternion(q.x * s, q.y * s, q.z * s, q.w * s);
        public static Quaternion operator /(Quaternion q, float s) => new Quaternion(q.x / s, q.y / s, q.z / s, q.w / s);
        public static Quaternion operator +(Quaternion a, Quaternion b) => new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static bool operator ==(Quaternion a, Quaternion b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(Quaternion a, Quaternion b) => !(a == b);

        public override bool Equals(object? obj) => obj is Quaternion q && Equals(q);
        public bool Equals(Quaternion other) => x == other.x && y == other.y && z == other.z && w == other.w;
        public override int GetHashCode() => HashCode.Combine(x, y, z, w);
        public override string ToString() => $"({x:F2}, {y:F2}, {z:F2}, {w:F2})";

        public static Quaternion identity = new Quaternion(0f, 0f, 0f, 1f);
    }
}
