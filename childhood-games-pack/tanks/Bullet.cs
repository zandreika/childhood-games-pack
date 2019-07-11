using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tanks {
    public partial class Bullet : Form {
        public DIRECTION Direction { get; set; }
        public BULLET_TYPE BulletType { get; set; }

        public Bullet(BULLET_TYPE bulletType, DIRECTION direction, Point location) {
            InitializeComponent();
            SetTopLevel(false);

            Direction = direction;
            BulletType = bulletType;

            Location = location;
            Size = new Size(TanksGame.bulletWidth, TanksGame.bulletHeight);
        }
    }
}
