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
                    Location = new Point(Location.X, Location.Y - step);
                    break;

                case Keys.S:
                    BackgroundImage = Properties.Resources.light_utank_d;
                    direction = DIRECTION.D;
                    Location = new Point(Location.X, Location.Y + step);
                    break;

                case Keys.A:
                    BackgroundImage = Properties.Resources.light_utank_l;
                    direction = DIRECTION.L;
                    Location = new Point(Location.X - step, Location.Y);
                    break;

                case Keys.D:
                    BackgroundImage = Properties.Resources.light_utank_r;
                    direction = DIRECTION.R;
                    Location = new Point(Location.X + step, Location.Y);
                    break;

                case Keys.Space:
                    new Bullet(game, direction, Location);
                    break;
            }
        }
    }
}
