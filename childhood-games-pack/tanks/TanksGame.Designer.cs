namespace childhood_games_pack.tanks {
    partial class TanksGame {
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
            this.level1Button = new System.Windows.Forms.Button();
            this.level2Button = new System.Windows.Forms.Button();
            this.level3Button = new System.Windows.Forms.Button();
            this.level4Button = new System.Windows.Forms.Button();
            this.resultGameChecker = new System.ComponentModel.BackgroundWorker();
            this.bulletsMoveWorker = new System.ComponentModel.BackgroundWorker();
            this.compTanksActionWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // level1Button
            // 
            this.level1Button.Location = new System.Drawing.Point(18, 12);
            this.level1Button.Name = "level1Button";
            this.level1Button.Size = new System.Drawing.Size(70, 70);
            this.level1Button.TabIndex = 0;
            this.level1Button.Text = "1";
            this.level1Button.UseVisualStyleBackColor = true;
            this.level1Button.Click += new System.EventHandler(this.level1Button_Click);
            // 
            // level2Button
            // 
            this.level2Button.Location = new System.Drawing.Point(92, 12);
            this.level2Button.Name = "level2Button";
            this.level2Button.Size = new System.Drawing.Size(70, 70);
            this.level2Button.TabIndex = 1;
            this.level2Button.Text = "2";
            this.level2Button.UseVisualStyleBackColor = true;
            // 
            // level3Button
            // 
            this.level3Button.Location = new System.Drawing.Point(166, 12);
            this.level3Button.Name = "level3Button";
            this.level3Button.Size = new System.Drawing.Size(70, 70);
            this.level3Button.TabIndex = 2;
            this.level3Button.Text = "3";
            this.level3Button.UseVisualStyleBackColor = true;
            // 
            // level4Button
            // 
            this.level4Button.Location = new System.Drawing.Point(240, 12);
            this.level4Button.Name = "level4Button";
            this.level4Button.Size = new System.Drawing.Size(70, 70);
            this.level4Button.TabIndex = 3;
            this.level4Button.Text = "4";
            this.level4Button.UseVisualStyleBackColor = true;
            // 
            // resultGameChecker
            // 
            this.resultGameChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.resultGameChecker_DoWork);
            // 
            // bulletsMoveWorker
            // 
            this.bulletsMoveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bulletsMoveWorker_DoWork);
            // 
            // compTanksActionWorker
            // 
            this.compTanksActionWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CompTanksActionWorker_DoWork);
            // 
            // TanksGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(332, 103);
            this.Controls.Add(this.level4Button);
            this.Controls.Add(this.level3Button);
            this.Controls.Add(this.level2Button);
            this.Controls.Add(this.level1Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "TanksGame";
            this.Text = "Tanks";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TanksMainForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TanksGame_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button level1Button;
        private System.Windows.Forms.Button level2Button;
        private System.Windows.Forms.Button level3Button;
        private System.Windows.Forms.Button level4Button;
        private System.ComponentModel.BackgroundWorker resultGameChecker;
        private System.ComponentModel.BackgroundWorker bulletsMoveWorker;
        private System.ComponentModel.BackgroundWorker compTanksActionWorker;
    }
}