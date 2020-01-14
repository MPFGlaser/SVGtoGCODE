using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGtoGCODE.Models;

namespace SVGtoGCODE
{
    public class GCode
    {
        List<string> GCodeCommands;
        // Is able to convert instance of FittedVector into GCode that complies with the restrictions given in instance of Workspace.
        // Should be able to generate, save, and export GCode.
        public GCode()
        {
            GCodeCommands = new List<string>();
        }

        public void AddCommand(int x, int y, int z)
        {
            GCodeCommands.Add("G1" + " X" + x.ToString() + " Y" + y.ToString() + " Z" + z.ToString());
        }

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

        public void SaveGCode()
        {
            // Should write list of commands to txt file.
            // https://stackoverflow.com/questions/15300572/saving-lists-to-txt-file
            TextWriter writer = new StreamWriter("D:\\GCode\\save1.GCODE");

            foreach (String s in GCodeCommands)
                writer.WriteLine(s);

            writer.Close();
        }
    }
}
