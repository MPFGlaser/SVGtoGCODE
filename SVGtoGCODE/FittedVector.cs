using SVGtoGCODE.Models;
using System;
using System.Xml;


namespace SVGtoGCODE
{
    /// <summary>
    /// Takes .SVG from Vector instance and applies transformations to ensure it fits within given parameters in ConversionSettings.
    /// </summary>
    public class FittedVector : Vector
    {
        private int originalHeight;
        private int originalWidth;
        private int fittedHeight;
        private int fittedWidth;
        private double scalingMultiplier;
        GCode gcode;

        /// <summary>
        /// Constructor for FittedVector. Takes vector from Vector class.
        /// </summary>
        /// <param name="vector"></param>
        // Imports max dimensions from workspace instance, then fetches size of document and makes adjustments accordingly.
        public FittedVector(XmlDocument vector)
        {
            gcode = new GCode();
            Workspace workspace = new Workspace();
            fittedHeight = workspace.SizeY;
            fittedWidth = workspace.SizeX;
            GetDocumentSize(vector);
            KeepCoordsWithinBounds(vector);
        }

        /// <summary>
        /// Fetches document size from vector. Then calculates scalingMultiplier.
        /// </summary>
        /// <param name="vector"></param>
        private void GetDocumentSize(XmlDocument vector)
        {
            // Reads <svg> tag from xml file, then saves the height and width attributes.
            XmlNodeList elemList = vector.GetElementsByTagName("svg");
            for (int i = 0; i < elemList.Count; i++)
            {
                double heightToParse = double.Parse(elemList[i].Attributes["height"].Value);
                originalHeight = (int)Math.Round(heightToParse, MidpointRounding.AwayFromZero);
                double widthToParse = double.Parse(elemList[i].Attributes["width"].Value);
                originalWidth = (int)Math.Round(widthToParse, MidpointRounding.AwayFromZero);
            }

            // Calculates scaling multiplier
            scalingMultiplier = (double)fittedHeight / (double)originalHeight;
            scalingMultiplier = Math.Floor(scalingMultiplier * 100d) / 100d;
        }

        /// <summary>
        /// Takes all lines from vector, then applies maths to make sure they fit within the given boundaries.
        /// </summary>
        /// <param name="vector"></param>
        private void KeepCoordsWithinBounds(XmlDocument vector)
        {
            // Makes a list of all <line> elements in vector, then applies scaling maths.
            XmlNodeList elemList = vector.GetElementsByTagName("line");
            for (int i = 0; i < elemList.Count; i++)
            {
                // Maths part for coordinate x1
                double x1ToParse = double.Parse(elemList[i].Attributes["x1"].Value);
                int x1 = (int)Math.Round(x1ToParse, MidpointRounding.AwayFromZero);
                if (!IsNegative(x1))
                {
                    if (x1 > originalWidth)
                    {
                        x1 = (int)(originalWidth * scalingMultiplier) + Properties.Settings.Default.OffsetX;

                    }
                    else
                    {
                        x1 = (int)(x1 * scalingMultiplier) + Properties.Settings.Default.OffsetX;
                    }
                }
                else
                {
                    x1 = 0 + Properties.Settings.Default.OffsetX;
                }

                // Maths part for coordinate y1
                double y1ToParse = double.Parse(elemList[i].Attributes["y1"].Value);
                int y1 = (int)Math.Round(y1ToParse, MidpointRounding.AwayFromZero);
                if (!IsNegative(y1))
                {
                    if (y1 > originalHeight)
                    {
                        y1 = (int)(originalHeight * scalingMultiplier) + Properties.Settings.Default.OffsetY;

                    }
                    else
                    {
                        y1 = (int)(y1 * scalingMultiplier) + Properties.Settings.Default.OffsetY;
                    }
                }
                else
                {
                    y1 = 0 + Properties.Settings.Default.OffsetY;
                }

                // Moves the print head to the right X/Y position while maintaining height and speed on the first run to prevent unwanted marks.
                if (i == 0)
                {
                    SendToGCode(x1, y1, Properties.Settings.Default.MoveHeight, MovementModes.Move);
                }

                // Moves printhead to start of line, then lowers to draw.
                SendToGCode(x1, y1, Properties.Settings.Default.MoveHeight, MovementModes.Print);
                SendToGCode(x1, y1, Properties.Settings.Default.PrintHeight, MovementModes.Print);

                // Maths part for coordinate x2
                double x2ToParse = double.Parse(elemList[i].Attributes["x2"].Value);
                int x2 = (int)Math.Round(x2ToParse, MidpointRounding.AwayFromZero);
                if (!IsNegative(x2))
                {
                    if (x2 > originalWidth)
                    {
                        x2 = (int)(originalWidth * scalingMultiplier) + Properties.Settings.Default.OffsetX;

                    }
                    else
                    {
                        x2 = (int)(x2 * scalingMultiplier) + Properties.Settings.Default.OffsetX;
                    }
                }
                else
                {
                    x2 = 0 + Properties.Settings.Default.OffsetX;
                }

                // Maths part for coordinate y2
                double y2ToParse = double.Parse(elemList[i].Attributes["y2"].Value);
                int y2 = (int)Math.Round(y2ToParse, MidpointRounding.AwayFromZero);
                if (!IsNegative(y2))
                {
                    if (y2 > originalHeight)
                    {
                        y2 = (int)(originalHeight * scalingMultiplier) + Properties.Settings.Default.OffsetY;

                    }
                    else
                    {
                        y2 = (int)(y2 * scalingMultiplier) + Properties.Settings.Default.OffsetY;
                    }
                }
                else
                {
                    y2 = 0 + Properties.Settings.Default.OffsetY;
                }

                // Moves to second point of line, then lifts up printhead.
                SendToGCode(x2, y2, Properties.Settings.Default.PrintHeight, MovementModes.Print);
                SendToGCode(x2, y2, Properties.Settings.Default.MoveHeight, MovementModes.Print);


                // Lifts the printhead straight up after completing the last command to prevent unwanted marks when returning to home position.
                if (i == elemList.Count - 1)
                {
                    SendToGCode(x2, y2, Properties.Settings.Default.MoveHeight, MovementModes.Move);
                    gcode.SaveGCode();
                }
            }
        }

        /// <summary>
        /// Sends specified coordinates to GCode instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void SendToGCode(int x, int y, int z)
        {
            gcode.AddCommand(x, y, z);
        }

        /// <summary>
        /// Sends specified coordinates and movement mode to GCode instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="movementMode"></param>
        private void SendToGCode(int x, int y, int z, MovementModes movementMode)
        {
            switch (movementMode)
            {
                case MovementModes.Print:
                    gcode.AddCommand(x, y, z, MovementModes.Print);
                    break;
                case MovementModes.Move:
                    gcode.AddCommand(x, y, z, MovementModes.Move);
                    break;
            }
        }

        /// <summary>
        /// Checks if given number is negative, then returns bool.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        private bool IsNegative(int coordinate)
        {
            if (coordinate < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}