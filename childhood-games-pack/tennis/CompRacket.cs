using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public partial class CompRacket : Form
    {
        private TennisGame tennisGame;
        public CompRacket(TennisGame tennisGame)
        {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;
            Size = tennisGame.userRacket.Size;
            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("BW");
            Thread.Sleep(200);
            while (tennisGame.gameStatus != GAME_STATUS.END)
            {
                if (!tennisGame.ball.isStay)
                {
                    if (Location.X + Size.Width / 2 < tennisGame.ball.Location.X)
                    {
                        if (Right + tennisGame.ball.Width > tennisGame.TablePanel.Width)
                        {
                            Location = new Point(tennisGame.TablePanel.Width - Size.Width, Location.Y);
                        }
                        else
                        {
                            Location = new Point(Location.X + tennisGame.ball.Width, Location.Y);
                        }
                        if (Location.X + Size.Width / 2 > tennisGame.TablePanel.Width / 2)
                        {
                            BackColor = Color.Black;
                        }
                    }
                    else if (Location.X + Size.Width / 2 > tennisGame.ball.Location.X)
                    {
                        if (Location.X - tennisGame.ball.Width < 0)
                        {
                            Location = new Point(0, Location.Y);
                        }
                        else
                        {
                            Location = new Point(Location.X - tennisGame.ball.Width, Location.Y);
                        }
                        if (Location.X + Size.Width / 2 <= tennisGame.TablePanel.Width / 2)
                        {
                            BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);
                    if (!tennisGame.ball.userServe)
                    {
                        Thread.Sleep(1000);
                        tennisGame.ball.isStay = false;
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
