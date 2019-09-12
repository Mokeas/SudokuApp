using System;

namespace SudokuApp
{
    //cell representation of Sudoku cell
    class Cell 
    {
        public int Num;

        public bool[] Options;

        public int OptionsCount;

        public bool IsChangeable;

        //initializing Cell with a certain number and properties
        public Cell(int n)
        {
            Num = n;
            if (n == 0)
            {
                Options = new bool[Globals.Size];
                for (int i = 0; i < Globals.Size; i++)
                {
                    Options[i] = true;
                }
                OptionsCount = Globals.Size;
                IsChangeable = true;
            }
            else
            {
                //setting flags for nonempty cell
                IsChangeable = false;
                Options = new bool[Globals.Size];
                OptionsCount = 0;
            }

            //Value = n;
            
        }

        
        public Cell()
        {
            //we need nonparametric constructor to init custom properties
        }
        

        public bool IsEmpty()
        {
            return Num == 0;
        }

        public override string ToString()
        {
            string ret = "Num: ";
            ret = ret + this.Num + ", Poss:(";
            for (int i = 0; i < Globals.Size; i++)
            {
                if (Options[i] == true)
                {
                    ret = ret + (i + 1) + ",";
                }
            }

            ret = ret + "), PossCount: ";
            ret = ret + OptionsCount;
            return ret;
        }

        public void SetNumber(int n)
        {
            this.Num = n;
        }

        public void ResetCell()
        {
            Num = 0;
            for (int i = 0; i < Globals.Size; i++)
            {
                Options[i] = true;
            }
            OptionsCount = Globals.Size;
        }
    }
}
