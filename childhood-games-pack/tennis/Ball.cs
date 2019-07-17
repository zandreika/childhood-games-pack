using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public partial class Ball : Form
    {
        readonly TennisGame tennisGame;
        public bool isStay = false;
        public bool isLastKickUser = true;
        public bool userServe = true;
        public Ball(TennisGame tennisGame)
        {
            InitializeComponent();
            SetTopLevel(false);
            this.tennisGame = tennisGame;
            isStay = true;

            Size = new Size(tennisGame.UserRacket.Size.Width / 3, tennisGame.UserRacket.Size.Height);
            Location = new Point(tennisGame.TablePanel.Width / 2, tennisGame.TablePanel.Height / 2 - Size.Height / 2);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (tennisGame.GameStatus != GAME_STATUS.END)
            {
                if (isStay)
                {
                    if (tennisGame.GameStatus == GAME_STATUS.IN_GAME)
                    {
                        if (tennisGame.UserScore >= 11 && tennisGame.UserScore - 2 >= tennisGame.CompScore)
                        {
                            MessageBox.Show("You win!", "Congrats");
                            tennisGame.GameStatus = GAME_STATUS.STOP;

                            tennisGame.RestartButton.Enabled = true;
                            tennisGame.RestartButton.Show();
                            Location = new Point(tennisGame.TablePanel.Width / 2, tennisGame.TablePanel.Height / 2 - Size.Height / 2);
                            continue;
                        }
                        else if (tennisGame.CompScore >= 11 && tennisGame.CompScore - 2 >= tennisGame.UserScore)
                        {
                            MessageBox.Show("You lose!");
                            tennisGame.GameStatus = GAME_STATUS.STOP;

                            tennisGame.RestartButton.Enabled = true;
                            tennisGame.RestartButton.Show();
                            Location = new Point(tennisGame.TablePanel.Width / 2, tennisGame.TablePanel.Height / 2 - Size.Height / 2);
                            continue;
                        }

                        if ((tennisGame.UserScore + tennisGame.CompScore) % 4 < 2)
                        {
                            Location = new Point(tennisGame.UserRacket.Left + tennisGame.UserRacket.Size.Width / 2 - Size.Width / 2, tennisGame.UserRacket.Top - tennisGame.UserRacket.Size.Height);
                            userServe = true;
                        }
                        else
                        {
                            Location = new Point(tennisGame.CompRacket.Left + tennisGame.CompRacket.Size.Width / 2 - Size.Width / 2, tennisGame.CompRacket.Top + tennisGame.CompRacket.Size.Height);
                            userServe = false;
                        }
                    }
                    continue;
                }

                if (isLastKickUser)
                {
                    if (Top > tennisGame.CompRacket.Bottom)
                    { // if the ball going from player to computer
                        if (Top - Size.Height < tennisGame.CompRacket.Bottom &&
                            Right >= tennisGame.CompRacket.Left &&
                            Left <= tennisGame.CompRacket.Right)
                        { // for not to cross computer racket
                            Location = new Point(Location.X, tennisGame.CompRacket.Bottom);
                        }
                        else
                        {
                            Location = new Point(Location.X + tennisGame.UserRacket.lastKickXTrajectory, Location.Y - Size.Height);
                        }
                    }
                    else
                    { // if the ball reached computer racket
                        if (Right < tennisGame.CompRacket.Left ||
                            Left > tennisGame.CompRacket.Right)
                        { // if computer racket can't kick the ball
                            if (Right > tennisGame.TablePanel.Width || Left < 0)
                            {
                                tennisGame.CompScore++;
                                tennisGame.CompScoreLabel.Text = tennisGame.CompScore.ToString();
                                isLastKickUser = false;
                            }
                            else
                            {
                                tennisGame.UserScore++;
                                tennisGame.UserScoreLabel.Text = tennisGame.UserScore.ToString();
                                isLastKickUser = true;
                            }
                            Location = new Point(Location.X, 0);
                            isStay = true;
                        }
                        else
                        {
                            isLastKickUser = false;
                        }
                    }

                }
                else
                {
                    if (Bottom < tennisGame.UserRacket.Top)
                    { // if the ball going from computer to player 
                        if (Bottom + Size.Height > tennisGame.UserRacket.Top &&
                            Right >= tennisGame.UserRacket.Left &&
                            Left <= tennisGame.UserRacket.Right) // for not to cross user racket
                        {
                            Location = new Point(Location.X, tennisGame.UserRacket.Top - Size.Height);
                        }
                        else
                        {
                            Location = new Point(Location.X, Location.Y + Size.Height);
                        }
                    }
                    else
                    { // if the ball reached user racket
                        if (Left > tennisGame.UserRacket.Right ||
                            Right < tennisGame.UserRacket.Left)
                        { // if user racket can't kick the ball
                            if (Right > tennisGame.TablePanel.Width || Left < 0)
                            {
                                tennisGame.UserScore++;
                                tennisGame.UserScoreLabel.Text = tennisGame.UserScore.ToString();
                                isLastKickUser = true;
                            }
                            else
                            {
                                tennisGame.CompScore++;
                                tennisGame.CompScoreLabel.Text = tennisGame.CompScore.ToString();
                                isLastKickUser = false;
                            }
                            Location = new Point(Location.X, Location.Y + Size.Height);
                            isStay = true;
                        }
                        else
                        {
                            isLastKickUser = true;
                            int trajectory = -((Left + Size.Width / 2) - (tennisGame.UserRacket.Left + tennisGame.UserRacket.Size.Width / 2)) / 3;
                            tennisGame.UserRacket.lastKickXTrajectory = trajectory;
                        }
                    }
                }
                Thread.Sleep(40);
            }
        }
    }
}
