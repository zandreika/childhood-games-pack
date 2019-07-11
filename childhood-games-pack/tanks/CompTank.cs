using System;
using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Utils;

namespace childhood_games_pack.tanks
{
    public partial class CompTank : Form
    {
        public DIRECTION Direction { get; set; }
        private TANK_TYPE Type { get; set; }
        private SPEED_LEVEL SpeedLevel { get; set; }

        public ICompTankStrategy Strategy { get; set; }

        public CompTank(TANK_TYPE type, SPEED_LEVEL speedLevel, Point location, ICompTankStrategy strategy)
        {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;

            Type = type;
            SpeedLevel = speedLevel;
            Direction = DIRECTION.D;
            Strategy = strategy;

            Shape();

            Location = location;
            Size = new Size(TanksGame.tankWidth, TanksGame.tankHeight);
        }

        //! Change shape of form depending on the tank-type.
        private void Shape()
        {
            switch (Type)
            {
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
