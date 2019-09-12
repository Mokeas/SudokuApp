using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SudokuApp
{
    public partial class Form1 : Form
    {
        private AppHandler appHandler;
        private int hours, minutes, seconds;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            appHandler = new AppHandler();
            appHandler.InitNewAppHandler(sudokuGrid);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // setting DataGridView to look like a Sudoku grid
        private void InitializeGrid()
        {
            //setting grid visual properties
            sudokuGrid.RowHeadersVisible = false;
            sudokuGrid.ScrollBars = ScrollBars.None;
            sudokuGrid.ColumnHeadersVisible = false;
            sudokuGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            sudokuGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            //sudokuGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            sudokuGrid.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            
            //resctricting user to change the grid except the content
            sudokuGrid.AllowUserToResizeRows = false;
            sudokuGrid.AllowUserToAddRows = false;
            sudokuGrid.AllowUserToDeleteRows = false;
            sudokuGrid.AllowUserToOrderColumns = false;
            sudokuGrid.AllowUserToResizeColumns = false;

            //setting grid size
            sudokuGrid.ColumnCount = Globals.Size;
            sudokuGrid.Rows.Add(Globals.Size);
            sudokuGrid.Height = Globals.GridSize;
            sudokuGrid.Width = Globals.GridSize;

            for (int i = 0; i < Globals.Size; i++)
            {
                sudokuGrid.Columns[i].Width = 40 + ((i + 1) % 3 == 0 ? 3 : 0);
                sudokuGrid.Rows[i].Height = 40 + ((i + 1) % 3 == 0 ? 3 : 0);
                //disable scrolling
                sudokuGrid.Rows[i].Frozen = true;
                sudokuGrid.Columns[i].Frozen = true;
            }
            
            //formatting grid's dividing lines
            sudokuGrid.Columns[2].DividerWidth = 3;
            sudokuGrid.Columns[5].DividerWidth = 3;
            sudokuGrid.Rows[2].DividerHeight = 3;
            sudokuGrid.Rows[5].DividerHeight = 3;
            
            //setting other properties of the grid
            sudokuGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sudokuGrid.GridColor = Color.Black;
            sudokuGrid.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            sudokuGrid[0, 0].Selected = false;
        }

        private void buttonLoadSudoku_Click(object sender, EventArgs e)
        {
            ResetStopwatch();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                TextReader reader = new StreamReader(filePath);
                appHandler.GetNewSudokuFromFile(reader);
                reader.Dispose();
            }
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            appHandler.SolveSudoku();
            ResetStopwatch();
        }

        private void ResetStopwatch()
        {
            timer1.Enabled = false;
            hours = 0;
            minutes = 0;
            seconds = 0;
            ShowTime();
        }

        private void buttonStartSolving_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            IncreaseSeconds();
            ShowTime();
        }

        private void IncreaseMinutes()
        {
            if (minutes == 59)
            {
                minutes = 0;
                hours++;
            }
            else
            {
                minutes++;
            }
        }

        private void sudokuGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void sudokuGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (sudokuGrid[e.ColumnIndex, e.RowIndex].Value != null)
            {
                if (Int32.TryParse(sudokuGrid[e.ColumnIndex, e.RowIndex].Value.ToString(), out int val))
                {
                    if (val <= 0 || val >= 10)
                    {
                        sudokuGrid[e.ColumnIndex, e.RowIndex].Value = "";
                    }
                }
                else
                {
                    sudokuGrid[e.ColumnIndex, e.RowIndex].Value = "";
                }
            }
            else
            {
                sudokuGrid[e.ColumnIndex, e.RowIndex].Value = "";
            }
            
        }


        private void buttonCheck_Click(object sender, EventArgs e)
        {
            string result="";
            if (appHandler.IsSolutionCorrect())
            {
                result = String.Format("Congratulations! You solved this Sudoku in {0} hours {1} minutes {2} seconds", hours, minutes, seconds);
                timer1.Enabled = false;
            }
            else
            {
                result = "Unlucky! This is not a correct solution to this puzzle...";
            }

            MessageBox.Show(result);
        }

        private void buttonGenerateSudoku_Click(object sender, EventArgs e)
        {

            string difficutly = "easy";

            if (radioButtonMedium.Checked)
            {
                difficutly = "medium";
            }

            if (radioButtonHard.Checked )
            {
                difficutly = "hard";
            }

            appHandler.GenerateSudoku(difficutly);
            ResetStopwatch();
        }

        private void buttonHint_Click(object sender, EventArgs e)
        {
            appHandler.HintNextStep();
        }

        private void IncreaseSeconds()
        {
            if (seconds == 59)
            {
                seconds = 0;
                IncreaseMinutes();
            }
            else
            {
                seconds++;
            }
        }

        private void ShowTime()
        {
            labelHours.Text = hours.ToString("00");
            labelMinutes.Text = minutes.ToString("00");
            labelSeconds.Text = seconds.ToString("00");
        }
    }
}
