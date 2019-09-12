using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuApp
{
    //global constants to make sudoku easily resizable in the future
    static class Globals
    {
        public const int Size = 9; 

        public const int GridSize = 369;

        public static int SubgridSize = Convert.ToInt32(Math.Sqrt(Size));

        public static Color CellColor = Color.WhiteSmoke;

        public static Color NonChangableCellColor = Color.LightGray;
    }
}
