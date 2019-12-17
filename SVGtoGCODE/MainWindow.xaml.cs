using System;
using System.Drawing;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Svg;
using System.Xml;

namespace SVGtoGCODE
{
    public partial class MainWindow : Window
    {
        Vector vector = new Vector();
        public MainWindow()
        {
            InitializeComponent();
            TextBlockStatus.Text = "Please select your file...";
        }

        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                FileName = "artwork.svg",
                Filter = "Vector graphics (*.svg)|*.svg",
                Title = "Select file",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                // not implemented in WPF
                //RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        vector.Setup(System.IO.Path.GetFullPath(openFileDialog.FileName));
                        DisplayController("status", "Selected file:\n" + vector.SelectedFileName());
                        PreviewController();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Info: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Unknown error occurred. Please try again later.");
            }
        }

        private void PreviewController()
        {
            PreviewImage.Source = vector.SendPreview();
        }

        private void DisplayController(string type, string message)
        {
            if (type == "error")
            {
                TextBlockStatus.Text = "ERROR!\n" + message;
            }
            if (type == "status")
            {
                TextBlockStatus.Text = message;
            }
        }
    }

    // Class for working with (imported) vector files
    public class Vector
    {
        // Should have:
        // Check aspect ratio/rotation on import, pad if necessary.
        // X Function to create temporary copy
        // X Path to temporary copy of file
        // X generate random file name + be able to have it be requested publicly
        // X Name (+ generator?)

        private string filePath;
        private string fileName;
        private string tempSVG;
        private BitmapImage previewImage;
        private Bitmap preview;

        public Vector() { }

        public void Setup(string path)
        {
            filePath = path;
            CopySVGToTempDir();
            VectorPreview();
            //Convert(preview);
        }

        public string SelectedFileName()
        {
            fileName = System.IO.Path.GetFileName(filePath);
            return fileName;
        }

        public Bitmap VectorPreview()
        {
            var svg = SvgDocument.Open(tempSVG);
            svg.ShapeRendering = SvgShapeRendering.Auto;
            preview = svg.Draw();
            Convert(preview);
            return preview;
        }

        private BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            previewImage = new BitmapImage();
            previewImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            previewImage.StreamSource = ms;
            previewImage.EndInit();
            return previewImage;
        }

        public BitmapImage SendPreview()
        {
            return previewImage;
        }

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
