using System;
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
            tetris.TetrisGame tetris = new tetris.TetrisGame(this);
            tetris.Show();
            Hide();
        }

        private void TennisGameButton_Click(object sender, EventArgs e) {
            tennis.TennisGame tennis = new tennis.TennisGame(this);
            tennis.Show();
            Hide();
        }
    }
}
