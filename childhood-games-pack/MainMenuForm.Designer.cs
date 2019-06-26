namespace childhood_games_pack
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tanksGameButton = new System.Windows.Forms.Button();
            this.tetrisGameButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tanksGameButton
            // 
            this.tanksGameButton.Location = new System.Drawing.Point(50, 45);
            this.tanksGameButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tanksGameButton.Name = "tanksGameButton";
            this.tanksGameButton.Size = new System.Drawing.Size(232, 41);
            this.tanksGameButton.TabIndex = 0;
            this.tanksGameButton.Text = "tanks";
            this.tanksGameButton.UseVisualStyleBackColor = true;
            this.tanksGameButton.Click += new System.EventHandler(this.tanksGameButton_Click);
            // 
            // tetrisGameButton
            // 
            this.tetrisGameButton.Location = new System.Drawing.Point(50, 105);
            this.tetrisGameButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tetrisGameButton.Name = "tetrisGameButton";
            this.tetrisGameButton.Size = new System.Drawing.Size(232, 48);
            this.tetrisGameButton.TabIndex = 1;
            this.tetrisGameButton.Text = "tetris";
            this.tetrisGameButton.UseVisualStyleBackColor = true;
            this.tetrisGameButton.Click += new System.EventHandler(this.TetrisGameButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(50, 167);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(232, 46);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 431);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tetrisGameButton);
            this.Controls.Add(this.tanksGameButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainMenuForm";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tanksGameButton;
        private System.Windows.Forms.Button tetrisGameButton;
        private System.Windows.Forms.Button button3;
    }
}

