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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        TextBlockStatus.Text = "Selected file: " + System.IO.Path.GetFileName(openFileDialog.FileName);
                        //TextBlockStatus.Text = System.IO.Path.GetTempPath();


                        //file1.Text = "File loaded.";

                        //string path = openFile.FileName;
                        //var svgDocument = Svg.SvgDocument.Open(path);
                        //svgDocument.ShapeRendering = SvgShapeRendering.Auto;

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
    }

    // Class for working with (imported) vector files
    public class Vector
    {
        // Should have:
        // Check aspect ratio/rotation on import, pad if necessary.
        // Function to create temporary copy
        // Path to temporary copy of file
        // generate random file name + be able to have it be requested publicly
        // Name (+ generator?)


        public Vector()
        {

        }

        private string GenerateFileName()
        {
            var uniqueFileName = System.IO.Path.GetRandomFileName();
            return uniqueFileName;
        }
    }
}
