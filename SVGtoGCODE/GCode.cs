using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddCommand(int x, int y, int z, int f)
        {
            GCodeCommands.Add("G1" + " X" + x.ToString() + " Y" + y.ToString() + " Z" + z.ToString() + " F" + f.ToString());
        }

        public void SaveGCode()
        {
            // Should write list of commands to txt file.
            // https://stackoverflow.com/questions/15300572/saving-lists-to-txt-file
        }
    }
}
