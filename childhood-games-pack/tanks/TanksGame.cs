using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Utils;

namespace childhood_games_pack.tanks
{
    public enum GAME_STATUS: int
    {
        LEVEL_SELECT    = 0,
        GAME_STARTED    = 1,
        GAME_PAUSED     = 2
    }

    public enum TANK_TYPE : int
    {
        NONE = 0,
        LIGHT = 1,
        MEDIUM = 2,
        HEAVY = 3
    }

    public enum SPEED_LEVEL : int
    {
        NONE = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGHT = 3
    }

    public enum DIRECTION : int
    {
        U = 1,
        D = 2,
        L = 3,
        R = 4
    }

    public enum BULLET_TYPE : int
    {
        USER = 1,
        COMP = 2
    }

    public partial class TanksGame : Form
    {
        public const int gameFieldWidth = 1200;
        public const int gameFieldHeight = 700;
        public const int gameFieldLocationX = 50;
        public const int gameFieldLocationY = 50;
 
        public const int tankHeight = 50;
        public const int tankWidth = 50;
        public const int bulletHeight = 8;
        public const int bulletWidth = 8;
        public const int baseHeight = 70;
        public const int baseWidth = 70;

        public const int bulletStep = 15;
        public const int bulletStepTimer = 150;

        public const int compTankStep = 20;
        public const int compTankStepTimer = 2000;

        public const int reloadTimerMs = 2000;
        public const int resultGameCheckerMs = 300;

        private AtomicList<CompTank> compTanks = new AtomicList<CompTank>();
        private AtomicList<Bullet> bullets = new AtomicList<Bullet>();

        private UserTank userTank;
        private bool isUserTankAlive;

        private UserBase userBase;

        private static bool isReadyToShoot = true;
        private List<CompTank> TanksReadyToShoot = new List<CompTank>();

        private static GAME_STATUS GameStatus { get; set; }

        private MainMenuForm MainMenu { get; set; }
        private List<Button> buttons = new List<Button>();

        public TanksGame(MainMenuForm mainMenu)
        {
            InitializeComponent();

            buttons.AddRange(new[] {
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

        private void ConfigureGameField(int level)
        {
            WindowState = FormWindowState.Maximized;

            foreach (Button b in buttons)
            {
                b.Hide();
            }

            switch (level)
            {
            case 1:
                LevelOneConfigure();
                break;
            }
        }

        private void LevelOneConfigure()
        {
            GameStatus = GAME_STATUS.GAME_STARTED;
            gunLabel.Text = "Gun: Ready";

            var baseLocation = new Point(gameFieldLocationX + gameFieldWidth / 2, gameFieldLocationY + gameFieldHeight - baseHeight);
            userBase = new UserBase(baseLocation);
            Controls.Add(userBase);
            userBase.Show();

            userTank = new UserTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, new Point(baseLocation.X, baseLocation.Y - tankHeight - 30));
            isUserTankAlive = true;
            Controls.Add(userTank);
            userTank.Show();

            /*
            var compSpot = new Point(gameFieldLocationX, gameFieldLocationY);
            var compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot, new DummyStrategy());
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();

            compSpot = new Point(gameFieldLocationX + 100, gameFieldLocationY);
            compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot, new DummyStrategy());
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();
            */

            var compSpot = new Point(gameFieldLocationX + 200, gameFieldLocationY);
            var compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
            compTank.SetStrategy(new DummyStrategy());
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();

            compSpot = new Point(gameFieldLocationX + 300, gameFieldLocationY);
            compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
            compTank.SetStrategy(new UserKillStrategy(compTank, userTank));
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();
            
            compSpot = new Point(gameFieldLocationX + 400, gameFieldLocationY);
            compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
            compTank.SetStrategy(new BaseKillStrategy(compTank, userBase));
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();

            userTank.Focus();
            bulletsMoveWorker.Start();
            resultGameChecker.Start();
            compTanksActionWorker.Start();
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

        private void ReloadTimer_Tick(object sender, EventArgs e)
        {
            isReadyToShoot = true;
            gunLabel.Text = "Gun: Ready";
            reloadTimer.Stop();
        }

        private void CompTanksAction_Tick(object sender, EventArgs e)
        {
            foreach (var tank in compTanks)
            {
                if (tank.IsDisposed)
                {
                    break;
                }

                tank.Direction = tank.Strategy.GetNewDirection();

                if (tank.Strategy.IsNeedShoot())
                {
                    TanksReadyToShoot.Add(tank);
                }

                switch (tank.Direction)
                {
                case DIRECTION.U:
                    tank.BackgroundImage = Properties.Resources.light_ctank_u;

                    Point newUpLoc = new Point(tank.Location.X, tank.Location.Y - compTankStep);
                    if (newUpLoc.Y <= gameFieldLocationY)
                    {
                        tank.Location = new Point(tank.Location.X, gameFieldLocationY);
                        break;
                    }

                    tank.Location = newUpLoc;
                    break;

                case DIRECTION.D:
                    tank.BackgroundImage = Properties.Resources.light_ctank_d;

                    Point newDownLoc = new Point(tank.Location.X, tank.Location.Y + compTankStep);
                    if (newDownLoc.Y + tankHeight >= gameFieldLocationY + gameFieldHeight)
                    {
                        tank.Location = new Point(tank.Location.X, gameFieldLocationY + gameFieldHeight - tankHeight);
                        break;
                    }

                    tank.Location = newDownLoc;
                    break;

                case DIRECTION.L:
                    tank.BackgroundImage = Properties.Resources.light_ctank_l;

                    Point newLeftLoc = new Point(tank.Location.X - compTankStep, tank.Location.Y);
                    if (newLeftLoc.X <= gameFieldLocationX)
                    {
                        tank.Location = new Point(gameFieldLocationX, tank.Location.Y);
                        break;
                    }

                    tank.Location = newLeftLoc;
                    break;

                case DIRECTION.R:
                    tank.BackgroundImage = Properties.Resources.light_ctank_r;

                    Point newRightLoc = new Point(tank.Location.X + compTankStep, tank.Location.Y);
                    if (newRightLoc.X >= gameFieldLocationX + gameFieldWidth)
                    {
                        tank.Location = new Point(gameFieldLocationX + gameFieldWidth - tankWidth, tank.Location.Y);
                        break;
                    }

                    tank.Location = newRightLoc;
                    break;
                }
            }

            foreach (var tank in TanksReadyToShoot)
            {
                var bullet = new Bullet(BULLET_TYPE.COMP, tank.Direction, GetBarrelLocation(tank.Location));
                bullets.Add(bullet);
                Controls.Add(bullet);
                bullet.Show();
            }

            TanksReadyToShoot.Clear();
        }

        private void ResultGameChecker_Tick(object sender, EventArgs e)
        {
            if (compTanks.Count() == 0)
            {
                resultGameChecker.Stop();
                MessageBox.Show("Winner!");
                RestartGame();
                return;
            }

            if (isUserTankAlive == false)
            {
                resultGameChecker.Stop();
                MessageBox.Show("You lose!");
                RestartGame();
                return;
            }
        }

        private void BulletsMoveWorker_Tick(object sender, EventArgs e)
        {
            foreach (var b in bullets)
            {
                if (b.IsDisposed)
                {
                    break;
                }

                switch (b.Direction)
                {
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

                default:
                    bullets.Remove(b);
                    b.Close();
                    break;
                }

                if (b.Location.X <= gameFieldLocationX || b.Location.X >= gameFieldLocationX + gameFieldWidth || 
                    b.Location.Y <= gameFieldLocationY || b.Location.Y >= gameFieldLocationY + gameFieldHeight)
                {
                    bullets.Remove(b);
                    b.Close();
                    break;
                }

                Point bulletCenter = new Point(b.Location.X + bulletHeight / 2, b.Location.Y + bulletWidth / 2);

                if (bulletCenter.X >= userBase.Location.X && bulletCenter.X <= userBase.Location.X + 70 &&
                    bulletCenter.Y >= userBase.Location.Y && bulletCenter.Y <= userBase.Location.Y + 70)
                {

                    isUserTankAlive = false;
                    userBase.Close();
                    bullets.Remove(b);
                    b.Close();
                    break;
                }

                bool isNeedBreak = false;
                switch (b.BulletType)
                {
                case BULLET_TYPE.USER:
                    foreach (CompTank tank in compTanks)
                    {
                        if (bulletCenter.X >= tank.Location.X && bulletCenter.X <= tank.Location.X + tankWidth &&
                            bulletCenter.Y >= tank.Location.Y && bulletCenter.Y <= tank.Location.Y + tankHeight)
                        {

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
                    if (bulletCenter.X >= userTank.Location.X && bulletCenter.X <= userTank.Location.X + tankWidth &&
                        bulletCenter.Y >= userTank.Location.Y && bulletCenter.Y <= userTank.Location.Y + tankHeight)
                    {

                        isUserTankAlive = false;
                        userTank.Close();
                        bullets.Remove(b);
                        b.Close();
                        isNeedBreak = true;
                        break;
                    }

                    break;
                }

                if (isNeedBreak)
                {
                    break;
                }
            }
        }

        private void DebugTimer_Tick(object sender, EventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                debugLabel.Text = "CompTanks: " + compTanks.Count() + " Bullets: " + bullets.Count();
            }
        }

        private void TanksGame_Paint(object sender, PaintEventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                DrawGameField();
            }
        }

        private void DrawGameField()
        {
            var canvas = CreateGraphics();
            var pen = new Pen(Brushes.Black, 4);
            canvas.DrawLine(pen, new Point(gameFieldLocationX, gameFieldLocationY), new Point(gameFieldLocationX + gameFieldWidth, gameFieldLocationY));
            canvas.DrawLine(pen, new Point(gameFieldLocationX + gameFieldWidth, gameFieldLocationY), new Point(gameFieldLocationX + gameFieldWidth, gameFieldLocationY + gameFieldHeight));
            canvas.DrawLine(pen, new Point(gameFieldLocationX + gameFieldWidth, gameFieldLocationY + gameFieldHeight), new Point(gameFieldLocationX, gameFieldLocationY + gameFieldHeight));
            canvas.DrawLine(pen, new Point(gameFieldLocationX, gameFieldLocationY + gameFieldHeight), new Point(gameFieldLocationX, gameFieldLocationY));
        }

        private void TanksGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (GameStatus == GAME_STATUS.GAME_STARTED)
            {
                mouseLabel.Text = e.Location.X + " " + e.Location.Y;
            }
        }

        private Point GetBarrelLocation(Point TankLocation)
        {
            return new Point(TankLocation.X + tankWidth / 2, TankLocation.Y + tankHeight / 2);
        }

        
    }
}
