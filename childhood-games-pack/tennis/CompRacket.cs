using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public partial class CompRacket : Form
    {
        readonly TennisGame tennisGame;
        public CompRacket(TennisGame tennisGame)
        {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;
            Size = tennisGame.UserRacket.Size;
            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("BW");
            Thread.Sleep(200);
            while (tennisGame.GameStatus != GAME_STATUS.END)
            {
                if (!tennisGame.Ball.isStay)
                {
                    if (Location.X + Size.Width / 2 < tennisGame.Ball.Location.X)
                    {
                        if (Right + tennisGame.Ball.Width > tennisGame.TablePanel.Width)
                        {
                            Location = new Point(tennisGame.TablePanel.Width - Size.Width, Location.Y);
                        }
                        else
                        {
                            Location = new Point(Location.X + tennisGame.Ball.Width, Location.Y);
                        }
                        if (Location.X + Size.Width / 2 > tennisGame.TablePanel.Width / 2)
                        {
                            BackColor = Color.Black;
                        }
                    }
                    else if (Location.X + Size.Width / 2 > tennisGame.Ball.Location.X)
                    {
                        if (Location.X - tennisGame.Ball.Width < 0)
                        {
                            Location = new Point(0, Location.Y);
                        }
                        else
                        {
                            Location = new Point(Location.X - tennisGame.Ball.Width, Location.Y);
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
                    if (!tennisGame.Ball.userServe)
                    {
                        Thread.Sleep(1000);
                        tennisGame.Ball.isStay = false;
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
