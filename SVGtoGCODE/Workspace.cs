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

        }

        public Workspace(int sizeX, int sizeY, int offsetX, int offsetY, int printHeight, int moveHeight, int printSpeed, int moveSpeed)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _printHeight = printHeight;
            _moveHeight = moveHeight;
            _printSpeed = printSpeed;
            _moveSpeed = moveSpeed;
        }
    }
}
