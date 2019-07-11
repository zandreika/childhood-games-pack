using System;
using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tanks {
    public partial class UserTank : Form {
        private TANK_TYPE Type { get; set; }
        private SPEED_LEVEL SpeedLevel { get; set; }
        public DIRECTION Direction { get; set; }

        public UserTank(TANK_TYPE type, SPEED_LEVEL speedLevel, Point location) {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;
            Enabled = true;

            Type = type;
            SpeedLevel = speedLevel;
            Direction = DIRECTION.U;

            Shape();

            Location = location;
            Size = new Size(TanksGame.tankWidth, TanksGame.tankHeight);
        }

        //! Change shape of form depending on the tank-type.
        private void Shape() {
            switch (Type) {
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
    }
}
