using System;
using System.IO;
using System.Windows.Forms;

namespace SudokuApp
{
    class SudokuParser 
    {
        private TextReader r;  

        public SudokuParser(TextReader reader)
        {
            this.r = reader;
        }
        
        public bool TryGetSudoku(out Sudoku newSudoku)
        {
            newSudoku = new Sudoku();
            string[] tokens;
            string line;

            for (int i = 0; i < Globals.Size; i++)
            {
                if ((line = r.ReadLine()) != null)
                {
                    tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length != Globals.Size)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = 0; j < Globals.Size; j++)
                        {
                            newSudoku.Grid[i, j]= new Cell(Convert.ToInt32(tokens[j]));
                        }
                    }
                }
                else
                {
                    return false;
                }

            }

            return true;
        }
    }
}
