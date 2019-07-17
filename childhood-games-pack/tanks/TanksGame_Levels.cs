using System.Drawing;
using System.Windows.Forms;
using childhood_games_pack.tanks.Strategy;
using childhood_games_pack.tanks.Unit;

namespace childhood_games_pack.tanks
{
    public partial class TanksGame
    {
        private void ConfigureGameField(int level)
        {
            WindowState = FormWindowState.Maximized;

            foreach (Button b in Buttons)
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

            // Computer tanks
            var compSpot = new Point(gameFieldLocationX, gameFieldLocationY);
            var compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
            compTank.SetStrategy(new DummyStrategy());
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();

            compSpot = new Point(gameFieldLocationX + 100, gameFieldLocationY);
            compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
            compTank.SetStrategy(new UserKillStrategy(compTank, userTank));
            compTanks.Add(compTank);
            Controls.Add(compTank);
            compTank.Show();

            compSpot = new Point(gameFieldLocationX + 200, gameFieldLocationY);
            compTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);
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

            // Walls around base
            DrawBaseWalls(baseLocation, WALL_TYPE.LIGHT);

            userTank.Focus();
            bulletsMoveWorker.Start();
            resultGameChecker.Start();
            compTanksActionWorker.Start();
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

        private void DrawBaseWalls(Point baseLocation, WALL_TYPE wallType)
        {
            // Up walls
            Point wallSpot = new Point(baseLocation.X - wallWidth, baseLocation.Y - wallHeight);
            for (int i = 0; i < 4; i++)
            {
                var wall = new Wall(wallType, wallSpot);
                walls.Add(wall);
                Controls.Add(wall);
                wall.Show();

                wallSpot = new Point(baseLocation.X + i * wallWidth, baseLocation.Y - wallHeight);
            }

            // Left walls
            wallSpot = new Point(baseLocation.X - wallWidth, baseLocation.Y - wallHeight);
            for (int i = 0; i < 3; i++)
            {
                var wall = new Wall(wallType, wallSpot);
                walls.Add(wall);
                Controls.Add(wall);
                wall.Show();

                wallSpot = new Point(baseLocation.X - wallWidth, baseLocation.Y + i * wallHeight);
            }

            // Right walls
            wallSpot = new Point(baseLocation.X + 2 * wallWidth, baseLocation.Y - wallHeight);
            for (int i = 0; i < 3; i++)
            {
                var wall = new Wall(wallType, wallSpot);
                walls.Add(wall);
                Controls.Add(wall);
                wall.Show();

                wallSpot = new Point(baseLocation.X + 2 * wallWidth, baseLocation.Y + i * wallHeight);
            }
        }
    }
}
