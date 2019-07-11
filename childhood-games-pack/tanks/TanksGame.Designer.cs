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
            this.components = new System.ComponentModel.Container();
            this.level1Button = new System.Windows.Forms.Button();
            this.level2Button = new System.Windows.Forms.Button();
            this.level3Button = new System.Windows.Forms.Button();
            this.level4Button = new System.Windows.Forms.Button();
            this.reloadTimer = new System.Windows.Forms.Timer(this.components);
            this.compTanksActionWorker = new System.Windows.Forms.Timer(this.components);
            this.resultGameChecker = new System.Windows.Forms.Timer(this.components);
            this.bulletsMoveWorker = new System.Windows.Forms.Timer(this.components);
            this.debugTimer = new System.Windows.Forms.Timer(this.components);
            this.debugLabel = new System.Windows.Forms.Label();
            this.gunLabel = new System.Windows.Forms.Label();
            this.mouseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // level1Button
            // 
            this.level1Button.Location = new System.Drawing.Point(21, 34);
            this.level1Button.Name = "level1Button";
            this.level1Button.Size = new System.Drawing.Size(70, 70);
            this.level1Button.TabIndex = 0;
            this.level1Button.Text = "1";
            this.level1Button.UseVisualStyleBackColor = true;
            this.level1Button.Click += new System.EventHandler(this.level1Button_Click);
            // 
            // level2Button
            // 
            this.level2Button.Location = new System.Drawing.Point(95, 34);
            this.level2Button.Name = "level2Button";
            this.level2Button.Size = new System.Drawing.Size(70, 70);
            this.level2Button.TabIndex = 1;
            this.level2Button.Text = "2";
            this.level2Button.UseVisualStyleBackColor = true;
            // 
            // level3Button
            // 
            this.level3Button.Location = new System.Drawing.Point(169, 34);
            this.level3Button.Name = "level3Button";
            this.level3Button.Size = new System.Drawing.Size(70, 70);
            this.level3Button.TabIndex = 2;
            this.level3Button.Text = "3";
            this.level3Button.UseVisualStyleBackColor = true;
            // 
            // level4Button
            // 
            this.level4Button.Location = new System.Drawing.Point(243, 34);
            this.level4Button.Name = "level4Button";
            this.level4Button.Size = new System.Drawing.Size(70, 70);
            this.level4Button.TabIndex = 3;
            this.level4Button.Text = "4";
            this.level4Button.UseVisualStyleBackColor = true;
            // 
            // reloadTimer
            // 
            this.reloadTimer.Tick += new System.EventHandler(this.ReloadTimer_Tick);
            // 
            // compTanksActionWorker
            // 
            this.compTanksActionWorker.Tick += new System.EventHandler(this.CompTanksAction_Tick);
            // 
            // resultGameChecker
            // 
            this.resultGameChecker.Tick += new System.EventHandler(this.ResultGameChecker_Tick);
            // 
            // bulletsMoveWorker
            // 
            this.bulletsMoveWorker.Tick += new System.EventHandler(this.BulletsMoveWorker_Tick);
            // 
            // debugTimer
            // 
            this.debugTimer.Tick += new System.EventHandler(this.DebugTimer_Tick);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(0, 0);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(46, 17);
            this.debugLabel.TabIndex = 4;
            this.debugLabel.Text = "label1";
            // 
            // gunLabel
            // 
            this.gunLabel.AutoSize = true;
            this.gunLabel.Location = new System.Drawing.Point(0, 17);
            this.gunLabel.Name = "gunLabel";
            this.gunLabel.Size = new System.Drawing.Size(46, 17);
            this.gunLabel.TabIndex = 5;
            this.gunLabel.Text = "label1";
            // 
            // mouseLabel
            // 
            this.mouseLabel.AutoSize = true;
            this.mouseLabel.Location = new System.Drawing.Point(0, 34);
            this.mouseLabel.Name = "mouseLabel";
            this.mouseLabel.Size = new System.Drawing.Size(46, 17);
            this.mouseLabel.TabIndex = 6;
            this.mouseLabel.Text = "label1";
            // 
            // TanksGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(854, 492);
            this.Controls.Add(this.mouseLabel);
            this.Controls.Add(this.gunLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.level4Button);
            this.Controls.Add(this.level3Button);
            this.Controls.Add(this.level2Button);
            this.Controls.Add(this.level1Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "TanksGame";
            this.Text = "Tanks";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TanksMainForm_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TanksGame_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TanksGame_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TanksGame_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button level1Button;
        private System.Windows.Forms.Button level2Button;
        private System.Windows.Forms.Button level3Button;
        private System.Windows.Forms.Button level4Button;
        private System.Windows.Forms.Timer reloadTimer;
        private System.Windows.Forms.Timer compTanksActionWorker;
        private System.Windows.Forms.Timer resultGameChecker;
        private System.Windows.Forms.Timer bulletsMoveWorker;
        private System.Windows.Forms.Timer debugTimer;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Label gunLabel;
        private System.Windows.Forms.Label mouseLabel;
    }
}