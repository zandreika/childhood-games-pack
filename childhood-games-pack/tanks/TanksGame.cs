using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Unit;

namespace childhood_games_pack.tanks
{
    public partial class TanksGame : Form
    {
        static GAME_STATUS GameStatus { get; set; }

        MainMenuForm MainMenu { get; set; }
        List<Button> Buttons = new List<Button>();

        public TanksGame(MainMenuForm mainMenu)
        {
            InitializeComponent();

            Buttons.AddRange(new[] {
                level1Button,
                level2Button,
                level3Button,
                level4Button,
            });

            GameStatus = GAME_STATUS.LEVEL_SELECT;
            MainMenu = mainMenu;

            Size = new Size(350, 150);
            WindowState = FormWindowState.Normal;

            reloadTimer.Interval = reloadTimerMs;
            compTanksActionWorker.Interval = compTankStepTimer;
            resultGameChecker.Interval = resultGameCheckerMs;
            bulletsMoveWorker.Interval = bulletStepTimer;

            gunLabel.Text = "";
            debugLabel.Text = "";
            mouseLabel.Text = "";

            gunLabel.Location = new Point(gameFieldLocationX + gameFieldWidth + 25, gunLabel.Location.Y + gameFieldLocationY + 25);
            debugLabel.Location = new Point(gameFieldLocationX + gameFieldWidth + 25, debugLabel.Location.Y + gameFieldLocationY + 25);
            mouseLabel.Location = new Point(gameFieldLocationX + gameFieldWidth + 25, mouseLabel.Location.Y + gameFieldLocationY + 25);

            debugTimer.Interval = 300;
            debugTimer.Start();
        }

        private void TanksMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenu.Show();
        }

        private void RestartGame()
        {
            bulletsMoveWorker.Stop();
            resultGameChecker.Stop();
            compTanksActionWorker.Stop();
            reloadTimer.Stop();
            Close();
        }

        private void level1Button_Click(object sender, EventArgs e)
        {
            ConfigureGameField(1);
        }

        private void TanksGame_Paint(object sender, PaintEventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                DrawGameField();
            }
        }

        private void TanksGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                mouseLabel.Text = e.Location.X + " " + e.Location.Y;
            }
        }

        private void TanksGame_KeyDown(object sender, KeyEventArgs e)
        {
            Random rnd = new Random();
            Bullet bullet;

            switch (e.KeyCode)
            {
            case Keys.W:
                userTank.BackgroundImage = Properties.Resources.light_utank_u;
                userTank.Direction = DIRECTION.U;

                Point newUpLoc = new Point(userTank.Location.X, userTank.Location.Y - compTankStep);
                if (newUpLoc.Y <= gameFieldLocationY)
                {
                    userTank.Location = new Point(userTank.Location.X, gameFieldLocationY);
                    break;
                }

                userTank.Location = newUpLoc;
                break;

            case Keys.S:
                userTank.BackgroundImage = Properties.Resources.light_utank_d;
                userTank.Direction = DIRECTION.D;

                Point newDownLoc = new Point(userTank.Location.X, userTank.Location.Y + compTankStep);
                if (newDownLoc.Y + tankHeight >= gameFieldLocationY + gameFieldHeight)
                {
                    userTank.Location = new Point(userTank.Location.X, gameFieldLocationY + gameFieldHeight - tankHeight);
                    break;
                }

                userTank.Location = newDownLoc;
                break;

            case Keys.A:
                userTank.BackgroundImage = Properties.Resources.light_utank_l;
                userTank.Direction = DIRECTION.L;

                Point newLeftLoc = new Point(userTank.Location.X - compTankStep, userTank.Location.Y);
                if (newLeftLoc.X <= gameFieldLocationX)
                {
                    userTank.Location = new Point(gameFieldLocationX, userTank.Location.Y);
                    break;
                }

                userTank.Location = newLeftLoc;
                break;

            case Keys.D:
                userTank.BackgroundImage = Properties.Resources.light_utank_r;
                userTank.Direction = DIRECTION.R;

                Point newRightLoc = new Point(userTank.Location.X + compTankStep, userTank.Location.Y);
                if (newRightLoc.X + tankWidth >= gameFieldLocationX + gameFieldWidth)
                {
                    userTank.Location = new Point(gameFieldLocationX + gameFieldWidth - tankWidth, userTank.Location.Y);
                    break;
                }

                userTank.Location = newRightLoc;
                break;

            case Keys.Space:
                if (!isReadyToShoot)
                {
                    break;
                }

                bullet = new Bullet(BULLET_TYPE.USER, userTank.Direction, GetBarrelLocation(userTank.Location));
                bullets.Add(bullet);
                Controls.Add(bullet);
                bullet.Show();
                isReadyToShoot = false;
                gunLabel.Text = "Gun: Reloading";
                reloadTimer.Start();
                break;
            }
        }

        private void DebugTimer_Tick(object sender, EventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                debugLabel.Text = "CompTanks: " + compTanks.Count() + " Bullets: " + bullets.Count();
            }
        }
    }
}
