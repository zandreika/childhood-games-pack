using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tanks
{
    public partial class UserBase : Form
    {
        public UserBase(Point location)
        {
            InitializeComponent();
            SetTopLevel(false);

            Location = location;
            Size = new Size(TanksGame.baseWidth, TanksGame.baseHeight);
        }
    }
}
