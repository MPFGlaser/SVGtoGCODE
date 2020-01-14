using SVGtoGCODE.Models;
using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace SVGtoGCODE
{
    // inherit from vector
    public class FittedVector : Vector
    {
        private int originalHeight;
        private int originalWidth;
        private int fittedHeight;
        private int fittedWidth;
        private double scalingMultiplier;
        GCode gcode;

        // Should hold the info for the instance of Vector which has been fitted in a specific printing/work space
        public FittedVector(XmlDocument vector)
        {
            gcode = new GCode();
            Workspace workspace = new Workspace();
            fittedHeight = workspace.sizeY();
            fittedWidth = workspace.sizeX();
            GetDocumentSize(vector);
            KeepCoordsWithinBounds(vector);
        }

        private void GetDocumentSize(XmlDocument vector)
        {
            // This works!
            // https://stackoverflow.com/questions/933687/read-xml-attribute-using-xmldocument

            // Reads <svg> tag from xml file, then saves the height and width attributes.
            XmlNodeList elemList = vector.GetElementsByTagName("svg");
            for (int i = 0; i < elemList.Count; i++)
            {
                originalHeight = int.Parse(elemList[i].Attributes["height"].Value);
                originalWidth = int.Parse(elemList[i].Attributes["width"].Value);
            }

            // Calculates scaling multiplier
            scalingMultiplier = (double)fittedHeight / (double)originalHeight;
            scalingMultiplier = Math.Floor(scalingMultiplier * 100d) / 100d;

        }

        private void KeepCoordsWithinBounds(XmlDocument vector)
        {
            //List<string> cords = new List<string>();
            XmlNodeList elemList = vector.GetElementsByTagName("line");
            for (int i = 0; i < elemList.Count; i++)
            {
                int x1 = int.Parse(elemList[i].Attributes["x1"].Value);
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

                int y1 = int.Parse(elemList[i].Attributes["y1"].Value);
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

                int x2 = int.Parse(elemList[i].Attributes["x2"].Value);
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

                int y2 = int.Parse(elemList[i].Attributes["y2"].Value);
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

        private void SendToGCode(int x, int y, int z)
        {
            gcode.AddCommand(x, y, z);
        }

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


        // Checks if given coordinate is negative (<0)
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