using System;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace SVGtoGCODE
{
    public partial class MainWindow : Window
    {
        Vector vector;
        public MainWindow()
        {
            InitializeComponent();
            SetStatusText("Please select your file...");
        }

        // Handles the select button.
        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            SelectSVG();
        }

        private void SelectSVG()
        {
            Stream checkStream;

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

                        vector = new Vector(System.IO.Path.GetFullPath(openFileDialog.FileName));
                        SetStatusText("Selected file:\n" + vector.SelectedFileName());
                        UpdatePreview();
                    }
                }
                catch (Exception ex)
                {
                    string error = "Error: Could not read file from disk. Info: " + ex.Message;
                    SetStatusText("ERROR", error);
                }
            }
            else
            {
                string error = "Unknown error occurred. Please try again later.";
                SetStatusText("ERROR", error);
            }
        }

        // Handles the convert button
        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            vector.Convert();
        }

        // Handles the preview window. Gets the preview from the current vector object.
        private void UpdatePreview()
        {
            PreviewImage.Source = vector.SendPreview();
        }

        // Handles all text to be shown to the user. Takes optional type argument as well as the message to display.
        private void SetStatusText(string message)
        {
            TextBlockStatus.Text = message;
        }

        private void SetStatusText(string type, string message)
        {
            TextBlockStatus.Text = type + "!\n" + message;
        }
    }
}