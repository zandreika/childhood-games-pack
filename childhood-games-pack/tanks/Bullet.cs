using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace childhood_games_pack.tanks {
    public partial class Bullet : Form {
        private TanksGame game;
        private DIRECTION direction;
        BULLET_TYPE bulletType;

        private int step;
        private int stepTimer;

        public Bullet(BULLET_TYPE bulletType, DIRECTION direction, Point location) {
            InitializeComponent();
            SetTopLevel(false);

            this.direction = direction;
            this.game = TanksGame.gameRef;
            this.bulletType = bulletType;

            Location = location;
            Size = new Size(TanksGame.bulletWidth, TanksGame.bulletHeight);

            step = 15;
            stepTimer = 150;

            move();
        }

        private void move() {
            game.Controls.Add(this);
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
                
                Point bulletCenter = new Point(Location.X + TanksGame.bulletHeight / 2, Location.Y + TanksGame.bulletWidth / 2);

                switch (bulletType) {
                    case BULLET_TYPE.USER:
                        foreach (CompTank tank in game.compTanks) {
                            if (bulletCenter.X >= tank.Location.X && bulletCenter.X <= tank.Location.X + TanksGame.tankWidth &&
                                bulletCenter.Y >= tank.Location.Y && bulletCenter.Y <= tank.Location.Y + TanksGame.tankHeight) {

                                game.compTanks.Remove(tank);
                                tank.Close();
                                Close();
                            }
                        }

                        break;

                    case BULLET_TYPE.COMP:
                        if (bulletCenter.X >= game.userTank.Location.X && bulletCenter.X <= game.userTank.Location.X + TanksGame.tankWidth &&
                            bulletCenter.Y >= game.userTank.Location.Y && bulletCenter.Y <= game.userTank.Location.Y + TanksGame.tankHeight) {

                            game.isUserTankAlive = false;
                            game.userTank.Close();
                            Close();
                        }

                        break;
                }
                
                Thread.Sleep(stepTimer);
            }
        }
    }
}
