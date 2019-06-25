namespace childhood_games_pack.tanks {
    partial class CompTankForm {
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
            this.walkAndAttackWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // walkAndAttackWorker
            // 
            this.walkAndAttackWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.walkAndAttackWorker_DoWork);
            // 
            // CompTankForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::childhood_games_pack.Properties.Resources.light_tank;
            this.ClientSize = new System.Drawing.Size(60, 60);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompTankForm";
            this.Text = "TankCompForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker walkAndAttackWorker;
    }
}