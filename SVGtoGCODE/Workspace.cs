using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGtoGCODE
{
    public class Workspace
    {
        private int _sizeX;
        private int _sizeY;
        private int _offsetX;
        private int _offsetY;
        private int _printHeight;
        private int _moveHeight;
        private int _printSpeed;
        private int _moveSpeed;

        // Holds the information about the defined workspace, such as the height and width dimensions.
        public Workspace()
        {
            UpdateParameters();
        }

        // Updates the parameters/values based on the user-specified settings from the Conversion Settings dialog.
        public void UpdateParameters()
        {
            _sizeX = Properties.Settings.Default.SizeX;
            _sizeY = Properties.Settings.Default.SizeY;
            _offsetX = Properties.Settings.Default.OffsetX;
            _offsetY = Properties.Settings.Default.OffsetY;
            _printHeight = Properties.Settings.Default.PrintHeight;
            _moveHeight = Properties.Settings.Default.MoveHeight;
            _printSpeed = Properties.Settings.Default.PrintSpeed;
            _moveSpeed = Properties.Settings.Default.MoveSpeed;
        }


        // Functions to return parameters of workspace when requested.
        public int sizeX() { return _sizeX; }
        public int sizeY() { return _sizeY; }
        public int offsetX() { return _offsetX; }
        public int offsetY() { return _offsetY; }
        public int printHeight() { return _printHeight; }
        public int moveHeight() { return _moveHeight; }
        public int printSpeed() { return _printSpeed; }
        public int moveSpeed() { return _moveSpeed; }
    }
}
