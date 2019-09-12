using System.Drawing;
using System.Windows.Forms;

namespace SudokuApp
{
    //handling of DataGridView, which is for visualizing the Sudoku
    class GridHandler
    {
        public DataGridView Grid;

        public GridHandler(DataGridView dataGridView)
        {
            Grid = dataGridView;
        }
        
        //showing the number in grid for user
        public void PrintNumberInGrid(int row, int column, int number)
        {
            if (number != 0)
            {
                Grid[column, row].Value = number;
                Grid[column, row].ReadOnly = true;
                Grid[column, row].Style.BackColor = Color.White;
            }         
        }

        public void PrintNonChangableNumberInGrid(int row, int column, int number)
        {
            Grid[column, row].Value = number;
            Grid[column, row].ReadOnly = true;
            Grid[column, row].Style.BackColor = Globals.NonChangableCellColor;
        }
        
        //highlighting for x seconds and printing at the same time 
        public void PrintAndHighlightGridCell(int row, int column, int number)
        {
            Grid[column, row].Value = number;     
            Grid[column, row].Style.BackColor = Color.DeepPink;
            Grid.Refresh();
            System.Threading.Thread.Sleep(1500);
            
            if (number != 0)
            {
                Grid[column, row].ReadOnly = true;
                Grid[column, row].Style.BackColor = Globals.CellColor;
            }
            else
            {
                Grid[column, row].Style.BackColor = Globals.CellColor;
            }
            
        }

        //removing all valueas from grid
        public void ClearGrid()
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    Grid[i, j].Value = "";
                    Grid[i, j].Style.BackColor = Globals.CellColor;
                    Grid[i, j].ReadOnly = false;
                }
            }
        }
    }
}
