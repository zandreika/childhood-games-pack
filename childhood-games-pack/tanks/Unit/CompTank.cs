using System;
using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Strategy;

namespace childhood_games_pack.tanks.Unit
{
    public partial class CompTank : Form
    {
        public DIRECTION Direction { get; set; }
        public ICompTankStrategy Strategy { get; set; }

        TANK_TYPE Type { get; set; }
        SPEED_LEVEL SpeedLevel { get; set; }

        public CompTank(TANK_TYPE type, SPEED_LEVEL speedLevel, Point location)
        {
            InitializeComponent();
            SetTopLevel(false);
            AutoSize = false;

            Type = type;
            SpeedLevel = speedLevel;
            Direction = DIRECTION.D;

            Shape();

            Location = location;
            Size = new Size(TanksGame.tankWidth, TanksGame.tankHeight);
        }

        public void SetStrategy(ICompTankStrategy strategy)
        {
            Strategy = strategy;
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
