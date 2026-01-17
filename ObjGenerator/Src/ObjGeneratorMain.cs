using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MathLibrary;

/// <summary>
/// OBJ file generator for creating and exporting mesh geometries.
/// </summary>
class ObjGenerator
{
    /// <summary>
    /// Represents 3D mesh geometry with vertices, normals, colors, and texture coordinates.
    /// </summary>
    class Mesh
    {
        public Vector3[]? vertices;
        public Vector3[]? normals;
        public Vector2[]? uvs0;
        public Vector2[]? uvs1;
        public int[]? triangles;

#pragma warning disable CS0649 // Unused fields reserved for future use
        public Vector3[]? tangents;
        public Vector4[]? colors;
#pragma warning restore CS0649
    }

    /// <summary>
    /// Provides factory methods for creating standard geometric meshes.
    /// </summary>
    class MeshLibrary
    {
        /// <summary>Predefined cube mesh.</summary>
        public static Mesh Cube;

        static MeshLibrary()
        {
            Cube = new Mesh();
            Cube.vertices = new Vector3[8]
            {
                new Vector3(-1, -1, -1),
                new Vector3(1, -1, -1),
                new Vector3(1, 1, -1),
                new Vector3(-1, 1, -1),
                new Vector3(-1, -1, 1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, 1),
                new Vector3(-1, 1, 1)
            };

            // Maps control points to vertex indices for each face
            var controlPointToVertexId = new Dictionary<int, int>
            {
                // Face 1 (Back)
                { 0, 3 }, { 1, 2 }, { 2, 1 }, { 3, 0 },
                // Face 2 (Left)
                { 4, 4 }, { 5, 7 }, { 6, 3 }, { 7, 0 },
                // Face 3 (Bottom)
                { 8, 0 }, { 9, 1 }, { 10, 5 }, { 11, 4 },
                // Face 4 (Top)
                { 12, 7 }, { 13, 6 }, { 14, 2 }, { 15, 3 },
                // Face 5 (Right)
                { 16, 6 }, { 17, 5 }, { 18, 1 }, { 19, 2 },
                // Face 6 (Front)
                { 20, 4 }, { 21, 5 }, { 22, 6 }, { 23, 7 }
            };

            // Initialize triangle array
            Cube.triangles = new int[36]
            {
                0,1,2,0,2,3,
                4,5,6,4,6,7,
                8,9,10,8,10,11,
                12,13,14,12,14,15,
                16,17,18,16,18,19,
                20,21,22,20,22,23
            };

            // Remap control points to actual vertex indices
            for (int i = 0; i < Cube.triangles.Length; i++)
            {
                Cube.triangles[i] = controlPointToVertexId[Cube.triangles[i]];
            }
        }

        /// <summary>
        /// Creates a single grass blade mesh with specified segment count and width.
        /// </summary>
        /// <param name="segmentCount">Number of segments along the blade length.</param>
        /// <param name="width">Width of the grass blade.</param>
        public static Mesh CreateSingleGrassMesh(int segmentCount, float width)
        {
            if (segmentCount < 1 || width <= 0)
                throw new ArgumentException("segmentCount must be >= 1 and width must be > 0");

            var mesh = new Mesh();
            float segmentLength = 1.0f / segmentCount;
            float halfWidth = width * 0.5f;

            // Create blade vertices (two rows for width)
            int vertexCount = segmentCount * 2 + 1;
            mesh.vertices = new Vector3[vertexCount];
            
            for (int i = 0; i < segmentCount; i++)
            {
                float zPos = segmentLength * i;
                mesh.vertices[i * 2] = new Vector3(-halfWidth, 0, zPos);
                mesh.vertices[i * 2 + 1] = new Vector3(halfWidth, 0, zPos);
            }
            mesh.vertices[vertexCount - 1] = new Vector3(0, 0, 1.0f);

            // Create triangles
            int triangleCount = ((segmentCount - 1) * 2 + 1) * 3;
            mesh.triangles = new int[triangleCount];
            for (int i = 0; i < segmentCount - 1; i++)
            {
                mesh.triangles[i * 2 * 3] = i * 2;
                mesh.triangles[i * 2 * 3 + 1] = i * 2 + 1;
                mesh.triangles[i * 2 * 3 + 2] = (i + 1) * 2;

                mesh.triangles[(i * 2 + 1) * 3] = i * 2 + 1;
                mesh.triangles[(i * 2 + 1) * 3 + 1] = (i + 1) * 2 + 1;
                mesh.triangles[(i * 2 + 1) * 3 + 2] = (i + 1) * 2;
            }
            mesh.triangles[(segmentCount - 1) * 2 * 3] = (segmentCount - 1) * 2;
            mesh.triangles[(segmentCount - 1) * 2 * 3 + 1] = (segmentCount - 1) * 2 + 1;
            mesh.triangles[(segmentCount - 1) * 2 * 3 + 2] = segmentCount * 2;

            return mesh;
        }

        /// <summary>
        /// Creates a UV sphere mesh with specified radius and segment count.
        /// </summary>
        /// <param name="radius">Radius of the sphere.</param>
        /// <param name="widthSegments">Number of horizontal segments (longitude).</param>
        /// <param name="heightSegments">Number of vertical segments (latitude).</param>
        public static Mesh CreateSphere(float radius = 1f, int widthSegments = 32, int heightSegments = 16)
        {
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            // Generate sphere vertices using spherical coordinates
            for (int y = 0; y <= heightSegments; y++)
            {
                float vPercent = y / (float)heightSegments;
                float vRad = Mathf.PI * vPercent;
                float sinV = Mathf.Sin(vRad);
                float cosV = Mathf.Cos(vRad);

                for (int x = 0; x <= widthSegments; x++)
                {
                    float uPercent = x / (float)widthSegments;
                    float uRad = Mathf.Tau * uPercent;

                    float xPos = radius * sinV * Mathf.Cos(uRad);
                    float yPos = radius * cosV;
                    float zPos = radius * sinV * Mathf.Sin(uRad);

                    vertices.Add(new Vector3(xPos, yPos, zPos));
                }
            }

            // Generate triangle indices for sphere faces
            for (int y = 0; y < heightSegments; y++)
            {
                for (int x = 0; x < widthSegments; x++)
                {
                    int a = y * (widthSegments + 1) + x;
                    int b = a + 1;
                    int c = a + (widthSegments + 1);
                    int d = c + 1;

                    triangles.Add(a);
                    triangles.Add(c);
                    triangles.Add(b);

                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(d);
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        /// <summary>
        /// 创建圆柱体网格
        /// Creates a cylindrical mesh with specified radius, height and segment count.
        /// </summary>
        public static Mesh CreateCylinder(float radius = 1f, float height = 2f, int segments = 32)
        {
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            float halfHeight = height * 0.5f;

            // Add top circle vertices
            for (int i = 0; i <= segments; i++)
            {
                float angle = (i / (float)segments) * Mathf.Tau;
                vertices.Add(new Vector3(radius * Mathf.Cos(angle), halfHeight, radius * Mathf.Sin(angle)));
            }

            // Add bottom circle vertices
            for (int i = 0; i <= segments; i++)
            {
                float angle = (i / (float)segments) * Mathf.Tau;
                vertices.Add(new Vector3(radius * Mathf.Cos(angle), -halfHeight, radius * Mathf.Sin(angle)));
            }

            // Add center vertices for caps
            int topCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, halfHeight, 0));

            int bottomCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, -halfHeight, 0));

            // Generate side faces
            for (int i = 0; i < segments; i++)
            {
                int topA = i;
                int topB = i + 1;
                int botA = segments + 1 + i;
                int botB = segments + 1 + i + 1;

                // Two triangles per quad
                triangles.Add(topA);
                triangles.Add(botA);
                triangles.Add(topB);

                triangles.Add(topB);
                triangles.Add(botA);
                triangles.Add(botB);
            }

            // Generate top cap
            for (int i = 0; i < segments; i++)
            {
                triangles.Add(topCenterIdx);
                triangles.Add(i + 1);
                triangles.Add(i);
            }

            // Generate bottom cap
            for (int i = 0; i < segments; i++)
            {
                triangles.Add(bottomCenterIdx);
                triangles.Add(segments + 1 + i);
                triangles.Add(segments + 1 + i + 1);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        /// <summary>
        /// 创建圆锥体网格
        /// Creates a conical mesh with specified radius, height and segment count.
        /// </summary>
        public static Mesh CreateCone(float radius = 1f, float height = 2f, int segments = 32)
        {
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            float halfHeight = height * 0.5f;

            // Add apex vertex
            int topIdx = vertices.Count;
            vertices.Add(new Vector3(0, halfHeight, 0));

            // Add base circle vertices
            for (int i = 0; i <= segments; i++)
            {
                float angle = (i / (float)segments) * Mathf.Tau;
                vertices.Add(new Vector3(radius * Mathf.Cos(angle), -halfHeight, radius * Mathf.Sin(angle)));
            }

            // Add base center vertex
            int bottomCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, -halfHeight, 0));

            // Generate side faces
            for (int i = 0; i < segments; i++)
            {
                int botA = 1 + i;
                int botB = 1 + i + 1;

                triangles.Add(topIdx);
                triangles.Add(botB);
                triangles.Add(botA);
            }

            // Generate bottom cap
            for (int i = 0; i < segments; i++)
            {
                triangles.Add(bottomCenterIdx);
                triangles.Add(1 + i);
                triangles.Add(1 + i + 1);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        /// <summary>
        /// 创建平面网格
        /// Creates a planar mesh with specified dimensions and segment count.
        /// </summary>
        public static Mesh CreatePlane(float width = 2f, float height = 2f, int widthSegments = 1, int heightSegments = 1)
        {
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            float halfWidth = width * 0.5f;
            float halfHeight = height * 0.5f;

            // Generate vertices
            for (int y = 0; y <= heightSegments; y++)
            {
                float yPercent = y / (float)heightSegments;
                float yPos = halfHeight - yPercent * height;

                for (int x = 0; x <= widthSegments; x++)
                {
                    float xPercent = x / (float)widthSegments;
                    float xPos = -halfWidth + xPercent * width;

                    vertices.Add(new Vector3(xPos, 0, yPos));
                }
            }

            // Generate triangle indices
            for (int y = 0; y < heightSegments; y++)
            {
                for (int x = 0; x < widthSegments; x++)
                {
                    int a = y * (widthSegments + 1) + x;
                    int b = a + 1;
                    int c = a + (widthSegments + 1);
                    int d = c + 1;

                    triangles.Add(a);
                    triangles.Add(c);
                    triangles.Add(b);

                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(d);
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        /// <summary>
        /// 创建四棱锥网格
        /// Creates a square pyramid mesh with specified base size and height.
        /// </summary>
        public static Mesh CreatePyramid(float baseSize = 2f, float height = 2f)
        {
            var mesh = new Mesh();
            float halfSize = baseSize * 0.5f;
            float halfHeight = height * 0.5f;

            // Create vertices: 4 base corners + 1 apex
            var vertices = new Vector3[5]
            {
                new Vector3(-halfSize, -halfHeight, -halfSize),  // Base corner 0
                new Vector3(halfSize, -halfHeight, -halfSize),   // Base corner 1
                new Vector3(halfSize, -halfHeight, halfSize),    // Base corner 2
                new Vector3(-halfSize, -halfHeight, halfSize),   // Base corner 3
                new Vector3(0, halfHeight, 0)                    // Apex
            };

            // Create triangles: 2 for base + 4 sides = 18 indices (6 triangles)
            var triangles = new int[18]
            {
                // Base (bottom)
                0, 2, 1,
                0, 3, 2,
                
                // Side faces (apex connected to base edges)
                0, 4, 1,  // Front
                1, 4, 2,  // Right
                2, 4, 3,  // Back
                3, 4, 0   // Left
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            return mesh;
        }

        /// <summary>
        /// 创建立方体网格（简化版本）
        /// Creates a cube mesh with specified size.
        /// Note: Uses 24 vertices (6 per face) to support per-face normals and UV mapping.
        /// </summary>
        public static Mesh CreateCube(float size = 2f)
        {
            var mesh = new Mesh();
            float hs = size * 0.5f;  // halfSize

            // Generate 24 vertices (6 per face for independent UV/normal mapping)
            var vertices = new Vector3[24]
            {
                // Front face (Z+)
                new Vector3(-hs, -hs, hs), new Vector3(hs, -hs, hs), new Vector3(hs, hs, hs), new Vector3(-hs, hs, hs),
                // Back face (Z-)
                new Vector3(-hs, -hs, -hs), new Vector3(-hs, hs, -hs), new Vector3(hs, hs, -hs), new Vector3(hs, -hs, -hs),
                // Top face (Y+)
                new Vector3(-hs, hs, -hs), new Vector3(-hs, hs, hs), new Vector3(hs, hs, hs), new Vector3(hs, hs, -hs),
                // Bottom face (Y-)
                new Vector3(-hs, -hs, -hs), new Vector3(hs, -hs, -hs), new Vector3(hs, -hs, hs), new Vector3(-hs, -hs, hs),
                // Right face (X+)
                new Vector3(hs, -hs, -hs), new Vector3(hs, hs, -hs), new Vector3(hs, hs, hs), new Vector3(hs, -hs, hs),
                // Left face (X-)
                new Vector3(-hs, -hs, -hs), new Vector3(-hs, -hs, hs), new Vector3(-hs, hs, hs), new Vector3(-hs, hs, -hs)
            };

            // Generate 36 triangle indices (6 faces * 6 indices per face)
            var triangles = new int[36]
            {
                // Front
                0, 2, 1, 0, 3, 2,
                // Back
                4, 6, 5, 4, 7, 6,
                // Top
                8, 10, 9, 8, 11, 10,
                // Bottom
                12, 14, 13, 12, 15, 14,
                // Right
                16, 18, 17, 16, 19, 18,
                // Left
                20, 22, 21, 20, 23, 22
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            return mesh;
        }
    }

    /// <summary>
    /// Provides utility methods for mesh manipulation and export.
    /// </summary>
    class MeshTools
    {
        /// <summary>
        /// Creates an instanced copy of a mesh at specified positions with optional rotations.
        /// </summary>
        /// <param name="points">Array of positions to place mesh copies.</param>
        /// <param name="mesh">Base mesh to copy.</param>
        /// <param name="rotations">Optional per-instance rotations. If provided, must match points.Length.</param>
        /// <returns>Combined mesh with all instances.</returns>
        public static Mesh CopyMeshToPoints(Vector3[] points, Mesh mesh, Quaternion[]? rotations = null)
        {
            var outMesh = new Mesh();

            // Validate rotation array if provided
            if (rotations != null && rotations.Length != points.Length)
            {
                throw new ArgumentException("Rotation array length must match points array length if provided.");
            }

            // Allocate output arrays
            if (mesh.vertices != null)
                outMesh.vertices = new Vector3[mesh.vertices.Length * points.Length];
            if (mesh.normals != null)
                outMesh.normals = new Vector3[mesh.normals.Length * points.Length];
            if (mesh.uvs0 != null)
                outMesh.uvs0 = new Vector2[mesh.uvs0.Length * points.Length];
            if (mesh.uvs1 != null)
                outMesh.uvs1 = new Vector2[mesh.uvs1.Length * points.Length];
            if (mesh.triangles != null)
                outMesh.triangles = new int[mesh.triangles.Length * points.Length];

            // Copy mesh data for each instance
            int vertexStride = mesh.vertices?.Length ?? 0;
            
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 position = points[i];
                Quaternion rotation = rotations?[i] ?? Quaternion.identity;
                int vertexOffset = i * vertexStride;

                // Copy and transform vertices
                if (mesh.vertices != null && outMesh.vertices != null)
                {
                    for (int j = 0; j < mesh.vertices.Length; j++)
                    {
                        Vector3 vertex = mesh.vertices[j];
                        if (rotations != null)
                            vertex = rotation.RotateVector(vertex);
                        outMesh.vertices[vertexOffset + j] = vertex + position;
                    }
                }

                // Copy normals (no translation, rotation only)
                if (mesh.normals != null && outMesh.normals != null)
                {
                    for (int j = 0; j < mesh.normals.Length; j++)
                    {
                        Vector3 normal = mesh.normals[j];
                        if (rotations != null)
                            normal = rotation.RotateVector(normal);
                        outMesh.normals[vertexOffset + j] = normal;
                    }
                }

                // Copy UVs (no transformation needed)
                if (mesh.uvs0 != null && outMesh.uvs0 != null)
                {
                    for (int j = 0; j < mesh.uvs0.Length; j++)
                        outMesh.uvs0[vertexOffset + j] = mesh.uvs0[j];
                }

                // Copy UVs1 (no transformation needed)
                if (mesh.uvs1 != null && outMesh.uvs1 != null)
                {
                    for (int j = 0; j < mesh.uvs1.Length; j++)
                        outMesh.uvs1[vertexOffset + j] = mesh.uvs1[j];
                }

                // Copy triangles with offset
                if (mesh.triangles != null && outMesh.triangles != null)
                {
                    for (int j = 0; j < mesh.triangles.Length; j++)
                        outMesh.triangles[i * mesh.triangles.Length + j] = mesh.triangles[j] + vertexOffset;
                }
            }

            return outMesh;
        }

        /// <summary>
        /// Exports a mesh to OBJ file format.
        /// </summary>
        /// <param name="mesh">Mesh to export.</param>
        /// <param name="filePath">Output file path.</param>
        public static void WriteMeshToObj(Mesh mesh, string filePath)
        {
            if (mesh == null)
                throw new ArgumentNullException(nameof(mesh));

            using (var writer = new StreamWriter(filePath))
            {
                var sb = new StringBuilder();
                writer.WriteLine("# Generated by ObjLibrary");
                
                if (mesh != null)
                {
                    // Write vertices
                    if (mesh.vertices != null)
                    {
                        foreach (Vector3 v in mesh.vertices)
                            sb.AppendFormat("v {0} {1} {2}\n", v.x, v.y, v.z);
                        sb.AppendLine();
                    }

                    // Write normals
                    if (mesh.normals != null)
                    {
                        foreach (Vector3 n in mesh.normals)
                            sb.AppendFormat("vn {0} {1} {2}\n", n.x, n.y, n.z);
                        sb.AppendLine();
                    }

                    // Write texture coordinates
                    if (mesh.uvs0 != null)
                    {
                        foreach (Vector2 uv in mesh.uvs0)
                            sb.AppendFormat("vt {0} {1}\n", uv.x, uv.y);
                        sb.AppendLine();
                    }

                    // Write faces
                    if (mesh.triangles != null)
                    {
                        bool hasNormals = mesh.normals != null;
                        bool hasUVs = mesh.uvs0 != null;

                        for (int i = 0; i < mesh.triangles.Length; i += 3)
                        {
                            // OBJ format uses 1-based indices
                            int v0 = mesh.triangles[i] + 1;
                            int v1 = mesh.triangles[i + 1] + 1;
                            int v2 = mesh.triangles[i + 2] + 1;

                            if (hasUVs && hasNormals)
                                sb.AppendFormat("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", v0, v1, v2);
                            else if (hasUVs)
                                sb.AppendFormat("f {0}/{0} {1}/{1} {2}/{2}\n", v0, v1, v2);
                            else if (hasNormals)
                                sb.AppendFormat("f {0}//{0} {1}//{1} {2}//{2}\n", v0, v1, v2);
                            else
                                sb.AppendFormat("f {0} {1} {2}\n", v0, v1, v2);
                        }
                        sb.AppendLine();
                    }
                }

                writer.Write(sb.ToString());
            }
        }
    }

    /// <summary>
    /// Main entry point for ObjGenerator demonstrations.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("========== OBJ Library Mesh Generator ==========");
                Console.WriteLine();

                // Create a grass blade mesh
                Console.WriteLine("Creating grass blade mesh...");
                var singleGrass = MeshLibrary.CreateSingleGrassMesh(segmentCount: 7, width: 0.04f);

                // Generate random positions for 25 grass instances
                Console.WriteLine("Generating 25 grass instances with random rotation...");
                var instancePoints = new Vector3[25];
                var instanceRotations = new Quaternion[25];

                for (int i = 0; i < 25; i++)
                {
                    instancePoints[i] = new Vector3(
                        RandomUtil.Range(-1f, 1f),
                        RandomUtil.Range(-1f, 1f),
                        0f
                    );
                    
                    // Random rotation around Z-axis (0-360 degrees)
                    float angle = RandomUtil.Range(0f, 360f);
                    instanceRotations[i] = Quaternion.AxisAngle(new Vector3(0, 0, 1), angle);
                }

                // Merge all instances into a single mesh
                Console.WriteLine("Merging instances into single mesh...");
                var mergedMesh = MeshTools.CopyMeshToPoints(instancePoints, singleGrass, instanceRotations);

                // Export to OBJ file
                string outputPath = "mergedGrass.obj";
                Console.WriteLine($"Writing mesh to {outputPath}...");
                MeshTools.WriteMeshToObj(mergedMesh, outputPath);

                Console.WriteLine();
                Console.WriteLine("========== Success ==========");
                Console.WriteLine($"Exported mesh with {mergedMesh.vertices?.Length ?? 0} vertices and {(mergedMesh.triangles?.Length ?? 0) / 3} triangles");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("========== Error ==========");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
