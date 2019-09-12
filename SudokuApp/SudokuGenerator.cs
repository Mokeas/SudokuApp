using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuApp
{
    //class for generating the sudoku
    class SudokuGenerator
    {
        private Sudoku sudoku;
        private GridHandler gh;
        private Random rnd;

        public SudokuGenerator(GridHandler gh)
        {
            this.gh = gh;
            rnd = new Random();
        }

        public Sudoku GenerateNewSudoku(string difficulty)
        {
            int baseNumberToRemove = 0;

            //we change the number as we know the difficutly
            switch (difficulty)
            {
                case "easy": baseNumberToRemove = 10; break;
                case "medium": baseNumberToRemove = 20; break;
                case "hard": baseNumberToRemove = 30; break;        
            }

            sudoku = new Sudoku();
            FillSudokuWithZeros();
            sudoku.Solve(); //actually fills up the whole Sudoku


            int numberToRemove = baseNumberToRemove + rnd.Next(7);
            int row = 0;
            int column = 0;
            int stored = 0;
            while (numberToRemove != 0)
            {
                //getting random cell
                row =rnd.Next(9);
                column = rnd.Next(9);
                // if the cell is not already empty
                if (!sudoku.Grid[row, column].IsEmpty())
                {
                    stored = sudoku.Grid[row, column].Num;
                    sudoku.Grid[row, column].Num = 0;
                    //if we can solve sudoku with stored number erased 
                    if (!sudoku.TrySolve())
                    {
                        //we cant solve it now so put it back in
                        sudoku.Grid[row, column].Num = stored;
                        numberToRemove++;
                    }
                    //sudoku.Grid[row, column].Num = 0;
                    numberToRemove--;
                }
            }

            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (sudoku.Grid[i, j].Num != 0)
                        sudoku.Grid[i, j].IsChangeable = false;
                }
            }
            return sudoku;

        }

        private void FillSudokuWithZeros()
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    sudoku.Grid[i, j] = new Cell(0);
                }
            }
        }
    }
}
