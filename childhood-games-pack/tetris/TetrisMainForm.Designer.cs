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
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // TetrisMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 450);
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
    }
}