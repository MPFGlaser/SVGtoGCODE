using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGtoGCODE
{
    public class Workspace
    {
        // Stores all variables for internal use.
        private int _sizeX;
        private int _sizeY;
        private int _offsetX;
        private int _offsetY;
        private int _printHeight;
        private int _moveHeight;
        private int _printSpeed;
        private int _moveSpeed;

        /// <summary>
        ///  Creates workspace based on parameters given in Settings.
        /// </summary>
        public Workspace()
        {
            UpdateParameters();
        }

        /// <summary>
        /// Updates internal parameters to the values defined in the settings box.
        /// </summary>
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


        // Properties to (publicly) access parameters of workspace when requested.
        public int SizeX => _sizeX;
        public int SizeY => _sizeY;
        public int OffsetX => _offsetX;
        public int OffsetY => _offsetY;
        public int PrintHeight => _printHeight;
        public int MoveHeight => _moveHeight;
        public int printSpeed => _printSpeed;
        public int MoveSpeed => _moveSpeed;
    }
}
