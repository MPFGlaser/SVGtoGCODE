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
using System.Windows.Navigation;

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
                Title = "Select file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        TextBlockStatus.Text = "Selected file: " + Path.GetFileName(openFileDialog.FileName);
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

    }
}
