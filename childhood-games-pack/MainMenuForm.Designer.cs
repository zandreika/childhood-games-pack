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
            this.tennisGameButton = new System.Windows.Forms.Button();
            this.snakeGameButton = new System.Windows.Forms.Button();
            this.tanksButtonLogo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tanksGameButton
            // 
            this.tanksGameButton.Location = new System.Drawing.Point(124, 55);
            this.tanksGameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tanksGameButton.Name = "tanksGameButton";
            this.tanksGameButton.Size = new System.Drawing.Size(252, 50);
            this.tanksGameButton.TabIndex = 0;
            this.tanksGameButton.Text = "Tanks";
            this.tanksGameButton.UseVisualStyleBackColor = true;
            this.tanksGameButton.Click += new System.EventHandler(this.tanksGameButton_Click);
            // 
            // tetrisGameButton
            // 
            this.tetrisGameButton.Location = new System.Drawing.Point(67, 129);
            this.tetrisGameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tetrisGameButton.Name = "tetrisGameButton";
            this.tetrisGameButton.Size = new System.Drawing.Size(309, 59);
            this.tetrisGameButton.TabIndex = 1;
            this.tetrisGameButton.Text = "tetris";
            this.tetrisGameButton.UseVisualStyleBackColor = true;
            this.tetrisGameButton.Click += new System.EventHandler(this.TetrisGameButton_Click);
            // 
            // tennisGameButton
            // 
            this.tennisGameButton.Location = new System.Drawing.Point(67, 206);
            this.tennisGameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tennisGameButton.Name = "tennisGameButton";
            this.tennisGameButton.Size = new System.Drawing.Size(309, 57);
            this.tennisGameButton.TabIndex = 2;
            this.tennisGameButton.Text = "tennis";
            this.tennisGameButton.UseVisualStyleBackColor = true;
            this.tennisGameButton.Click += new System.EventHandler(this.TennisGameButton_Click);
            // 
            // snakeGameButton
            // 
            this.snakeGameButton.Location = new System.Drawing.Point(67, 281);
            this.snakeGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.snakeGameButton.Name = "snakeGameButton";
            this.snakeGameButton.Size = new System.Drawing.Size(309, 57);
            this.snakeGameButton.TabIndex = 3;
            this.snakeGameButton.Text = "snake";
            this.snakeGameButton.UseVisualStyleBackColor = true;
            this.snakeGameButton.Click += new System.EventHandler(this.SnakeGameButton_Click);
            // 
            // tanksButtonLogo
            // 
            this.tanksButtonLogo.BackgroundImage = global::childhood_games_pack.Properties.Resources.light_utank_u;
            this.tanksButtonLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tanksButtonLogo.Location = new System.Drawing.Point(67, 55);
            this.tanksButtonLogo.Name = "tanksButtonLogo";
            this.tanksButtonLogo.Size = new System.Drawing.Size(51, 50);
            this.tanksButtonLogo.TabIndex = 4;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 530);
            this.Controls.Add(this.tanksButtonLogo);
            this.Controls.Add(this.snakeGameButton);
            this.Controls.Add(this.tennisGameButton);
            this.Controls.Add(this.tetrisGameButton);
            this.Controls.Add(this.tanksGameButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainMenuForm";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tanksGameButton;
        private System.Windows.Forms.Button tetrisGameButton;
        private System.Windows.Forms.Button tennisGameButton;
        private System.Windows.Forms.Button snakeGameButton;
        private System.Windows.Forms.Panel tanksButtonLogo;
    }
}

