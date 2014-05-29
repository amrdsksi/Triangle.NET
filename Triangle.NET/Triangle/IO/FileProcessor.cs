﻿
namespace TriangleNet.IO
{
    using System;
    using System.Collections.Generic;
    using TriangleNet.Geometry;
    using TriangleNet.Meshing;

    public static class FileProcessor
    {
        static List<IFileFormat> formats = new List<IFileFormat>();

        public static void Add(IFileFormat format)
        {
            formats.Add(format);
        }

        public static bool IsSupported(string file)
        {
            foreach (var format in formats)
            {
                if (format.IsSupported(file))
                {
                    return true;
                }
            }

            return false;
        }

        #region Polygon read/write

        /// <summary>
        /// Read a file containing polygon geometry.
        /// </summary>
        /// <param name="filename">The path of the file to read.</param>
        /// <returns>An instance of the <see cref="IPolygon" /> class.</returns>
        public static InputGeometry Read(string filename)
        {
            foreach (IPolygonFormat format in formats)
            {
                if (format != null && format.IsSupported(filename))
                {
                    return format.Read(filename);
                }
            }

            throw new Exception("File format not supported.");
        }

        /// <summary>
        /// Save a polygon geometry to disk.
        /// </summary>
        /// <param name="mesh">An instance of the <see cref="IPolygon" /> class.</param>
        /// <param name="filename">The path of the file to save.</param>
        public static void Write(InputGeometry polygon, string filename)
        {
            foreach (IPolygonFormat format in formats)
            {
                if (format != null && format.IsSupported(filename))
                {
                    format.Write(polygon, filename);
                }
            }

            throw new Exception("File format not supported.");
        }

        #endregion

        #region Mesh read/write

        /// <summary>
        /// Read a file containing a mesh.
        /// </summary>
        /// <param name="filename">The path of the file to read.</param>
        /// <returns>An instance of the <see cref="IMesh" /> interface.</returns>
        public static Mesh Import(string filename)
        {
            foreach (IMeshFormat format in formats)
            {
                if (format != null && format.IsSupported(filename))
                {
                    return format.Import(filename);
                }
            }

            throw new Exception("File format not supported.");
        }

        /// <summary>
        /// Save a mesh to disk.
        /// </summary>
        /// <param name="mesh">An instance of the <see cref="IMesh" /> interface.</param>
        /// <param name="filename">The path of the file to save.</param>
        public static void Write(Mesh mesh, string filename)
        {
            foreach (IMeshFormat format in formats)
            {
                if (format != null && format.IsSupported(filename))
                {
                    format.Write(mesh, filename);
                }
            }

            throw new Exception("File format not supported.");
        }

        #endregion
    }
}