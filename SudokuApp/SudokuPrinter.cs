using System;

namespace SudokuApp
{
    class SudokuPrinter
    {
        private GridHandler gh;
        
        public SudokuPrinter(GridHandler gh)
        {
            this.gh = gh;
        }

        // w must be initialized TextWriter to use the PrintSudoku method
        /*
        public void PrintSudoku(Sudoku sudoku)
        {
            w.WriteLine();
            w.WriteLine("-----------------------------");
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size - 1; j++)
                {
                    if ((j + 1) % Globals.SubgridSize != 0)
                    {
                        w.Write(sudoku.Grid[i, j].Num + "  ");
                    }
                    else
                    {
                        w.Write(sudoku.Grid[i, j].Num + "  | ");
                    }
                }
                w.WriteLine(sudoku.Grid[i, Globals.Size - 1].Num);

                if ((i + 1) % Globals.SubgridSize != 0)
                {
                    w.WriteLine();
                }
                else
                {
                    w.WriteLine("-----------------------------");
                }
            }
            w.WriteLine();
        }
        */

        public void PrintSudokuInGrid(Sudoku sudoku)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (!sudoku.Grid[i, j].IsChangeable)
                    {
                        gh.PrintNonChangableNumberInGrid(i, j, sudoku.Grid[i, j].Num);
                    }
                    else    
                    {
                        gh.PrintNumberInGrid(i,j,sudoku.Grid[i,j].Num);
                    }
                }
            }

            gh.Grid[0, 0].Selected = false;
        }
    }
}
