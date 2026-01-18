using System;

namespace MathLibrary
{
    /// <summary>
    /// 表示3D变换，包含位置、旋转和缩放
    /// </summary>
    public struct Transform : IEquatable<Transform>
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public Transform(Vector3 position = default, Quaternion rotation = default, Vector3 scale = default)
        {
            this.position = position;
            // 如果旋转为默认值，使用单位四元数
            if (rotation.SqrMagnitude == 0)
                this.rotation = Quaternion.identity;
            else
                this.rotation = rotation.Normalized;
            
            // 如果缩放为默认值，使用 (1,1,1)
            if (scale.SqrMagnitude == 0)
                this.scale = Vector3.one;
            else
                this.scale = scale;
        }

        /// <summary>
        /// 获取单位变换
        /// </summary>
        public static Transform identity => new Transform(Vector3.zero, Quaternion.identity, Vector3.one);

        /// <summary>
        /// 获取前向向量（旋转应用于Z轴负方向）
        /// </summary>
        public Vector3 Forward => rotation.RotateVector(new Vector3(0, 0, -1));

        /// <summary>
        /// 获取右向向量（旋转应用于X轴正方向）
        /// </summary>
        public Vector3 Right => rotation.RotateVector(new Vector3(1, 0, 0));

        /// <summary>
        /// 获取上向向量（旋转应用于Y轴正方向）
        /// </summary>
        public Vector3 Up => rotation.RotateVector(new Vector3(0, 1, 0));

        /// <summary>
        /// 获取变换矩阵
        /// </summary>
        public Matrix4x4 Matrix => Matrix4x4.TRS(position, rotation, scale);

        /// <summary>
        /// 应用变换到点
        /// </summary>
        public Vector3 TransformPoint(Vector3 point)
        {
            // 缩放
            Vector3 scaled = new Vector3(
                point.x * scale.x,
                point.y * scale.y,
                point.z * scale.z
            );
            // 旋转
            Vector3 rotated = rotation.RotateVector(scaled);
            // 平移
            return rotated + position;
        }

        /// <summary>
        /// 应用变换到方向向量（不包括平移）
        /// </summary>
        public Vector3 TransformDirection(Vector3 direction)
        {
            // 缩放
            Vector3 scaled = new Vector3(
                direction.x * scale.x,
                direction.y * scale.y,
                direction.z * scale.z
            );
            // 旋转
            return rotation.RotateVector(scaled);
        }

        /// <summary>
        /// 应用变换的逆变换到点
        /// </summary>
        public Vector3 InverseTransformPoint(Vector3 point)
        {
            // 逆平移
            Vector3 translated = point - position;
            // 逆旋转
            Vector3 rotated = rotation.Inverse.RotateVector(translated);
            // 逆缩放
            return new Vector3(
                rotated.x / scale.x,
                rotated.y / scale.y,
                rotated.z / scale.z
            );
        }

        /// <summary>
        /// 应用变换的逆变换到方向向量
        /// </summary>
        public Vector3 InverseTransformDirection(Vector3 direction)
        {
            // 逆旋转
            Vector3 rotated = rotation.Inverse.RotateVector(direction);
            // 逆缩放
            return new Vector3(
                rotated.x / scale.x,
                rotated.y / scale.y,
                rotated.z / scale.z
            );
        }

        /// <summary>
        /// 组合两个变换
        /// </summary>
        public static Transform Combine(Transform parent, Transform child)
        {
            Vector3 combinedPosition = parent.TransformPoint(child.position);
            Quaternion combinedRotation = parent.rotation * child.rotation;
            Vector3 combinedScale = new Vector3(
                parent.scale.x * child.scale.x,
                parent.scale.y * child.scale.y,
                parent.scale.z * child.scale.z
            );
            return new Transform(combinedPosition, combinedRotation, combinedScale);
        }

        /// <summary>
        /// 获取变换的逆变换
        /// </summary>
        public Transform Inverse
        {
            get
            {
                Quaternion invRotation = rotation.Inverse;
                Vector3 invScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
                Vector3 invPosition = invRotation.RotateVector(new Vector3(-position.x * invScale.x, -position.y * invScale.y, -position.z * invScale.z));
                return new Transform(invPosition, invRotation, invScale);
            }
        }

        public static Transform operator *(Transform a, Transform b) => Combine(a, b);

        public override bool Equals(object? obj) => obj is Transform transform && Equals(transform);

        public bool Equals(Transform other) =>
            position.Equals(other.position) &&
            rotation.Equals(other.rotation) &&
            scale.Equals(other.scale);

        public override int GetHashCode() => HashCode.Combine(position, rotation, scale);

        public override string ToString() => $"Transform(Position: {position}, Rotation: {rotation}, Scale: {scale})";

        public static bool operator ==(Transform lhs, Transform rhs) => lhs.Equals(rhs);
        public static bool operator !=(Transform lhs, Transform rhs) => !lhs.Equals(rhs);
    }
}
