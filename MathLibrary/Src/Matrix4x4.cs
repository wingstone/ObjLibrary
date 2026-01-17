using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示一个4x4矩阵，用于3D变换
    /// </summary>
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public Matrix4x4(
            float m00 = 1f, float m01 = 0f, float m02 = 0f, float m03 = 0f,
            float m10 = 0f, float m11 = 1f, float m12 = 0f, float m13 = 0f,
            float m20 = 0f, float m21 = 0f, float m22 = 1f, float m23 = 0f,
            float m30 = 0f, float m31 = 0f, float m32 = 0f, float m33 = 1f)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
            this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        public float Determinant
        {
            get
            {
                float a = m00 * (m11 * m22 * m33 + m12 * m23 * m31 + m13 * m21 * m32 - m13 * m22 * m31 - m12 * m21 * m33 - m11 * m23 * m32);
                float b = -m01 * (m10 * m22 * m33 + m12 * m23 * m30 + m13 * m20 * m32 - m13 * m22 * m30 - m12 * m20 * m33 - m10 * m23 * m32);
                float c = m02 * (m10 * m21 * m33 + m11 * m23 * m30 + m13 * m20 * m31 - m13 * m21 * m30 - m11 * m20 * m33 - m10 * m23 * m31);
                float d = -m03 * (m10 * m21 * m32 + m11 * m22 * m30 + m12 * m20 * m31 - m12 * m21 * m30 - m11 * m20 * m32 - m10 * m22 * m31);
                return a + b + c + d;
            }
        }

        public Matrix4x4 Inverse
        {
            get
            {
                float det = Determinant;
                if (Math.Abs(det) < 0.0001f)
                    return identity;

                Matrix4x4 result = new Matrix4x4();
                
                // 计算各个元素的代数余子式
                result.m00 = (m11 * m22 * m33 + m12 * m23 * m31 + m13 * m21 * m32 - m13 * m22 * m31 - m12 * m21 * m33 - m11 * m23 * m32) / det;
                result.m01 = -(m01 * m22 * m33 + m02 * m23 * m31 + m03 * m21 * m32 - m03 * m22 * m31 - m02 * m21 * m33 - m01 * m23 * m32) / det;
                result.m02 = (m01 * m12 * m33 + m02 * m13 * m31 + m03 * m11 * m32 - m03 * m12 * m31 - m02 * m11 * m33 - m01 * m13 * m32) / det;
                result.m03 = -(m01 * m12 * m23 + m02 * m13 * m21 + m03 * m11 * m22 - m03 * m12 * m21 - m02 * m11 * m23 - m01 * m13 * m22) / det;

                result.m10 = -(m10 * m22 * m33 + m12 * m23 * m30 + m13 * m20 * m32 - m13 * m22 * m30 - m12 * m20 * m33 - m10 * m23 * m32) / det;
                result.m11 = (m00 * m22 * m33 + m02 * m23 * m30 + m03 * m20 * m32 - m03 * m22 * m30 - m02 * m20 * m33 - m00 * m23 * m32) / det;
                result.m12 = -(m00 * m12 * m33 + m02 * m13 * m30 + m03 * m10 * m32 - m03 * m12 * m30 - m02 * m10 * m33 - m00 * m13 * m32) / det;
                result.m13 = (m00 * m12 * m23 + m02 * m13 * m20 + m03 * m10 * m22 - m03 * m12 * m20 - m02 * m10 * m23 - m00 * m13 * m22) / det;

                result.m20 = (m10 * m21 * m33 + m11 * m23 * m30 + m13 * m20 * m31 - m13 * m21 * m30 - m11 * m20 * m33 - m10 * m23 * m31) / det;
                result.m21 = -(m00 * m21 * m33 + m01 * m23 * m30 + m03 * m20 * m31 - m03 * m21 * m30 - m01 * m20 * m33 - m00 * m23 * m31) / det;
                result.m22 = (m00 * m11 * m33 + m01 * m13 * m30 + m03 * m10 * m31 - m03 * m11 * m30 - m01 * m10 * m33 - m00 * m13 * m31) / det;
                result.m23 = -(m00 * m11 * m23 + m01 * m13 * m20 + m03 * m10 * m21 - m03 * m11 * m20 - m01 * m10 * m23 - m00 * m13 * m21) / det;

                result.m30 = -(m10 * m21 * m32 + m11 * m22 * m30 + m12 * m20 * m31 - m12 * m21 * m30 - m11 * m20 * m32 - m10 * m22 * m31) / det;
                result.m31 = (m00 * m21 * m32 + m01 * m22 * m30 + m02 * m20 * m31 - m02 * m21 * m30 - m01 * m20 * m32 - m00 * m22 * m31) / det;
                result.m32 = -(m00 * m11 * m32 + m01 * m12 * m30 + m02 * m10 * m31 - m02 * m11 * m30 - m01 * m10 * m32 - m00 * m12 * m31) / det;
                result.m33 = (m00 * m11 * m22 + m01 * m12 * m20 + m02 * m10 * m21 - m02 * m11 * m20 - m01 * m10 * m22 - m00 * m12 * m21) / det;

                return result;
            }
        }

        public Matrix4x4 Transpose
        {
            get
            {
                return new Matrix4x4(
                    m00, m10, m20, m30,
                    m01, m11, m21, m31,
                    m02, m12, m22, m32,
                    m03, m13, m23, m33
                );
            }
        }

        /// <summary>
        /// 创建平移矩阵
        /// </summary>
        public static Matrix4x4 Translate(Vector3 translation)
        {
            return new Matrix4x4(
                1f, 0f, 0f, translation.x,
                0f, 1f, 0f, translation.y,
                0f, 0f, 1f, translation.z,
                0f, 0f, 0f, 1f
            );
        }

        /// <summary>
        /// 创建缩放矩阵
        /// </summary>
        public static Matrix4x4 Scale(Vector3 scale)
        {
            return new Matrix4x4(
                scale.x, 0f, 0f, 0f,
                0f, scale.y, 0f, 0f,
                0f, 0f, scale.z, 0f,
                0f, 0f, 0f, 1f
            );
        }

        /// <summary>
        /// 创建旋转矩阵
        /// </summary>
        public static Matrix4x4 Rotate(Quaternion rotation)
        {
            float x2 = rotation.x + rotation.x;
            float y2 = rotation.y + rotation.y;
            float z2 = rotation.z + rotation.z;
            float xx2 = rotation.x * x2;
            float yy2 = rotation.y * y2;
            float zz2 = rotation.z * z2;
            float xy2 = rotation.x * y2;
            float yz2 = rotation.y * z2;
            float xz2 = rotation.x * z2;
            float wx2 = rotation.w * x2;
            float wy2 = rotation.w * y2;
            float wz2 = rotation.w * z2;

            return new Matrix4x4(
                1f - yy2 - zz2, xy2 - wz2, xz2 + wy2, 0f,
                xy2 + wz2, 1f - xx2 - zz2, yz2 - wx2, 0f,
                xz2 - wy2, yz2 + wx2, 1f - xx2 - yy2, 0f,
                0f, 0f, 0f, 1f
            );
        }

        /// <summary>
        /// 创建TRS矩阵（平移、旋转、缩放）
        /// </summary>
        public static Matrix4x4 TRS(Vector3 translation, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 t = Translate(translation);
            Matrix4x4 r = Rotate(rotation);
            Matrix4x4 s = Scale(scale);
            return t * r * s;
        }

        public static Vector3 operator *(Matrix4x4 m, Vector3 v)
        {
            return new Vector3(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23
            );
        }

        public static Vector4 operator *(Matrix4x4 m, Vector4 v)
        {
            return new Vector4(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03 * v.w,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13 * v.w,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23 * v.w,
                m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33 * v.w
            );
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            return new Matrix4x4(
                a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30,
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31,
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32,
                a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33,

                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,
                a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,

                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,
                a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,

                a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30,
                a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,
                a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,
                a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33
            );
        }

        public static bool operator ==(Matrix4x4 a, Matrix4x4 b)
        {
            return a.m00 == b.m00 && a.m01 == b.m01 && a.m02 == b.m02 && a.m03 == b.m03 &&
                   a.m10 == b.m10 && a.m11 == b.m11 && a.m12 == b.m12 && a.m13 == b.m13 &&
                   a.m20 == b.m20 && a.m21 == b.m21 && a.m22 == b.m22 && a.m23 == b.m23 &&
                   a.m30 == b.m30 && a.m31 == b.m31 && a.m32 == b.m32 && a.m33 == b.m33;
        }

        public static bool operator !=(Matrix4x4 a, Matrix4x4 b) => !(a == b);

        public override bool Equals(object? obj) => obj is Matrix4x4 m && Equals(m);
        public bool Equals(Matrix4x4 other) => this == other;
        public override int GetHashCode()
        {
            var h1 = HashCode.Combine(m00, m01, m02, m03);
            var h2 = HashCode.Combine(m10, m11, m12, m13);
            var h3 = HashCode.Combine(m20, m21, m22, m23);
            var h4 = HashCode.Combine(m30, m31, m32, m33);
            return HashCode.Combine(h1, h2, h3, h4);
        }
        public override string ToString() => $"Matrix4x4({m00}, {m01}, {m02}, {m03}; {m10}, {m11}, {m12}, {m13}; {m20}, {m21}, {m22}, {m23}; {m30}, {m31}, {m32}, {m33})";

        public static Matrix4x4 identity = new Matrix4x4();
        public static Matrix4x4 zero = new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    }
}
