using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace childhood_games_pack.tanks {
    public enum TANK_TYPE : int {
        NONE = 0,
        LIGHT = 1,
        MEDIUM = 2,
        HEAVY = 3
    }

    public enum SPEED_LEVEL : int {
        NONE = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGHT = 3
    }

    public enum DIRECTION : int {
        U   = 1,
        D   = 2,
        L   = 3,
        R   = 4
    }

    public partial class TanksGame : Form {
        private enum GAME_STATUS : int {
            LEVEL_SELECT = 1,
            GAME = 2
        }

        public int tankHeight = 50;
        public int tankWidth = 50;
        public int bulletHeight = 8;
        public int bulletWidth = 8;

        public List<CompTank> compTanks = new List<CompTank>();
        public UserTank userTank;

        private MainMenuForm mainMenu;
        private GAME_STATUS gameStatus;
        private List<Button> buttons = new List<Button>();
        
        public TanksGame(MainMenuForm mainMenu) {
            InitializeComponent();

            buttons.Add(level1Button);
            buttons.Add(level2Button);
            buttons.Add(level3Button);
            buttons.Add(level4Button);
            this.mainMenu = mainMenu;

            gameStatus = GAME_STATUS.LEVEL_SELECT;
            Size = new Size(350, 150);
        }

        private void configureGameField(int level) {
            gameStatus = GAME_STATUS.GAME;
            Size = new Size(1260, 660);

            foreach (Button b in buttons) {
                b.Hide();
            }

            switch (level) {
                case 1:
                    levelOneConfigure();
                    break;
            }
        }

        private void levelOneConfigure() {
            Point userSpot = new Point(500, 500);
            Point compSpot = new Point(500, 50);

            userTank = new UserTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, userSpot, this);
            Controls.Add(userTank);
            userTank.Show();

            int spotDifference = -200;
            int countOfEnemies = 5;
            for (int i = 0; i < countOfEnemies; i++) {
                CompTank compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, new Point(compSpot.X + spotDifference, compSpot.Y), this);
                compTanks.Add(compTank);
                Controls.Add(compTank);
                compTank.Show();

                spotDifference += 100;
            }

            userTank.Focus();
            resultGameChecker.RunWorkerAsync();
        }

        private void TanksMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            mainMenu.Show();
        }

        private void restartGame() {
            foreach (CompTank tank in compTanks) {
                Controls.Remove(tank);
                tank.Close();
            }

            userTank.Close();

            gameStatus = GAME_STATUS.LEVEL_SELECT;
            Size = new Size(350, 150);

            foreach (Button b in buttons) {
                b.Show();
            }
        }

        private void level1Button_Click(object sender, EventArgs e) {
            configureGameField(1);
        }

        private void resultGameChecker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            while (true) {
                if (compTanks.Count == 0) {
                    MessageBox.Show("Winner!");
                    restartGame();
                    return;
                }

                Thread.Sleep(300);
            }
        }
    }
}
