using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public partial class UserRacket : Form
    {
        private TennisGame tennisGame;
        public int lastKickXTrajectory;
        public UserRacket(TennisGame tennisGame)
        {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;
            Size = new Size(45, 15);
            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, tennisGame.TablePanel.Height - Size.Height);
        }

        private void Racket_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                {
                    Point newLocation = new Point(Location.X - tennisGame.ball.Width / 3, Location.Y);

                    if (newLocation.X + Size.Width / 2 < tennisGame.TablePanel.Width / 2)
                    {
                        BackColor = Color.Black;
                    }

                    if (newLocation.X < 0)
                    {
                        newLocation.X = 0;
                    }
                    Location = newLocation;
                    break;
                }
                case Keys.Right:
                case Keys.D:
                {
                    Point newLocation = new Point(Location.X + tennisGame.ball.Width / 3, Location.Y);

                    if (newLocation.X + Size.Width / 2 >= tennisGame.TablePanel.Width / 2)
                    {
                        BackColor = Color.Red;
                    }

                    if (newLocation.X + Size.Width > tennisGame.TablePanel.Width)
                    {
                        newLocation.X = tennisGame.TablePanel.Width - Size.Width;
                    }
                    Location = newLocation;
                    break;
                }
                case Keys.Up:
                case Keys.W:
                {
                    Shoot();
                    break;
                }
            }
        }

        private void Shoot()
        {
            if (tennisGame.ball.isStay && tennisGame.gameStatus == GAME_STATUS.IN_GAME)
            {
                tennisGame.ball.isStay = false;
                lastKickXTrajectory = 0;
            }
        }
    }
}
