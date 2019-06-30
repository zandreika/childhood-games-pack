using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis {
    public partial class Ball : Form {
        private TennisGame tennisGame;
        public bool isStay { get; set; } = false;
        public bool isLastKickUser { get; set; } = true;
        public Ball(TennisGame tennisGame) {
            InitializeComponent();
            SetTopLevel(false);
            this.tennisGame = tennisGame;
            isStay = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
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
                    if (Location.Y > tennisGame.compRacket.Size.Height) {
                        if (Top < tennisGame.compRacket.Bottom &&
                            Right >= tennisGame.compRacket.Left &&
                            Left <= tennisGame.compRacket.Right) 
                        {
                            Location = new Point(Location.X, tennisGame.compRacket.Size.Height);
                        }
                        else {
                            Location = new Point(Location.X, Location.Y - Size.Height);
                        }
                    }
                    else {
                        if (Right < tennisGame.compRacket.Left  || 
                            Left > tennisGame.compRacket.Right) {
                            Location = new Point(Location.X, 0);
                            MessageBox.Show("You win!");
                            isStay = true;
                        }
                        else {
                            isLastKickUser = false;
                        }
                    }

                }
                else {
                    if (Bottom < tennisGame.userRacket.Top) {
                        if (Bottom + Size.Height > tennisGame.userRacket.Top &&
                            Right >= tennisGame.userRacket.Left &&
                            Left <= tennisGame.userRacket.Right) 
                        {
                            Location = new Point(Location.X, tennisGame.userRacket.Location.Y - Size.Height);
                        }
                        else {
                            Location = new Point(Location.X, Location.Y + Size.Height);
                        }
                    }
                    else {
                        if (Location.X > tennisGame.userRacket.Location.X + tennisGame.userRacket.Size.Width ||
                            Right < tennisGame.userRacket.Left) {
                            Location = new Point(Location.X, Location.Y + Size.Height);
                            MessageBox.Show("You lose!");
                            isStay = true;
                        }
                        else {
                            isLastKickUser = true;
                        }
                    }
                }
                Thread.Sleep(40);
            }
        }
    }
}
