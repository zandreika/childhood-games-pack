using System;
using System.Drawing;
using System.Windows.Forms;


namespace childhood_games_pack.tanks {
    public partial class UserTank : Form {
        private TanksGame game;
        private TANK_TYPE type;
        private SPEED_LEVEL speedLevel;
        private DIRECTION direction;

        private int step; // pxl

        public UserTank(TANK_TYPE type, SPEED_LEVEL speedLevel, Point spot, TanksGame game) {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;
            Enabled = true;

            this.type = type;
            this.speedLevel = speedLevel;
            this.game = game;

            step = 20;
            direction = DIRECTION.U;

            shape();
            Location = spot;
            Size = new Size(game.tankWidth, game.tankHeight);
        }

        //! Change shape of form depending on the tank-type.
        private void shape() {
            switch (type) {
                case TANK_TYPE.LIGHT:
                    BackgroundImage = Properties.Resources.light_utank_u;
                    break;

                case TANK_TYPE.MEDIUM:
                    BackgroundImage = Properties.Resources.medium_tank;
                    break;

                case TANK_TYPE.HEAVY:
                    BackgroundImage = Properties.Resources.heavy_tank;
                    break;

                case TANK_TYPE.NONE:
                default:
                    throw new Exception("Wrong type of Tank");
            }
        }

        private void TankForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W:
                    BackgroundImage = Properties.Resources.light_utank_u;
                    direction = DIRECTION.U;

                    Point newUpLoc = new Point(Location.X, Location.Y - step);
                    if (newUpLoc.Y <= 0) {
                        break;
                    }

                    Location = newUpLoc;
                    break;

                case Keys.S:
                    BackgroundImage = Properties.Resources.light_utank_d;
                    direction = DIRECTION.D;

                    Point newDownLoc = new Point(Location.X, Location.Y + step);
                    if (newDownLoc.Y >= 600) {
                        break;
                    }

                    Location = newDownLoc;
                    break;

                case Keys.A:
                    BackgroundImage = Properties.Resources.light_utank_l;
                    direction = DIRECTION.L;

                    Point newLeftLoc = new Point(Location.X - step, Location.Y);
                    if (newLeftLoc.X <= 0) {
                        break;
                    }

                    Location = newLeftLoc;
                    break;

                case Keys.D:
                    BackgroundImage = Properties.Resources.light_utank_r;
                    direction = DIRECTION.R;

                    Point newRightLoc = new Point(Location.X + step, Location.Y);
                    if (newRightLoc.X >= 1200) {
                        break;
                    }

                    Location = newRightLoc;
                    break;

                case Keys.Space:
                    new Bullet(game, direction, Location);
                    break;
            }
        }
    }
}
