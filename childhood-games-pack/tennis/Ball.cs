using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace childhood_games_pack.tennis {
    public partial class Ball : Form {
        private TennisGame tennisMainForm;
        public bool isStay { get; set; } = false;
        public Ball(TennisGame tennisMainForm) {
            InitializeComponent();
            SetTopLevel(false);
            this.tennisMainForm = tennisMainForm;

            Location = new Point(tennisMainForm.userRacket.Location.X + tennisMainForm.userRacket.Size.Width / 2, tennisMainForm.userRacket.Location.Y - tennisMainForm.userRacket.Size.Height);
            isStay = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                if (isStay) {
                    Location = new Point(tennisMainForm.userRacket.Location.X + tennisMainForm.userRacket.Size.Width / 2, tennisMainForm.userRacket.Location.Y - tennisMainForm.userRacket.Size.Height);
                }
                else {
                    break;
                }
            }
            MessageBox.Show("shoot");
        }
    }
}
