using System;
using System.IO;
using System.Windows.Forms;

namespace SudokuApp
{
    //class for handling the correct working of the app
    //holding all neccessary data, calling right methods with neccessary arguments
    class AppHandler
    {
        public GridHandler gridHandler { get; set; }
        public SudokuPrinter printer;
        public SudokuParser parser;
        public Sudoku sudoku;
        private SudokuGenerator generator;

        //printer of the Sudoku and gridHandler doesn't change in the process of SudokuApp running
        public void InitNewAppHandler(DataGridView dataGridView)
        {
            gridHandler = new GridHandler(dataGridView);
            printer = new SudokuPrinter(gridHandler);
        }

        //parsing, printing and setting new Sudoku for the AppHandler and user
        public void GetNewSudokuFromFile(TextReader reader)
        {
            gridHandler.ClearGrid();
            parser = new SudokuParser(reader);
            if (parser.TryGetSudoku(out Sudoku newSudoku))
            {
                sudoku = newSudoku;
                printer.PrintSudokuInGrid(sudoku);
            }
            else
            {
                ShowMessageBoxFileFail();
            }
            
        }

        //handling correct calling of SudokuSolver and actually solving it if possible
        public void SolveSudoku()
        {
            if (sudoku.TrySolve())
            {
                sudoku.Solve(); //Solve changes sudoku
                printer.PrintSudokuInGrid(sudoku); //printer shows sudoku to the user

            }
            else
            {
                ShowMessageBoxImpossibleSudoku(); // not possible to solve
            }
            
        }

        //returns true if solution in the grid is correct
        public bool IsSolutionCorrect()
        { 
            //checking if sudoku is solvable and also solving it 
            if (sudoku.TrySolve())
            {
                sudoku.Solve();
                //printer.PrintSudokuInGrid(sudoku);
            }
            else
            {
                ShowMessageBoxImpossibleSudoku();
            }

            //sudoku is internal and solved - comparing sudoku with the grid of user
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (IsDifferent(gridHandler.Grid[j,i].Value, sudoku.Grid[i,j].Num))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //comparing Value of DataGridViewCell with a number 
        private bool IsDifferent(object o, int correctVal)
        {
            if (o != null)
            {
                if (Int32.TryParse(o.ToString(), out int val))
                {
                    if (val == correctVal)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //calling and handling sudoku generator
        public void GenerateSudoku(string difficulty)
        {
            gridHandler.ClearGrid();
            generator = new SudokuGenerator(gridHandler);
            sudoku = generator.GenerateNewSudoku(difficulty);
            printer.PrintSudokuInGrid(sudoku);
        }

        //returns next possible step to solve the Sudoku
        public void HintNextStep()
        {
            if (sudoku.TrySolve())
            {
                if (sudoku.TryHintNextStep(out int number, out Position position))
                {
                    sudoku.Grid[position.Row, position.Col].Num = number;
                    //sudoku.Grid[position.Row, position.Col].IsChangeable = false;
                    gridHandler.PrintAndHighlightGridCell(position.Row, position.Col, number);    
                }
                else
                {
                    //no next step available - sudoku is filled
                    ShowMessageBoxSolvedSudoku();
                }
            }
            else
            {
                //no next step available - sudoku is impossible
                ShowMessageBoxImpossibleSudoku();
            }
        }

        //method for showing user that the inputed Sudoku is impossible to solve
        private void ShowMessageBoxImpossibleSudoku()
        {
            MessageBox.Show("This Sudoku is impossible to solve");
        }

        //method for showing user that the Sudoku is fully filled
        private void ShowMessageBoxSolvedSudoku()
        {
            MessageBox.Show("This Sudoku is already solved");
        }

        //method for showing user that the Sudoku file was loaded incorrectly
        private void ShowMessageBoxFileFail()
        {
            MessageBox.Show("File error!");
        }
    }
}
