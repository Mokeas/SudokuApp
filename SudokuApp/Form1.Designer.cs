namespace SudokuApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sudokuGrid = new System.Windows.Forms.DataGridView();
            this.buttonLoadSudoku = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.buttonStartSolving = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelHours = new System.Windows.Forms.Label();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelSeconds = new System.Windows.Forms.Label();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonGenerateSudoku = new System.Windows.Forms.Button();
            this.radioButtonEasy = new System.Windows.Forms.RadioButton();
            this.radioButtonMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonHard = new System.Windows.Forms.RadioButton();
            this.buttonHint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sudokuGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // sudokuGrid
            // 
            this.sudokuGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sudokuGrid.Location = new System.Drawing.Point(499, 29);
            this.sudokuGrid.Name = "sudokuGrid";
            this.sudokuGrid.RowTemplate.Height = 28;
            this.sudokuGrid.Size = new System.Drawing.Size(450, 450);
            this.sudokuGrid.TabIndex = 0;
            this.sudokuGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sudokuGrid_CellClick);
            this.sudokuGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.sudokuGrid_CellEndEdit);
            // 
            // buttonLoadSudoku
            // 
            this.buttonLoadSudoku.BackColor = System.Drawing.Color.LightCoral;
            this.buttonLoadSudoku.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonLoadSudoku.ForeColor = System.Drawing.Color.Black;
            this.buttonLoadSudoku.Location = new System.Drawing.Point(71, 29);
            this.buttonLoadSudoku.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLoadSudoku.Name = "buttonLoadSudoku";
            this.buttonLoadSudoku.Size = new System.Drawing.Size(350, 50);
            this.buttonLoadSudoku.TabIndex = 1;
            this.buttonLoadSudoku.Text = "Load Sudoku";
            this.buttonLoadSudoku.UseVisualStyleBackColor = false;
            this.buttonLoadSudoku.Click += new System.EventHandler(this.buttonLoadSudoku_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.BackColor = System.Drawing.Color.LightCoral;
            this.buttonSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSolve.ForeColor = System.Drawing.Color.Black;
            this.buttonSolve.Location = new System.Drawing.Point(71, 265);
            this.buttonSolve.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(350, 50);
            this.buttonSolve.TabIndex = 2;
            this.buttonSolve.Text = "Solve Sudoku by PC";
            this.buttonSolve.UseVisualStyleBackColor = false;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // buttonStartSolving
            // 
            this.buttonStartSolving.BackColor = System.Drawing.Color.LightCoral;
            this.buttonStartSolving.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStartSolving.ForeColor = System.Drawing.Color.Black;
            this.buttonStartSolving.Location = new System.Drawing.Point(71, 352);
            this.buttonStartSolving.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStartSolving.Name = "buttonStartSolving";
            this.buttonStartSolving.Size = new System.Drawing.Size(350, 50);
            this.buttonStartSolving.TabIndex = 3;
            this.buttonStartSolving.Text = "Start Solving Sudoku";
            this.buttonStartSolving.UseVisualStyleBackColor = false;
            this.buttonStartSolving.Click += new System.EventHandler(this.buttonStartSolving_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelHours.Location = new System.Drawing.Point(143, 420);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(55, 37);
            this.labelHours.TabIndex = 4;
            this.labelHours.Text = "00";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMinutes.Location = new System.Drawing.Point(224, 420);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(55, 37);
            this.labelMinutes.TabIndex = 5;
            this.labelMinutes.Text = "00";
            // 
            // labelSeconds
            // 
            this.labelSeconds.AutoSize = true;
            this.labelSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSeconds.Location = new System.Drawing.Point(305, 420);
            this.labelSeconds.Name = "labelSeconds";
            this.labelSeconds.Size = new System.Drawing.Size(55, 37);
            this.labelSeconds.TabIndex = 6;
            this.labelSeconds.Text = "00";
            // 
            // buttonCheck
            // 
            this.buttonCheck.BackColor = System.Drawing.Color.LightCoral;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCheck.ForeColor = System.Drawing.Color.Black;
            this.buttonCheck.Location = new System.Drawing.Point(71, 553);
            this.buttonCheck.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(350, 50);
            this.buttonCheck.TabIndex = 7;
            this.buttonCheck.Text = "Check Your Solution";
            this.buttonCheck.UseVisualStyleBackColor = false;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // buttonGenerateSudoku
            // 
            this.buttonGenerateSudoku.BackColor = System.Drawing.Color.LightCoral;
            this.buttonGenerateSudoku.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGenerateSudoku.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerateSudoku.Location = new System.Drawing.Point(71, 108);
            this.buttonGenerateSudoku.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGenerateSudoku.Name = "buttonGenerateSudoku";
            this.buttonGenerateSudoku.Size = new System.Drawing.Size(350, 50);
            this.buttonGenerateSudoku.TabIndex = 8;
            this.buttonGenerateSudoku.Text = "Generate Sudoku";
            this.buttonGenerateSudoku.UseVisualStyleBackColor = false;
            this.buttonGenerateSudoku.Click += new System.EventHandler(this.buttonGenerateSudoku_Click);
            // 
            // radioButtonEasy
            // 
            this.radioButtonEasy.AutoSize = true;
            this.radioButtonEasy.Checked = true;
            this.radioButtonEasy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonEasy.Location = new System.Drawing.Point(159, 164);
            this.radioButtonEasy.Name = "radioButtonEasy";
            this.radioButtonEasy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButtonEasy.Size = new System.Drawing.Size(79, 26);
            this.radioButtonEasy.TabIndex = 9;
            this.radioButtonEasy.TabStop = true;
            this.radioButtonEasy.Text = "Easy";
            this.radioButtonEasy.UseVisualStyleBackColor = true;
            // 
            // radioButtonMedium
            // 
            this.radioButtonMedium.AutoSize = true;
            this.radioButtonMedium.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonMedium.Location = new System.Drawing.Point(159, 194);
            this.radioButtonMedium.Name = "radioButtonMedium";
            this.radioButtonMedium.Size = new System.Drawing.Size(103, 26);
            this.radioButtonMedium.TabIndex = 10;
            this.radioButtonMedium.Text = "Medium";
            this.radioButtonMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonHard
            // 
            this.radioButtonHard.AutoSize = true;
            this.radioButtonHard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonHard.Location = new System.Drawing.Point(159, 224);
            this.radioButtonHard.Name = "radioButtonHard";
            this.radioButtonHard.Size = new System.Drawing.Size(78, 26);
            this.radioButtonHard.TabIndex = 11;
            this.radioButtonHard.Text = "Hard";
            this.radioButtonHard.UseVisualStyleBackColor = true;
            // 
            // buttonHint
            // 
            this.buttonHint.BackColor = System.Drawing.Color.LightCoral;
            this.buttonHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonHint.ForeColor = System.Drawing.Color.Black;
            this.buttonHint.Location = new System.Drawing.Point(71, 475);
            this.buttonHint.Margin = new System.Windows.Forms.Padding(0);
            this.buttonHint.Name = "buttonHint";
            this.buttonHint.Size = new System.Drawing.Size(350, 50);
            this.buttonHint.TabIndex = 12;
            this.buttonHint.Text = "Show Next Move";
            this.buttonHint.UseVisualStyleBackColor = false;
            this.buttonHint.Click += new System.EventHandler(this.buttonHint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(194, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 39);
            this.label1.TabIndex = 13;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(275, 417);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 39);
            this.label2.TabIndex = 14;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(121, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(1396, 731);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonHint);
            this.Controls.Add(this.radioButtonHard);
            this.Controls.Add(this.radioButtonMedium);
            this.Controls.Add(this.buttonGenerateSudoku);
            this.Controls.Add(this.radioButtonEasy);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.labelSeconds);
            this.Controls.Add(this.labelMinutes);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.buttonStartSolving);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.buttonLoadSudoku);
            this.Controls.Add(this.sudokuGrid);
            this.Name = "Form1";
            this.Text = "Load Sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sudokuGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView sudokuGrid;
        private System.Windows.Forms.Button buttonLoadSudoku;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Button buttonStartSolving;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelSeconds;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonGenerateSudoku;
        private System.Windows.Forms.RadioButton radioButtonEasy;
        private System.Windows.Forms.RadioButton radioButtonMedium;
        private System.Windows.Forms.RadioButton radioButtonHard;
        private System.Windows.Forms.Button buttonHint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

