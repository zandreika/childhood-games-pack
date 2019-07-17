using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Unit;
using childhood_games_pack.tanks.Utils;

namespace childhood_games_pack.tanks
{
    public partial class TanksGame
    {
        AtomicList<CompTank> compTanks = new AtomicList<CompTank>();
        AtomicList<Bullet> bullets = new AtomicList<Bullet>();
        AtomicList<Wall> walls = new AtomicList<Wall>();

        UserTank userTank;
        bool isUserTankAlive;

        UserBase userBase;

        static bool isReadyToShoot = true;
        List<CompTank> TanksReadyToShoot = new List<CompTank>();

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
                foreach (var wall in walls)
                {
                    if (bulletCenter.X >= wall.Location.X && bulletCenter.X <= wall.Location.X + wallWidth &&
                        bulletCenter.Y >= wall.Location.Y && bulletCenter.Y <= wall.Location.Y + wallHeight)
                    {
                        walls.Remove(wall);
                        Controls.Remove(wall);
                        bullets.Remove(b);
                        b.Close();
                        break;
                    }
                }

                if (bulletCenter.X >= userBase.Location.X && bulletCenter.X <= userBase.Location.X + baseWidth &&
                    bulletCenter.Y >= userBase.Location.Y && bulletCenter.Y <= userBase.Location.Y + baseHeight)
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

        private Point GetBarrelLocation(Point TankLocation)
        {
            return new Point(TankLocation.X + tankWidth / 2, TankLocation.Y + tankHeight / 2);
        }
    }
}
