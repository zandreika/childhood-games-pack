using System;
using System.Drawing;
using System.Windows.Forms;


namespace childhood_games_pack.tanks {
    public partial class UserTankForm : Form {
        private TANK_TYPE type;
        private SPEED_LEVEL speedLevel;

        private int step = 20; // pxl

        public UserTankForm(TANK_TYPE type, SPEED_LEVEL speedLevel, Point spot) {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;
            Enabled = true;

            this.type = type;
            this.speedLevel = speedLevel;
            
            shape();
            Location = spot;
        }

        //! Change shape of form depending on the tank-type.
        private void shape() {
            switch (type) {
                case TANK_TYPE.LIGHT:
                    BackgroundImage = Properties.Resources.light_tank;
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
                    Location = new Point(Location.X, Location.Y - step);
                    break;

                case Keys.S:
                    Location = new Point(Location.X, Location.Y + step);
                    break;

                case Keys.A:
                    Location = new Point(Location.X - step, Location.Y);
                    break;

                case Keys.D:
                    Location = new Point(Location.X + step, Location.Y);
                    break;
            }
        }
    }
}
