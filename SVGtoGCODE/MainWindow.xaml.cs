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


// Declare vector instance globally, then initialise later. Constructor should work then.


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
            Stream checkStream;

            // Opens file selection dialog with the user's documents folder as default. File input is restricted to .svg files.

            // MOVE TO SEPARATE METHOD!!!!! 
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
                    string error = "Error: Could not read file from disk. Info: " + ex.Message;
                    DisplayController("ERROR", error);
                }
            }
            else
            {
                string error = "Unknown error occurred. Please try again later.";
                DisplayController("ERROR", error);
            }
        }

        // Handles the convert button
        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            vector.Convert();
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

    // Constructor, empty because class is created on programme startup
    public Vector() { }

    // Passes the right parameters to class on image selection
    public void Setup(string path)
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

    public class Workspace
    {
        // Holds the information about the defined workspace, such as the height and width dimensions.
    }


    // inherit from vector
    public class FittedVector
    {
        // Should hold the info for the instance of Vector which has been fitted in a specific printing/work space
    }

    public class GCode
    {
        // Is able to convert instance of FittedVector into GCode that complies with the restrictions given in instance of Workspace.
        // Should be able to generate, save, and export GCode.
    }
}

