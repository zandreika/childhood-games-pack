namespace childhood_games_pack.tetris {
    partial class TetrisGame {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tetrisGamePanel = new System.Windows.Forms.Panel();
            this.SmallFigureRadioButton = new System.Windows.Forms.RadioButton();
            this.MediumFigureRadioButton = new System.Windows.Forms.RadioButton();
            this.BigFigureRadioButton = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.nextFigurePanel = new System.Windows.Forms.Panel();
            this.ScoreHeaderLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.InfoButton = new System.Windows.Forms.Button();
            this.FigureSizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tetrisGamePanel
            // 
            this.tetrisGamePanel.BackColor = System.Drawing.SystemColors.Window;
            this.tetrisGamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tetrisGamePanel.Location = new System.Drawing.Point(12, 12);
            this.tetrisGamePanel.Name = "tetrisGamePanel";
            this.tetrisGamePanel.Size = new System.Drawing.Size(200, 400);
            this.tetrisGamePanel.TabIndex = 1;
            this.tetrisGamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TetrisGamePanel_Paint);
            // 
            // SmallFigureRadioButton
            // 
            this.SmallFigureRadioButton.AutoSize = true;
            this.SmallFigureRadioButton.Location = new System.Drawing.Point(75, 31);
            this.SmallFigureRadioButton.Name = "SmallFigureRadioButton";
            this.SmallFigureRadioButton.Size = new System.Drawing.Size(50, 17);
            this.SmallFigureRadioButton.TabIndex = 0;
            this.SmallFigureRadioButton.TabStop = true;
            this.SmallFigureRadioButton.Text = "Small";
            this.SmallFigureRadioButton.UseVisualStyleBackColor = true;
            // 
            // MediumFigureRadioButton
            // 
            this.MediumFigureRadioButton.AutoSize = true;
            this.MediumFigureRadioButton.Location = new System.Drawing.Point(75, 54);
            this.MediumFigureRadioButton.Name = "MediumFigureRadioButton";
            this.MediumFigureRadioButton.Size = new System.Drawing.Size(62, 17);
            this.MediumFigureRadioButton.TabIndex = 1;
            this.MediumFigureRadioButton.TabStop = true;
            this.MediumFigureRadioButton.Text = "Medium";
            this.MediumFigureRadioButton.UseVisualStyleBackColor = true;
            // 
            // BigFigureRadioButton
            // 
            this.BigFigureRadioButton.AutoSize = true;
            this.BigFigureRadioButton.Location = new System.Drawing.Point(75, 77);
            this.BigFigureRadioButton.Name = "BigFigureRadioButton";
            this.BigFigureRadioButton.Size = new System.Drawing.Size(40, 17);
            this.BigFigureRadioButton.TabIndex = 2;
            this.BigFigureRadioButton.TabStop = true;
            this.BigFigureRadioButton.Text = "Big";
            this.BigFigureRadioButton.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // nextFigurePanel
            // 
            this.nextFigurePanel.BackColor = System.Drawing.SystemColors.Window;
            this.nextFigurePanel.Location = new System.Drawing.Point(250, 100);
            this.nextFigurePanel.Name = "nextFigurePanel";
            this.nextFigurePanel.Size = new System.Drawing.Size(100, 100);
            this.nextFigurePanel.TabIndex = 2;
            this.nextFigurePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.NextFigurePanel_Paint);
            // 
            // ScoreHeaderLabel
            // 
            this.ScoreHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoreHeaderLabel.Location = new System.Drawing.Point(250, 10);
            this.ScoreHeaderLabel.Name = "ScoreHeaderLabel";
            this.ScoreHeaderLabel.Size = new System.Drawing.Size(100, 25);
            this.ScoreHeaderLabel.TabIndex = 3;
            this.ScoreHeaderLabel.Text = "Score";
            this.ScoreHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoreLabel.Location = new System.Drawing.Point(250, 36);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(100, 25);
            this.ScoreLabel.TabIndex = 4;
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(75, 100);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(100, 30);
            this.StartGameButton.TabIndex = 0;
            this.StartGameButton.Text = "Start Game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // InfoButton
            // 
            this.InfoButton.Location = new System.Drawing.Point(75, 139);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(100, 30);
            this.InfoButton.TabIndex = 5;
            this.InfoButton.Text = "Info";
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // FigureSizeLabel
            // 
            this.FigureSizeLabel.AutoSize = true;
            this.FigureSizeLabel.Location = new System.Drawing.Point(75, 10);
            this.FigureSizeLabel.Name = "FigureSizeLabel";
            this.FigureSizeLabel.Size = new System.Drawing.Size(105, 13);
            this.FigureSizeLabel.TabIndex = 6;
            this.FigureSizeLabel.Text = "Choose size of figure";
            // 
            // TetrisGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.FigureSizeLabel);
            this.Controls.Add(this.BigFigureRadioButton);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.MediumFigureRadioButton);
            this.Controls.Add(this.SmallFigureRadioButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.ScoreHeaderLabel);
            this.Controls.Add(this.nextFigurePanel);
            this.Controls.Add(this.tetrisGamePanel);
            this.Name = "TetrisGame";
            this.Text = "TetrisMainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TetrisMainForm_FormClosed);
            this.Load += new System.EventHandler(this.TetrisGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TetrisMainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tetrisGamePanel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel nextFigurePanel;
        private System.Windows.Forms.Label ScoreHeaderLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Button InfoButton;
        private System.Windows.Forms.RadioButton BigFigureRadioButton;
        private System.Windows.Forms.RadioButton MediumFigureRadioButton;
        private System.Windows.Forms.RadioButton SmallFigureRadioButton;
        private System.Windows.Forms.Label FigureSizeLabel;
    }
}