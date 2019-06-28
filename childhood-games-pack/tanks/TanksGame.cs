using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace childhood_games_pack.tanks {
    public enum TANK_TYPE : int {
        NONE    = 0,
        LIGHT   = 1,
        MEDIUM  = 2,
        HEAVY   = 3
    }

    public enum SPEED_LEVEL : int {
        NONE    = 0,
        LOW     = 1,
        MEDIUM  = 2,
        HIGHT   = 3
    }

    public enum DIRECTION : int {
        U   = 1,
        D   = 2,
        L   = 3,
        R   = 4
    }

    public enum BULLET_TYPE : int {
        USER    = 1,
        COMP    = 2
    }

    public partial class TanksGame : Form {
        public static TanksGame gameRef = null;

        public const int tankHeight = 50;
        public const int tankWidth = 50;
        public const int bulletHeight = 8;
        public const int bulletWidth = 8;

        private enum GAME_STATUS : int {
            LEVEL_SELECT = 1,
            GAME = 2
        }

        public List<CompTank> compTanks = new List<CompTank>();
        public UserTank userTank;
        public bool isUserTankAlive;

        private MainMenuForm mainMenu;
        private GAME_STATUS gameStatus;
        private List<Button> buttons = new List<Button>();

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_EXTENDEDKEY = 1;

        public static void ShootKeyDown(Keys vKey) {
            keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        public TanksGame(MainMenuForm mainMenu) {
            InitializeComponent();

            buttons.Add(level1Button);
            buttons.Add(level2Button);
            buttons.Add(level3Button);
            buttons.Add(level4Button);
            this.mainMenu = mainMenu;

            gameStatus = GAME_STATUS.LEVEL_SELECT;
            Size = new Size(350, 150);

            TanksGame.gameRef = this;
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

            userTank = new UserTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, userSpot);
            isUserTankAlive = true;
            Controls.Add(userTank);
            userTank.Show();

            int spotDifference = -200;
            int countOfEnemies = 5;
            for (int i = 0; i < countOfEnemies; i++) {
                CompTank compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, new Point(compSpot.X + spotDifference, compSpot.Y));
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

                if (isUserTankAlive == false) {
                    MessageBox.Show("You lose!");
                    restartGame();
                    return;
                }

                Thread.Sleep(300);
            }
        }
    }
}
