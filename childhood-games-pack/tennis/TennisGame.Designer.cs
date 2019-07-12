namespace childhood_games_pack.tennis {
    partial class TennisGame {
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
            this.TablePanel = new System.Windows.Forms.Panel();
            this.UserScoreHeaderLabel = new System.Windows.Forms.Label();
            this.UserScoreLabel = new System.Windows.Forms.Label();
            this.CompScoreHeaderLabel = new System.Windows.Forms.Label();
            this.CompScoreLabel = new System.Windows.Forms.Label();
            this.RestartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TablePanel
            // 
            this.TablePanel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TablePanel.Location = new System.Drawing.Point(13, 13);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.Size = new System.Drawing.Size(228, 411);
            this.TablePanel.TabIndex = 0;
            this.TablePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TablePanel_Paint);
            // 
            // UserScoreHeaderLabel
            // 
            this.UserScoreHeaderLabel.AutoSize = true;
            this.UserScoreHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserScoreHeaderLabel.Location = new System.Drawing.Point(291, 15);
            this.UserScoreHeaderLabel.Name = "UserScoreHeaderLabel";
            this.UserScoreHeaderLabel.Size = new System.Drawing.Size(57, 25);
            this.UserScoreHeaderLabel.TabIndex = 1;
            this.UserScoreHeaderLabel.Text = "User";
            // 
            // UserScoreLabel
            // 
            this.UserScoreLabel.AutoSize = true;
            this.UserScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserScoreLabel.Location = new System.Drawing.Point(310, 50);
            this.UserScoreLabel.Name = "UserScoreLabel";
            this.UserScoreLabel.Size = new System.Drawing.Size(0, 25);
            this.UserScoreLabel.TabIndex = 2;
            // 
            // CompScoreHeaderLabel
            // 
            this.CompScoreHeaderLabel.AutoSize = true;
            this.CompScoreHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompScoreHeaderLabel.Location = new System.Drawing.Point(354, 15);
            this.CompScoreHeaderLabel.Name = "CompScoreHeaderLabel";
            this.CompScoreHeaderLabel.Size = new System.Drawing.Size(105, 25);
            this.CompScoreHeaderLabel.TabIndex = 3;
            this.CompScoreHeaderLabel.Text = "Computer";
            // 
            // CompScoreLabel
            // 
            this.CompScoreLabel.AutoSize = true;
            this.CompScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompScoreLabel.Location = new System.Drawing.Point(396, 50);
            this.CompScoreLabel.Name = "CompScoreLabel";
            this.CompScoreLabel.Size = new System.Drawing.Size(0, 25);
            this.CompScoreLabel.TabIndex = 4;
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(296, 235);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(75, 51);
            this.RestartButton.TabIndex = 5;
            this.RestartButton.Text = "Start new game";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // TennisGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.CompScoreLabel);
            this.Controls.Add(this.CompScoreHeaderLabel);
            this.Controls.Add(this.UserScoreLabel);
            this.Controls.Add(this.UserScoreHeaderLabel);
            this.Controls.Add(this.TablePanel);
            this.Name = "TennisGame";
            this.Text = "TennisMainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TennisMainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel TablePanel;
        private System.Windows.Forms.Label UserScoreHeaderLabel;
        public System.Windows.Forms.Label UserScoreLabel;
        private System.Windows.Forms.Label CompScoreHeaderLabel;
        public System.Windows.Forms.Label CompScoreLabel;
        public System.Windows.Forms.Button RestartButton;
    }
}