using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace childhood_games_pack.tanks {
    public partial class Bullet : Form {
        public DIRECTION direction;
        public BULLET_TYPE bulletType;

        public Bullet(BULLET_TYPE bulletType, DIRECTION direction, Point location) {
            InitializeComponent();
            SetTopLevel(false);

            this.direction = direction;
            this.bulletType = bulletType;

            Location = location;
            Size = new Size(TanksGame.bulletWidth, TanksGame.bulletHeight);
        }
    }
}
