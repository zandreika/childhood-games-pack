﻿namespace childhood_games_pack.snake {
    partial class SnakeGame {
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
            this.snakePanel = new System.Windows.Forms.Panel();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.pauseGameButton = new System.Windows.Forms.Button();
            this.endGameButton = new System.Windows.Forms.Button();
            this.resLabel = new System.Windows.Forms.Label();
            this.startGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // snakePanel
            // 
            this.snakePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.snakePanel.Location = new System.Drawing.Point(27, 9);
            this.snakePanel.Name = "snakePanel";
            this.snakePanel.Size = new System.Drawing.Size(382, 358);
            this.snakePanel.TabIndex = 0;
            this.snakePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.snakePanel_Paint);
            this.snakePanel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.snakePanel_PreviewKeyDown);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scoreLabel.Location = new System.Drawing.Point(444, 9);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(92, 38);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "Score";
            // 
            // pauseGameButton
            // 
            this.pauseGameButton.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pauseGameButton.Location = new System.Drawing.Point(434, 156);
            this.pauseGameButton.Name = "pauseGameButton";
            this.pauseGameButton.Size = new System.Drawing.Size(112, 38);
            this.pauseGameButton.TabIndex = 3;
            this.pauseGameButton.Text = "Pause game";
            this.pauseGameButton.UseVisualStyleBackColor = true;
            // 
            // endGameButton
            // 
            this.endGameButton.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endGameButton.Location = new System.Drawing.Point(434, 213);
            this.endGameButton.Name = "endGameButton";
            this.endGameButton.Size = new System.Drawing.Size(112, 38);
            this.endGameButton.TabIndex = 4;
            this.endGameButton.Text = "End game";
            this.endGameButton.UseVisualStyleBackColor = true;
            // 
            // resLabel
            // 
            this.resLabel.AutoSize = true;
            this.resLabel.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resLabel.Location = new System.Drawing.Point(471, 47);
            this.resLabel.Name = "resLabel";
            this.resLabel.Size = new System.Drawing.Size(26, 29);
            this.resLabel.TabIndex = 5;
            this.resLabel.Text = "0";
            // 
            // startGameButton
            // 
            this.startGameButton.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startGameButton.Location = new System.Drawing.Point(145, 128);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(283, 107);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Start new game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // SnakeGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 395);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.resLabel);
            this.Controls.Add(this.endGameButton);
            this.Controls.Add(this.pauseGameButton);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.snakePanel);
            this.Name = "SnakeGame";
            this.Text = "SnakeGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnakeGame_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel snakePanel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Button pauseGameButton;
        private System.Windows.Forms.Button endGameButton;
        private System.Windows.Forms.Label resLabel;
        private System.Windows.Forms.Button startGameButton;
    }
}