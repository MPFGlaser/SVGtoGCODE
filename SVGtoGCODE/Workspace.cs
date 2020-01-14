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
    }
}
