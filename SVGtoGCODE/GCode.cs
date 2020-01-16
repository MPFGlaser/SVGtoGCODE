using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using SVGtoGCODE.Models;

namespace SVGtoGCODE
{
    public class GCode
    {
        List<string> GCodeCommands;

        /// <summary>
        /// Takes FittedVector and converts given coordinates to GCode.
        /// </summary>
        public GCode()
        {
            GCodeCommands = new List<string>();
        }

        /// <summary>
        /// Adds a GCode command to the list using the given X, Y, and Z coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddCommand(int x, int y, int z)
        {
            GCodeCommands.Add("G1" + " X" + x.ToString() + " Y" + y.ToString() + " Z" + z.ToString());
        }

        /// <summary>
        /// Adds a GCode command to the list using the given X, Y, and Z coordinates, as well as a speed based on the movement mode.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="movementMode"></param>
        public void AddCommand(int x, int y, int z, MovementModes movementMode)
        {
            switch (movementMode)
            {
                case MovementModes.Print:
                    GCodeCommands.Add("G1" + " X" + x.ToString() + " Y" + y.ToString() + " Z" + z.ToString() + " F" + Properties.Settings.Default.PrintSpeed);
                    break;
                case MovementModes.Move:
                    GCodeCommands.Add("G1" + " X" + x.ToString() + " Y" + y.ToString() + " Z" + z.ToString() + " F" + Properties.Settings.Default.MoveSpeed);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Saves list of GCode to file. Location of file is determined by user using SaveFileDialog.
        /// </summary>
        public void SaveGCode()
        {
            // Opens file selection dialog with the user's documents folder as default. File input is restricted to .svg files.
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = "result.GCODE",
                Filter = "GCODE (*.GCODE)|*.GCODE",
                Title = "Save GCode",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            // Tries to read file (if selected) and passes it to the vector object. 
            //Then makes sure the user knows what file is selected by showing the file name and a preview.
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    TextWriter writer = new StreamWriter(saveFileDialog.FileName);

                    foreach (String s in GCodeCommands)
                        writer.WriteLine(s);

                    writer.Close();
                }
                catch (Exception ex)
                {
                    //MainWindow.SetStatusText("ERROR", $"Error: Could not read file from disk. Info: {ex.Message}");
                    MessageBox.Show("Errror: Could not save file to disk. Info");
                }
            }
            else
            {
                //Mainwindow.SetStatusText("ERROR", "Unknown error occurred. Please try again later.");
            }

        }
    }
}
