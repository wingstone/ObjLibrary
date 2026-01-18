using System;
using GeometryTools;

namespace ModelIOTools
{
    /// <summary>
    /// Handles reading and writing FBX file format for mesh data.
    /// Note: This is a placeholder for FBX support. Full FBX implementation would require
    /// native libraries or third-party dependencies.
    /// </summary>
    public class FbxFileHandler
    {
        /// <summary>
        /// Exports a mesh to FBX file format.
        /// </summary>
        /// <param name="mesh">Mesh to export.</param>
        /// <param name="filePath">Output file path.</param>
        public static void WriteMesh(Mesh mesh, string filePath)
        {
            throw new NotImplementedException(
                "FBX export is not yet implemented. Consider using ObjFileHandler or implementing FBX support with a third-party library.");
        }

        /// <summary>
        /// Reads a mesh from FBX file format.
        /// </summary>
        /// <param name="filePath">Path to the FBX file.</param>
        /// <returns>Mesh loaded from the file.</returns>
        public static Mesh ReadMesh(string filePath)
        {
            throw new NotImplementedException(
                "FBX import is not yet implemented. Consider using ObjFileHandler or implementing FBX support with a third-party library.");
        }
    }
}
