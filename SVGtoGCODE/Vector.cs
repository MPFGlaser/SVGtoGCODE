using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;

namespace SVGtoGCODE
{
    /// <summary>
    /// Takes imported Vector files and allows conversion to happen.
    /// </summary>
    public class Vector
    {
        // Variables for internal storage of data within class
        private string filePath;
        private string fileName;
        private string tempSVG;
        private BitmapImage preview;

        // Constructor
        public Vector() { }

        /// <summary>
        /// Takes path of .SVG file upon instance creation. Copies file to temporary directory and saves path.
        /// </summary>
        /// <param name="path"></param>
        public Vector(string path)
        {
            filePath = path;
            CopySVGToTempDir();
            try
            {
                CreatePreview();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the name of selected file. Handy for displaying it to the user.
        /// </summary>
        /// <returns></returns>
        public string SelectedFileName()
        {
            fileName = System.IO.Path.GetFileName(filePath);
            return fileName;
        }

        /// <summary>
        /// Returns the generated preview of the selected vector file.
        /// </summary>
        /// <returns></returns>
        public BitmapImage SendPreview()
        {
            return preview;
        }

        /// <summary>
        /// Converts the SVG into GCode
        /// </summary>
        public void Convert()
        {
            XmlDocument vector = new XmlDocument();
            vector.Load(tempSVG);
            FittedVector vectorFitted = new FittedVector(vector);
        }

        /// <summary>
        /// Creates .PNG preview based on the selected vector file. Renders the vector shapes, then draws them in the bitmap image.
        /// </summary>
        private void CreatePreview()
        {
            var svg = SvgDocument.Open(tempSVG);
            svg.ShapeRendering = SvgShapeRendering.Auto;
            Bitmap previewImage = svg.Draw();
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)previewImage).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            preview = new BitmapImage();
            preview.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            preview.StreamSource = ms;
            preview.EndInit();
        }

        /// <summary>
        /// Copies the selected vector file to temp directory to prevent possible damage to the original file during transformation.
        /// </summary>
        private void CopySVGToTempDir()
        {
            tempSVG = System.IO.Path.GetTempFileName();
            try
            {
                File.Copy(filePath, tempSVG, true);
            }
            catch (Exception)
            {
                // TO DO: Exception handling
                throw;
            }
        }
    }
}