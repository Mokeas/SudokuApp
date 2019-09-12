using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp
{
    //simple class to store row and column index as one
    class Position
    {
        public int Row;
        public int Col;

        public Position(int row, int column)
        {
            Row = row;
            Col = column;
        }

        public override bool Equals(object obj)
        {
            Position pos = obj as Position;
            return (this.Row == pos.Row && this.Col == pos.Col);

        }

        public override string ToString()
        {
            return "[" + Row + "," + Col + "]";
        }
    }
}
