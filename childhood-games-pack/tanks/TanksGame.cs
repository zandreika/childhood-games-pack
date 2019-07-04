using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using childhood_games_pack.tanks.Utils;

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
        U = 1,
        D = 2,
        L = 3,
        R = 4
    }

    public enum BULLET_TYPE : int {
        USER = 1,
        COMP = 2
    }

    public partial class TanksGame : Form {
        public const int tankHeight = 50;
        public const int tankWidth = 50;
        public const int bulletHeight = 8;
        public const int bulletWidth = 8;

        public const int bulletStep = 15;
        public const int bulletStepTimer = 150;

        public const int compTankStep = 20;
        public const int compTankStepTimer = 2000;

        private enum GAME_STATUS : int {
            LEVEL_SELECT = 1,
            GAME = 2
        }

        public AtomicList<CompTank> compTanks = new AtomicList<CompTank>();
        //public List<CompTank> compTanks = new List<CompTank>();
        public AtomicList<Bullet> bullets = new AtomicList<Bullet>();
        //public List<Bullet> bullets = new List<Bullet>();
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

            buttons.AddRange(new[] {
                level1Button,
                level2Button,
                level3Button,
                level4Button,
            });
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
            bulletsMoveWorker.RunWorkerAsync();
            compTanksActionWorker.RunWorkerAsync();
        }

        private void TanksMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            mainMenu.Show();
        }

        private void restartGame() {
            /*
            foreach (CompTank tank in compTanks) {
                Controls.Remove(tank);
                tank.Close();
            }

            compTanks.Clear();
            userTank.Close();

            resultGameChecker.CancelAsync();
            bulletsMoveWorker.CancelAsync();
            compTanksActionWorker.CancelAsync();
            */
            Close();
        }

        private void level1Button_Click(object sender, EventArgs e) {
            configureGameField(1);
        }

        private void resultGameChecker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            while (true) {
                if (compTanks.Count() == 0) {
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

        private void bulletsMoveWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            while (true) {
                if (bulletsMoveWorker.CancellationPending) {
                    e.Cancel = true;
                    return;
                }

                foreach (var b in bullets) {
                    if (bulletsMoveWorker.CancellationPending) {
                        e.Cancel = true;
                        return;
                    }

                    if (b.IsDisposed) {
                        break;
                    }

                    switch (b.direction) {
                        case DIRECTION.U:
                            b.Location = new Point(b.Location.X, b.Location.Y - bulletStep);
                            break;

                        case DIRECTION.D:
                            b.Location = new Point(b.Location.X, b.Location.Y + bulletStep);
                            break;

                        case DIRECTION.L:
                            b.Location = new Point(b.Location.X - bulletStep, b.Location.Y);
                            break;

                        case DIRECTION.R:
                            b.Location = new Point(b.Location.X + bulletStep, b.Location.Y);
                            break;
                    }

                    if (b.Location.X <= 0 || b.Location.X >= 1260 || b.Location.Y <= 0 || b.Location.Y >= 660) {
                        bullets.Remove(b);
                        b.Close();
                        break;
                    }

                    Point bulletCenter = new Point(b.Location.X + TanksGame.bulletHeight / 2, b.Location.Y + TanksGame.bulletWidth / 2);

                    bool isNeedBreak = false;
                    switch (b.bulletType) {
                        case BULLET_TYPE.USER:
                            foreach (CompTank tank in compTanks) {
                                if (bulletCenter.X >= tank.Location.X && bulletCenter.X <= tank.Location.X + TanksGame.tankWidth &&
                                    bulletCenter.Y >= tank.Location.Y && bulletCenter.Y <= tank.Location.Y + TanksGame.tankHeight) {

                                    compTanks.Remove(tank);
                                    tank.Close();
                                    bullets.Remove(b);
                                    b.Close();
                                    isNeedBreak = true;
                                    break;
                                }
                            }

                            break;

                        case BULLET_TYPE.COMP:
                            if (bulletCenter.X >= userTank.Location.X && bulletCenter.X <= userTank.Location.X + TanksGame.tankWidth &&
                                bulletCenter.Y >= userTank.Location.Y && bulletCenter.Y <= userTank.Location.Y + TanksGame.tankHeight) {

                                isUserTankAlive = false;
                                userTank.Close();
                                bullets.Remove(b);
                                b.Close();
                                isNeedBreak = true;
                                break;
                            }

                            break;
                    }

                    if (isNeedBreak) {
                        break;
                    }
                } // foreach b in bullets

                Thread.Sleep(bulletStepTimer);
            }
        }

        private void CompTanksActionWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            Random rnd = new Random();

            while (true) {
                if (compTanksActionWorker.CancellationPending) {
                    e.Cancel = true;
                    return;
                }

                foreach (var tank in compTanks) {
                    if (compTanksActionWorker.CancellationPending) {
                        e.Cancel = true;
                        return;
                    }

                    if (tank.IsDisposed) {
                        break;
                    }

                    tank.direction = (DIRECTION)(rnd.Next() % 4);
                    int isNeedShot = rnd.Next() % 10000;

                    if (isNeedShot < 1000) {
                        TanksGame.ShootKeyDown(Keys.B);
                    }

                    switch (tank.direction) {
                        case DIRECTION.U:
                            tank.BackgroundImage = Properties.Resources.light_ctank_u;

                            Point newUpLoc = new Point(tank.Location.X, tank.Location.Y - compTankStep);
                            if (newUpLoc.Y <= 0) {
                                break;
                            }

                            tank.Location = newUpLoc;
                            break;

                        case DIRECTION.D:
                            tank.BackgroundImage = Properties.Resources.light_ctank_d;

                            Point newDownLoc = new Point(tank.Location.X, tank.Location.Y + compTankStep);
                            if (newDownLoc.Y >= 600) {
                                break;
                            }

                            tank.Location = newDownLoc;
                            break;

                        case DIRECTION.L:
                            tank.BackgroundImage = Properties.Resources.light_ctank_l;

                            Point newLeftLoc = new Point(tank.Location.X - compTankStep, tank.Location.Y);
                            if (newLeftLoc.X <= 0) {
                                break;
                            }

                            tank.Location = newLeftLoc;
                            break;

                        case DIRECTION.R:
                            tank.BackgroundImage = Properties.Resources.light_ctank_r;

                            Point newRightLoc = new Point(tank.Location.X + compTankStep, tank.Location.Y);
                            if (newRightLoc.X >= 1200) {
                                break;
                            }

                            tank.Location = newRightLoc;
                            break;
                    }
                }

                Thread.Sleep(compTankStepTimer);
            }
        }

        private void TanksGame_KeyDown(object sender, KeyEventArgs e) {
            Random rnd = new Random();
            Bullet bullet = null;

            switch (e.KeyCode) {
                case Keys.W:
                    userTank.BackgroundImage = Properties.Resources.light_utank_u;
                    userTank.direction = DIRECTION.U;

                    Point newUpLoc = new Point(userTank.Location.X, userTank.Location.Y - compTankStep);
                    if (newUpLoc.Y <= 0) {
                        break;
                    }

                    userTank.Location = newUpLoc;
                    break;

                case Keys.S:
                    userTank.BackgroundImage = Properties.Resources.light_utank_d;
                    userTank.direction = DIRECTION.D;

                    Point newDownLoc = new Point(userTank.Location.X, userTank.Location.Y + compTankStep);
                    if (newDownLoc.Y >= 600) {
                        break;
                    }

                    userTank.Location = newDownLoc;
                    break;

                case Keys.A:
                    userTank.BackgroundImage = Properties.Resources.light_utank_l;
                    userTank.direction = DIRECTION.L;

                    Point newLeftLoc = new Point(userTank.Location.X - compTankStep, userTank.Location.Y);
                    if (newLeftLoc.X <= 0) {
                        break;
                    }

                    userTank.Location = newLeftLoc;
                    break;

                case Keys.D:
                    userTank.BackgroundImage = Properties.Resources.light_utank_r;
                    userTank.direction = DIRECTION.R;

                    Point newRightLoc = new Point(userTank.Location.X + compTankStep, userTank.Location.Y);
                    if (newRightLoc.X >= 1200) {
                        break;
                    }

                    userTank.Location = newRightLoc;
                    break;

                case Keys.Space:
                    bullet = new Bullet(BULLET_TYPE.USER, userTank.direction, userTank.Location);
                    bullets.Add(bullet);
                    Controls.Add(bullet);
                    bullet.Show();
                    break;

                case Keys.B:
                    if (compTanks.Count() == 0) {
                        break;
                    }

                    int tankIndex = rnd.Next() % compTanks.Count();
                    bullet = new Bullet(BULLET_TYPE.COMP, compTanks[tankIndex].direction, compTanks[tankIndex].Location);
                    bullets.Add(bullet);
                    Controls.Add(bullet);
                    bullet.Show();
                    break;
            }
        }
    }
}
