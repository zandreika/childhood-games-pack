using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace childhood_games_pack.tanks {
    public partial class Bullet : Form {
        private TanksMainMenu parent;
        private DIRECTION direction;

        private int step;
        private int stepTimer;

        public Bullet(TanksMainMenu parent, DIRECTION direction, Point location) {
            InitializeComponent();
            SetTopLevel(false);

            this.direction = direction;
            this.parent = parent;

            Location = location;

            step = 15;
            stepTimer = 150;

            move();
        }

        private void move() {
            parent.Controls.Add(this);
            Show();

            backgroundMove.RunWorkerAsync();
        }

        private void backgroundMove_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                switch (direction) {
                    case DIRECTION.U:
                        Location = new Point(Location.X, Location.Y - step);
                        break;

                    case DIRECTION.D:
                        Location = new Point(Location.X, Location.Y + step);
                        break;

                    case DIRECTION.L:
                        Location = new Point(Location.X - step, Location.Y);
                        break;

                    case DIRECTION.R:
                        Location = new Point(Location.X + step, Location.Y);
                        break;
                }

                if (Location.X <= 0 || Location.X >= 1260 || Location.Y <= 0 || Location.Y >= 660) {
                    Close();
                }

                Thread.Sleep(stepTimer);
            }
        }
    }
}
