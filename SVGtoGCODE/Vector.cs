using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace SVGtoGCODE
{
    // Class for working with imported vector files
    public class Vector
    {
        /* TO DO
           - Check aspect ratio/rotation on import, pad if necessary.
           - Extract & convert vector shapes to GCode
        */

        // Variables for internal storage of data within class
        private string filePath;
        private string fileName;
        private string tempSVG;
        private BitmapImage preview;
        private List<string> coordinates = new List<string>();

        // Constructor
        public Vector() { }

        // Constructor. Passes the right parameters to class on image selection/instance creation
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

        // Gets the name of selected file. Handy for displaying it to the user.
        public string SelectedFileName()
        {
            fileName = System.IO.Path.GetFileName(filePath);
            return fileName;
        }

        // Returns the generated preview of the selected vector file.
        public BitmapImage SendPreview()
        {
            return preview;
        }

        // Converts the SVG into GCode
        public void Convert()
        {
            // TO DO
        }

        // Creates .PNG preview based on the selected vector file. Renders the vector shapes, then draws them in the bitmap image.
        private void CreatePreview()
        {
            var svg = SvgDocument.Open(tempSVG);
            svg.ShapeRendering = SvgShapeRendering.Auto;

            // try using this to fix stuff :)
            //svg.Transforms.Add(new Svg.Transforms.SvgRotate(90.0f));

            Bitmap previewImage = svg.Draw();
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)previewImage).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            preview = new BitmapImage();
            preview.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            preview.StreamSource = ms;
            preview.EndInit();
        }

        // Copies the selected vector file to temp directory to prevent possible damage to the original file during transformation.
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