namespace childhood_games_pack.tetris {
    partial class TetrisMainForm {
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.restartGameButton = new System.Windows.Forms.Button();
            this.nextFigurePanel = new System.Windows.Forms.Panel();
            this.ScoreHeaderLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tetrisGamePanel
            // 
            this.tetrisGamePanel.BackColor = System.Drawing.SystemColors.Window;
            this.tetrisGamePanel.Location = new System.Drawing.Point(12, 12);
            this.tetrisGamePanel.Name = "tetrisGamePanel";
            this.tetrisGamePanel.Size = new System.Drawing.Size(200, 400);
            this.tetrisGamePanel.TabIndex = 0;
            this.tetrisGamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TetrisGamePanel_Paint);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // restartGameButton
            // 
            this.restartGameButton.Location = new System.Drawing.Point(243, 209);
            this.restartGameButton.Name = "restartGameButton";
            this.restartGameButton.Size = new System.Drawing.Size(100, 25);
            this.restartGameButton.TabIndex = 1;
            this.restartGameButton.TabStop = false;
            this.restartGameButton.Text = "restart";
            this.restartGameButton.UseVisualStyleBackColor = true;
            this.restartGameButton.Click += new System.EventHandler(this.RestartGameButton_Click);
            // 
            // nextFigurePanel
            // 
            this.nextFigurePanel.BackColor = System.Drawing.SystemColors.Window;
            this.nextFigurePanel.Location = new System.Drawing.Point(243, 93);
            this.nextFigurePanel.Name = "nextFigurePanel";
            this.nextFigurePanel.Size = new System.Drawing.Size(100, 100);
            this.nextFigurePanel.TabIndex = 2;
            this.nextFigurePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.NextFigurePanel_Paint);
            // 
            // ScoreHeaderLabel
            // 
            this.ScoreHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoreHeaderLabel.Location = new System.Drawing.Point(243, 13);
            this.ScoreHeaderLabel.Name = "ScoreHeaderLabel";
            this.ScoreHeaderLabel.Size = new System.Drawing.Size(100, 23);
            this.ScoreHeaderLabel.TabIndex = 3;
            this.ScoreHeaderLabel.Text = "Score";
            this.ScoreHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoreLabel.Location = new System.Drawing.Point(243, 36);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(100, 25);
            this.ScoreLabel.TabIndex = 4;
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TetrisMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 450);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.ScoreHeaderLabel);
            this.Controls.Add(this.nextFigurePanel);
            this.Controls.Add(this.restartGameButton);
            this.Controls.Add(this.tetrisGamePanel);
            this.Name = "TetrisMainForm";
            this.Text = "TetrisMainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TetrisMainForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TetrisMainForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tetrisGamePanel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button restartGameButton;
        private System.Windows.Forms.Panel nextFigurePanel;
        private System.Windows.Forms.Label ScoreHeaderLabel;
        private System.Windows.Forms.Label ScoreLabel;
    }
}