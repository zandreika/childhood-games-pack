using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace childhood_games_pack {
    public partial class MainMenuForm : Form {
        public MainMenuForm() {
            InitializeComponent();
        }

        private void tanksGameButton_Click(object sender, EventArgs e) {
            tanks.TanksGame tanks = new tanks.TanksGame(this);
            tanks.Show();
            Hide();
        }

        private void TetrisGameButton_Click(object sender, EventArgs e) {
            tetris.TetrisMainForm tetris = new tetris.TetrisMainForm(this);
            tetris.Show();
            Hide();
        }
    }
}
