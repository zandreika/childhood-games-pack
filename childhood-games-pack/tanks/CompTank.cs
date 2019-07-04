using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace childhood_games_pack.tanks {
    public partial class CompTank : Form {
        public DIRECTION direction = DIRECTION.D;

        private TANK_TYPE type;
        private SPEED_LEVEL speedLevel;
        
        public CompTank(TANK_TYPE type, SPEED_LEVEL speedLevel, Point spot) {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;

            this.type = type;
            this.speedLevel = speedLevel;

            shape();

            Location = spot;
            Size = new Size(TanksGame.tankWidth, TanksGame.tankHeight);
        }

        //! Change shape of form depending on the tank-type.
        private void shape() {
            switch (type) {
                case TANK_TYPE.LIGHT:
                    BackgroundImage = Properties.Resources.light_ctank_u;
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
