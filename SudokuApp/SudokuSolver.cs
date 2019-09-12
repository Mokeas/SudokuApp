using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.SqlServer.Server;

namespace SudokuApp
{
    //class for solving the sudoku and managing the solve
    class SudokuSolver
    {
        private Cell[,] sudoku;
        private int emptyCellsCount;
        private Random rnd;
        private int[] auxArray;
        private Queue<Position> cellsWithOneOption;
        private Position hintPosition;
        private int hintNumber;
        private bool findMeHint;

        //initializing all components
        public SudokuSolver()
        {
            sudoku = new Cell[Globals.Size, Globals.Size];
            rnd = new Random();
            findMeHint = false;
            hintNumber = -1;
            hintPosition = null;
            cellsWithOneOption = new Queue<Position>();
            // auxArray has numbers from 1 to 9 
            auxArray = new int[Globals.Size];
            for (int i = 0; i < Globals.Size; i++)
            {
                auxArray[i] = i + 1;
            }
        }

        //returning the grid with same values, but not the same reference
        private Cell[,] CopyGrid(Cell[,] sudokuGrid)
        {
            Cell[,] copy = new Cell[Globals.Size, Globals.Size];
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    Cell newCell = new Cell();
                    newCell.Num = sudokuGrid[i, j].Num;
                    newCell.IsChangeable = sudokuGrid[i, j].IsChangeable;
                    newCell.Options = CopyOptions(sudokuGrid[i, j].Options);
                    newCell.OptionsCount = sudokuGrid[i, j].OptionsCount;
                    copy[i, j] = newCell;
                }
            }

            return copy;
        }

        //returning the bool array with same values, but not the same reference
        private bool[] CopyOptions(bool[] options)
        {
            bool[] newOptions = new bool[Globals.Size];
            for (int i = 0; i < Globals.Size; i++)
            {
                newOptions[i] = options[i];
            }

            return newOptions;
        }

        // just trying solving sudoku - does NOT change it!
        // need to be implemented for Sudoku generating
        public bool TrySolve(Cell[,] sudokuGrid)
        {
            sudoku = CopyGrid(sudokuGrid);
            emptyCellsCount = GetEmptyCellsCount();
            if (!SolveLikeHuman())
            {
                if (!SolveByBacktracking())
                {
                    //Sudoku is not solvable = impossible puzzle
                    return false;
                }
            }

            return true;
        }

        // solves Sudoku, when it's possible
        public void Solve(Cell[,] sudokuGrid)
        {
            sudoku = sudokuGrid;
            emptyCellsCount = GetEmptyCellsCount();
        
            if (!SolveLikeHuman())
            {
                SolveByBacktracking();
            }
        }

        public bool TryHintNextStep(Cell[,] sudokuGrid,out int number, out Position position)
        {
            //asssigning out parameters with values 
            number = 0;
            position = new Position(0,0);

            sudoku = CopyGrid(sudokuGrid);
            emptyCellsCount = GetEmptyCellsCount();

            //next hint is not available, because the Sudoku is already solved
            if (emptyCellsCount == 0)
            {
                return false;
            }

            if (!SolveLikeHuman())
            {
                if (hintPosition == null)
                {
                    int row = 0;
                    int col = -1;
                    TryGetNextEmptyCell(ref row, ref col);
                    SolveByBacktracking();
                    //this just returns possible number, not in the order of human solving
                    //backtracking cant give us a good rational clue, but it can surely add a number
                    position.Row = row;
                    position.Col = col;
                    number = sudoku[row, col].Num;
                }
                else
                {
                    //it was correctly assigned in SolveLikeHuman, yet it cant be solved with it
                    number = hintNumber;
                    position = hintPosition;
                }
            }
            else
            {
                //hintPosition and hintNumber are set in algorithm
                position = hintPosition;
                number = hintNumber;
            }

            return true;
        }

        //return empty cells count in the Sudoku
        private int GetEmptyCellsCount()
        {
            int a = 0; 
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (sudoku[i, j].IsEmpty())
                    {
                        a++;
                    }
                }
            }
            return a;
        }

        //backtracking recursive method for solving Sudoku
        private bool SolveByBacktracking()
        {
            //start at the beginning
            int row = 0;
            int col = -1;
            int x = 0; // needs to be assigned with a value

            //if we didnt find empty cell we managed to fill the whole sudoku
            if (!TryGetNextEmptyCell(ref row, ref col))
            {
                return true;
            }

            //rnd order of numbers 1..Globals.Size in array
            //the order must be random due to the method of creating Sudoku
            //if it wasn't random, we would get identic Sudoku everytime when creating it
            int[] rndOrderedNumbers = GetRandomArrayAllValues();

            //trying every number possible in rnd order
            for (int i = 0; i < Globals.Size; i++)
            {
                x = rndOrderedNumbers[i];
                // filling sudoku with number doesnt break the constraint of sudoku
                if (IsSafeToFillCellWith(row, col, x))
                {
                    sudoku[row, col].Num = x; // fill x in the [row,col]

                    //check if x leads to solution
                    if (SolveByBacktracking())
                    {
                        return true;
                    }

                    // x didnt lead us to solution so erase it (=set number to 0)
                    SetNumberInCell(row,col,0);
                }
            }
            return false;
        }

        //returns auxArray in random order 
        private int[] GetRandomArrayAllValues()
        {
            return auxArray.OrderBy(x => rnd.Next()).ToArray();
        }

        //returns first possible number fitting to the Cell
        private bool TryChooseNumberToCell(Cell cell, out int number)
        {
            number = 0;
            while (number != Globals.Size)
            {
                if (cell.Options[number])
                {
                    number++;
                    return true;
                }
                    

                number++;
            }

            return false; //value must be returned in all paths - NEVER should occur
        }

        //alternative for going back through the grid
        //returning last previous Cell, which is not filled in the puzzle from the start (if possible)
        private bool TryGoBackToNextChangebaleCell(ref int row, ref int col)
        {
            if (!TryGoBackOneCell(ref row, ref col))
                return false;

            while (!sudoku[row, col].IsChangeable)
            {
                if (!TryGoBackOneCell(ref row, ref col))
                    return false;
            }

            return true;
        }

        //changing values of row and col to step one Cell back 
        private bool TryGoBackOneCell(ref int row, ref int col)
        {
            if (col != 0)
            {
                col--;
            }
            else
            { 
                // column = 0 so we need to go one row up
                if (row == 0)
                {
                    //first Cell of the grid
                    return false;
                }
                row--;
                col = Globals.Size - 1;
            }

            return true;
        }

        //alternative for going forward through the grid
        //returning next empty Cell, which is not filled (if possible)
        private bool TryGetNextEmptyCell(ref int row, ref int col)
        {
            while (TryGetNextCell(ref row, ref col))
            {
                if (sudoku[row, col].IsEmpty())
                {
                    return true;
                }
            }

            return false;

        }

        //changing values of row and col to step one Cell forward
        private bool TryGetNextCell(ref int row, ref int col)
        {

            if (col != Globals.Size - 1)
            {
                col++;
            }
            else
            {
                // column is indexed on number 8, so we have to go one row down
                if (row == Globals.Size - 1)
                {
                    //last Cell of the grid -> next is not possible
                    return false;
                }
                
                col = 0;
                row++;
                
            }

            return true;
        }

        //checking constraints of the Sudoku
        //returns true if it is possible to fill grid[row,col] with a number
        private bool IsSafeToFillCellWith(int row, int col, int number)
        {
            return IsSafeToFill_Row(row, number) && //not repeating in a row
                   IsSafeToFill_Column(col, number) && //not repeating in a column
                   IsSafeToFill_Subgrid(row, col, number); // not repeating in a subgrid
        }

        //checking if "number" is not in the row already
        private bool IsSafeToFill_Row(int row, int number)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (sudoku[row, i].Num == number)
                {
                    return false;
                }
            }

            return true;
        }

        //checking if "number" is not in the column already
        private bool IsSafeToFill_Column(int col, int number)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (sudoku[i, col].Num == number)
                {
                    return false;
                }
            }

            return true;
        }

        //checking if "number" is not in the subgrid already
        private bool IsSafeToFill_Subgrid(int row, int col, int number)
        {
            //topleft corner indices of the subrid the cell belong to
            int squareRow = (row / Globals.SubgridSize) * Globals.SubgridSize;
            int squareCol = (col / Globals.SubgridSize) * Globals.SubgridSize;

            for (int i = 0; i < Globals.SubgridSize; i++)
            {
                for (int j = 0; j < Globals.SubgridSize; j++)
                {
                    if (sudoku[squareRow + i, squareCol + j].Num == number)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //auxilliary Print method of readable output in Console
        private void Print()
        {
            Console.WriteLine("-----------------------------");
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size - 1; j++)
                {
                    if ((j + 1) % Globals.SubgridSize != 0)
                    {
                        Console.Write(sudoku[i, j].Num + "  ");
                    }
                    else
                    {
                        Console.Write(sudoku[i, j].Num + "  | ");
                    }
                }
                Console.WriteLine(sudoku[i, Globals.Size - 1].Num);

                if ((i + 1) % Globals.SubgridSize != 0)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("-----------------------------");
                }
            }
            Console.WriteLine();
        }

        //auxilliary Print method of readable output in Console with Options in each cell 
        private void PrintWithOptions()
        {
            Console.WriteLine("-----------------------------");
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size - 1; j++)
                {
                    if ((j + 1) % Globals.SubgridSize != 0)
                    {
                        Console.Write(sudoku[i, j].Num);
                        Console.Write("[");
                        for (int k = 0; k < Globals.Size; k++)
                        {
                            if (sudoku[i, j].Options[k])
                            {
                                Console.Write(k + 1);
                            }
                            else
                            {
                                Console.Write(".");
                            }
                            Console.Write(",");
                        }
                        Console.Write("]  ");
                    }
                    else
                    {
                        Console.Write(sudoku[i, j].Num);
                        Console.Write("[");
                        for (int k = 0; k < Globals.Size; k++)
                        {
                            if (sudoku[i, j].Options[k])
                            {
                                Console.Write(k + 1);
                            }
                            else
                            {
                                Console.Write(".");
                            }
                            Console.Write(",");
                        }
                        Console.Write("]  | ");
                    }
                }
                Console.WriteLine(sudoku[i, Globals.Size - 1].Num);

                if ((i + 1) % Globals.SubgridSize != 0)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("-----------------------------");
                }
            }
            Console.WriteLine();
        }

        // ------------------------------- HUMAN BASED ALGORITHM --------------------------------------

        // algorithm for solviing Sudoku based on human-like solution
        private bool SolveLikeHuman()
        {
            // getting all empty cells that need to be filled in
            List<Position> emptyCells = GetAllEmptyCells();

            //all empty cells must be updated 
            foreach (var cellPos in emptyCells)
            {
                //originally empty cell has 9 options of numbers that we can fill it with
                //looking to row, column, subgrid of the cell 
                //we remove options that are already impossible 
                UpdateCellOptions(cellPos.Row, cellPos.Col);

                //we get Queue of cells with one option 
                //which are cells we can already fill with right number
                if (sudoku[cellPos.Row, cellPos.Col].OptionsCount == 1)
                {
                    cellsWithOneOption.Enqueue(cellPos);
                }
            }

            //setting hint variables for hinting
            if (cellsWithOneOption.Count != 0)
            {
                hintPosition = cellsWithOneOption.Peek();
                if (TryChooseNumberToCell(sudoku[hintPosition.Row, hintPosition.Col], out int num))
                {
                    hintNumber = num;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                findMeHint = true;
            }

            //keep going if we fill at least one cell with number
            bool anyCellFilled = true;
            while (anyCellFilled)
            {
                anyCellFilled = false;

                //first - try execute whole cellsWithOneOption queue, while we can
                while (cellsWithOneOption.Count != 0)
                {
                    anyCellFilled = true;
                    if (!FillOneCellWithOneOption())
                    {
                        return false;
                    }
                }

                //there may be some cells that can be filled with the right number
                //but there are more options than one
                // e.g. only 1 cell in a row has 4 as a number, that you can fill the cell in
                // so we can fill the cell with 4, since there has to be every number in the row
                // we keep repeating this for every row, column, subgrid and for every number
                while (TryFillCellsWithAnalyzing())
                {
                    anyCellFilled = true;
                }

                //last TryFillCellWithAnalyzing can add
                //Cells that are already filled in cellsWithOneOption
                //so break when we are done already
                if (emptyCellsCount == 0)
                    break;
            }
            
            if (emptyCellsCount == 0)
            {
                //filled whole sudoku
                return true;
            }

            //multiple choice => backtracking
            //lets now say that is not possible to solve
            //this case is handled in Solve()/TrySolve() call itself

            return false;
        }

        private bool FillOneCellWithOneOption()
        {
            Position pos = cellsWithOneOption.Dequeue(); //taking row and column of cell with one option
            //one option let us fill cell with certain number
            int num = 0;
            if (TryChooseNumberToCell(sudoku[pos.Row, pos.Col], out int posNumber))
            {
                num = posNumber;
            }
            else
            {
                return false;
            }
            SetNumberInCell(pos.Row, pos.Col, num);  //setting the number in grid
            emptyCellsCount--; //one less number to fill to succeed
            UpdateGridOptionsAndAddOneOptionCells(pos.Row, pos.Col, num);
            //check how that number filled in changed the state of the grid + add cell with one number 
            return true;
        }

        //goes through grid and returns all cells that need to be filled
        private List<Position> GetAllEmptyCells()
        {
            List<Position> tmpList = new List<Position>();
            for (int i = 0; i < Globals.Size; i++)
            {
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (sudoku[i, j].IsEmpty())
                    {
                        tmpList.Add(new Position(i, j));
                    }
                }
            }

            return tmpList;
        }

        //going through the grid and filling numbers possible from Sudoku constraints
        private bool TryFillCellsWithAnalyzing()
        {
            return TryFillCellsWithAnalyzing_Rows() |
                   TryFillCellsWithAnalyzing_Columns() |
                   TryFillCellsWithAnalyzing_Subgrids();
        }

        private bool TryFillCellsWithAnalyzing_Rows()
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.Size; i++)
            {
                if (TryFillCellsWithAnalyzing_Row(i))
                {
                    cellFilled = true;
                }
            }

            return cellFilled;
        }

        private bool TryFillCellsWithAnalyzing_Row(int row)
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.Size; i++)
            {
                int numCount = 0;
                int col = 0;
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (sudoku[row, j].IsEmpty())
                    {
                        if (sudoku[row, j].Options[i])
                        {
                            numCount++;
                            col = j;
                        }
                    }
                }

                if (numCount == 1 && sudoku[row,col].Num == 0)
                {
                    emptyCellsCount--;
                    SetNumberInCell(row,col,i+1);
                    UpdateGridOptionsAndAddOneOptionCells(row,col,i+1);
                    cellFilled = true;
                }
            }

            return cellFilled;
        }

        private bool TryFillCellsWithAnalyzing_Columns()
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.Size; i++)
            {
                if (TryFillCellsWithAnalyzing_Column(i))
                {
                    cellFilled = true;
                }
            }

            return cellFilled;
        }

        private bool TryFillCellsWithAnalyzing_Column(int col)
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.Size; i++)
            {
                int numCount = 0;
                int row = 0;
                for (int j = 0; j < Globals.Size; j++)
                {
                    if (sudoku[j, col].IsEmpty())
                    {
                        if (sudoku[j, col].Options[i])
                        {
                            numCount++;
                            row = j;
                        }
                    }
                }

                if (numCount == 1 && sudoku[row, col].Num == 0)
                {
                    SetNumberInCell(row, col, i + 1);
                    emptyCellsCount--;
                    UpdateGridOptionsAndAddOneOptionCells(row, col, i + 1);
                    cellFilled = true;
                }
            }

            return cellFilled;
        }

        private bool TryFillCellsWithAnalyzing_Subgrids()
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.SubgridSize; i++)
            {
                for (int j = 0; j < Globals.SubgridSize; j++)
                {
                    if (TryFillCellsWithAnalyzing_Subgrid(i*3,j*3))
                    {
                        cellFilled = true;
                    }
                }  
            }

            return cellFilled;
        }

        private bool TryFillCellsWithAnalyzing_Subgrid(int row, int col)
        {
            bool cellFilled = false;
            for (int i = 0; i < Globals.Size; i++)
            {
                int numCount = 0;
                int fRow = 0;
                int fCol = 0;
                for (int j = 0; j < Globals.SubgridSize; j++)
                {
                    for (int k = 0; k < Globals.SubgridSize; k++)
                    {
                        if (sudoku[row+j,col+k].IsEmpty())
                        {
                            if (sudoku[row + j, col + k].Options[i])
                            {
                                numCount++;
                                fRow = row + j;
                                fCol = col + k;
                            }
                        }
                    }
                }

                if (numCount == 1 && sudoku[row, col].Num == 0)
                {
                    emptyCellsCount--;
                    SetNumberInCell(fRow, fCol, i + 1);
                    UpdateGridOptionsAndAddOneOptionCells(fRow, fCol, i + 1);
                    cellFilled = true;
                }
            }

            return cellFilled;
        }

        //updates options in each empty cell of the grid after filling number "num" in [row,column] cell
        private void UpdateGridOptionsAndAddOneOptionCells(int row, int col, int num)
        {
            //each of these methods adds new cell with one option to the queue
            UpdateGridOptions_Row(row, col, num);
            UpdateGridOptions_Column(row, col, num);
            UpdateGridOptions_Subgrid(row, col, num);
        }

        //updates options in each empty cell in a row after filling number "num" to this row
        private void UpdateGridOptions_Row(int row, int col, int num)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (i != col)
                {
                    if (sudoku[row, i].IsEmpty())
                    {
                        if (sudoku[row, i].Options[num - 1])
                        {
                            sudoku[row, i].Options[num - 1] = false;
                            sudoku[row, i].OptionsCount--;
                        }


                        if (sudoku[row, i].OptionsCount == 1)
                        {
                            if (!cellsWithOneOption.Contains(new Position(row, i)))
                            {
                                cellsWithOneOption.Enqueue(new Position(row, i));
                            }
                        }


                    }
                }
            }
        }

        //updates options in each empty cell in a column after filling number "num" to this column
        private void UpdateGridOptions_Column(int row, int col, int num)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (i != row)
                {
                    if (sudoku[i, col].IsEmpty())
                    {
                        if (sudoku[i, col].Options[num - 1])
                        {
                            sudoku[i, col].Options[num - 1] = false;
                            sudoku[i, col].OptionsCount--;
                        }


                        if (sudoku[i, col].OptionsCount == 1)
                        {
                            if (!cellsWithOneOption.Contains(new Position(i, col)))
                            {
                                cellsWithOneOption.Enqueue(new Position(i, col));
                            }
                        }


                    }
                }
            }
        }

        //updates options in each empty cell in a subgrid after filling number "num" to this subgrid
        private void UpdateGridOptions_Subgrid(int row, int col, int num)
        {
            //topleft corner indices of the subrid the cell belong to
            int squareRow = (row / Globals.SubgridSize) * Globals.SubgridSize;
            int squareCol = (col / Globals.SubgridSize) * Globals.SubgridSize;

            for (int i = 0; i < Globals.SubgridSize; i++)
            {
                for (int j = 0; j < Globals.SubgridSize; j++)
                {
                    if (squareRow + i != row || squareCol + j != col)
                    {
                        if (sudoku[squareRow + i, squareCol + j].IsEmpty())
                        {
                            if (sudoku[squareRow + i, squareCol + j].Options[num - 1])
                            {
                                sudoku[squareRow + i, squareCol + j].Options[num - 1] = false;
                                sudoku[squareRow + i, squareCol + j].OptionsCount--;
                            }


                            if (sudoku[squareRow + i, squareCol + j].OptionsCount == 1)
                            {
                                if (!cellsWithOneOption.Contains(new Position(squareRow + i, squareCol + j)))
                                {
                                    cellsWithOneOption.Enqueue(new Position(squareRow + i, squareCol + j));
                                }
                            }

                        }
                    }
                }
            }
        }

        //updates options in a certain cell
        private void UpdateCellOptions(int row, int col)
        {
            UpdateCellOptions_Row(row, col);
            UpdateCellOptions_Column(row, col);
            UpdateCellOptions_Subgrid(row, col);
        }

        //updates options of a certain cell which are influenced by the row
        private void UpdateCellOptions_Row(int row, int col)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (!sudoku[row, i].IsEmpty())
                {
                    if (sudoku[row, col].Options[sudoku[row, i].Num - 1])
                    {
                        sudoku[row, col].Options[sudoku[row, i].Num - 1] = false;
                        sudoku[row, col].OptionsCount--;
                    }
                }
            }
        }

        //updates options of a certain cell which are influenced by the column
        private void UpdateCellOptions_Column(int row, int col)
        {
            for (int i = 0; i < Globals.Size; i++)
            {
                if (!sudoku[i, col].IsEmpty())
                {
                    if (sudoku[row, col].Options[sudoku[i, col].Num - 1])
                    {
                        sudoku[row, col].Options[sudoku[i, col].Num - 1] = false;
                        sudoku[row, col].OptionsCount--;
                    }

                }
            }
        }

        //updates options of a certain cell which are influenced by the subgrid
        private void UpdateCellOptions_Subgrid(int row, int col)
        {
            //topleft corner indices of the subrid the cell belong to
            int squareRow = (row / Globals.SubgridSize) * Globals.SubgridSize;
            int squareCol = (col / Globals.SubgridSize) * Globals.SubgridSize;

            for (int i = 0; i < Globals.SubgridSize; i++)
            {
                for (int j = 0; j < Globals.SubgridSize; j++)
                {
                    if (!sudoku[squareRow + i, squareCol + j].IsEmpty())
                    {
                        if (sudoku[row, col].Options[sudoku[squareRow + i, squareCol + j].Num - 1])
                        {
                            sudoku[row, col].Options[sudoku[squareRow + i, squareCol + j].Num - 1] = false;
                            sudoku[row, col].OptionsCount--;
                        }

                    }
                }
            }
        }

        //unification of filling number in the grid if needed
        private void SetNumberInCell(int row, int col, int num)
        {
            /*
            if (sudoku[row, col].Num == 0)
            {
                emptyCellsCount--;
            }
            */
            sudoku[row, col].Num = num;

            if (findMeHint)
            {
                hintNumber = num;
                hintPosition = new Position(row,col);
                findMeHint = false;
            }
            //gh.PrintNumberInGrid(row, col, num);
        }

    }
}
