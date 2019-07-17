using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tanks.Unit
{
    class Wall : Panel
    {
        private WALL_TYPE Type { get; set; }
        public Wall(WALL_TYPE type, Point location)
        {
            SetTopLevel(false);
            Size = new Size(TanksGame.wallWidth, TanksGame.wallHeight);
            BackgroundImage = Properties.Resources.light_wall;
            AutoSize = false;

            Location = location;
            Type = type;
        }
    }
}
