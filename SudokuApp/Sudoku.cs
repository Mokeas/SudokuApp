using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp
{
    class Sudoku
    {
        public Cell[,] Grid;

        public Sudoku()
        {
            Grid = new Cell[Globals.Size, Globals.Size];
        }

        public bool TrySolve()
        {
            SudokuSolver solver = new SudokuSolver();
            return solver.TrySolve(Grid);
        }

        public void Solve()
        {
            SudokuSolver solver = new SudokuSolver();
            solver.Solve(Grid);
        }

        public bool TryHintNextStep(out int number, out Position position)
        {
            SudokuSolver solver = new SudokuSolver();
            //values need to be assigned before exiting function
            number = 0;
            position = new Position(0,0);
            if (solver.TryHintNextStep(Grid,out int num, out Position pos))
            {
                number = num;
                position = pos;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
