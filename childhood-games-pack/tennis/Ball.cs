using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis {
    public partial class Ball : Form {
        private TennisGame tennisGame;
        public bool isStay = false;
        public bool isLastKickUser = true;
        public Ball(TennisGame tennisGame) {
            InitializeComponent();
            SetTopLevel(false);
            this.tennisGame = tennisGame;
            isStay = true;

            Size = new Size(tennisGame.userRacket.Size.Width / 3, tennisGame.userRacket.Size.Height);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (tennisGame.gameStatus == GAME_STATUS.IN_GAME) {
                if (isStay) {
                    if (isLastKickUser) {
                        Location = new Point(tennisGame.userRacket.Left + tennisGame.userRacket.Size.Width / 2 - Size.Width / 2, tennisGame.userRacket.Top - tennisGame.userRacket.Size.Height);
                    }
                    else {
                        Location = new Point(tennisGame.compRacket.Left + tennisGame.compRacket.Size.Width / 2 - Size.Width / 2, tennisGame.compRacket.Top + tennisGame.compRacket.Size.Height);
                    }
                    continue;
                }

                if (isLastKickUser) {
                    if (Top > tennisGame.compRacket.Bottom) { // if the ball going from player to computer
                        if (Top - Size.Height < tennisGame.compRacket.Bottom &&
                            Right >= tennisGame.compRacket.Left &&
                            Left <= tennisGame.compRacket.Right) { // for not to cross computer racket
                            Location = new Point(Location.X, tennisGame.compRacket.Bottom);
                        }
                        else {
                            Location = new Point(Location.X + tennisGame.userRacket.lastKickXTrajectory, Location.Y - Size.Height);
                        }
                    }
                    else { // if the ball reached computer racket
                        if (Right < tennisGame.compRacket.Left  || 
                            Left > tennisGame.compRacket.Right) { // if computer racket can't kick the ball
                            if (Right > tennisGame.TablePanel.Width || Left < 0) {
                                tennisGame.CompScore++;
                                tennisGame.CompScoreLabel.Text = tennisGame.CompScore.ToString();
                                isLastKickUser = false;
                            }
                            else {
                                tennisGame.UserScore++;
                                tennisGame.UserScoreLabel.Text = tennisGame.UserScore.ToString();
                                isLastKickUser = false;
                            }
                            Location = new Point(Location.X, 0);
                            //MessageBox.Show("You win!");
                            isStay = true;
                        }
                        else {
                            isLastKickUser = false;
                        }
                    }

                }
                else {
                    if (Bottom < tennisGame.userRacket.Top) { // if the ball going from computer to player 
                        if (Bottom + Size.Height > tennisGame.userRacket.Top &&
                            Right >= tennisGame.userRacket.Left &&
                            Left <= tennisGame.userRacket.Right) // for not to cross user racket
                        {
                            Location = new Point(Location.X, tennisGame.userRacket.Top - Size.Height);
                        }
                        else {
                            Location = new Point(Location.X, Location.Y + Size.Height);
                        }
                    }
                    else { // if the ball reached user racket
                        if (Left > tennisGame.userRacket.Right ||
                            Right < tennisGame.userRacket.Left) { // if user racket can't kick the ball
                            if (Right > tennisGame.TablePanel.Width || Left < 0) {
                                tennisGame.UserScore++;
                                tennisGame.UserScoreLabel.Text = tennisGame.UserScore.ToString();
                                isLastKickUser = true;
                            }
                            else {
                                tennisGame.CompScore++;
                                tennisGame.CompScoreLabel.Text = tennisGame.CompScore.ToString();
                                isLastKickUser = false;
                            }
                            Location = new Point(Location.X, Location.Y + Size.Height);
                            //MessageBox.Show("You lose!");
                            isStay = true;
                        }
                        else {
                            isLastKickUser = true;
                            int trajectory = - ((Left + Size.Width / 2) - (tennisGame.userRacket.Left + tennisGame.userRacket.Size.Width / 2)) / 3;
                            tennisGame.userRacket.lastKickXTrajectory = trajectory;
                        }
                    }
                }
                Thread.Sleep(40);
            }
        }
    }
}
