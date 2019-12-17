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

        // Handles the select button.
        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;

            // Opens file selection dialog with the user's documents folder as default. File input is restricted to .svg files.
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                FileName = "artwork.svg",
                Filter = "Vector graphics (*.svg)|*.svg",
                Title = "Select file",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            // Tries to read file (if selected) and passes it to the vector object. 
            //Then makes sure the user knows what file is selected by showing the file name and a preview.
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        vector.Setup(System.IO.Path.GetFullPath(openFileDialog.FileName));
                        DisplayController("Selected file:\n" + vector.SelectedFileName());
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
                MessageBox.Show("Unknown error occurred. Please try again later.");
            }
        }

        // Handles the preview window. Gets the preview from the current vector object.
        private void PreviewController()
        {
            PreviewImage.Source = vector.SendPreview();
        }

        // Handles all text to be shown to the user. Takes optional type argument as well as the message to display.
        private void DisplayController(string message)
        {
            TextBlockStatus.Text = message;
        }
        private void DisplayController(string type, string message)
        {
            TextBlockStatus.Text = type + "!\n" + message;
        }
    }
}

// Class for working with imported vector files
public class Vector
{
    // Should have:
    // Check aspect ratio/rotation on import, pad if necessary.
    // X Function to create temporary copy
    // X Path to temporary copy of file
    // X generate random file name + be able to have it be requested publicly
    // X Name (+ generator?)


    // Variables for internal storage of data within class
    private string filePath;
    private string fileName;
    private string tempSVG;
    private BitmapImage preview;

    // Constructor, empty because class is created on programme startup
    public Vector() { }

    // Passes the right parameters to class on image selection
    public void Setup(string path)
    {
        filePath = path;
        CopySVGToTempDir();
        CreatePreview();
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

    // Creates .PNG preview based on the selected vector file. Renders the vector shapes, then draws them in the bitmap image.
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
