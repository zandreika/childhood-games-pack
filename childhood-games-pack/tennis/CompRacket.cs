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
    public partial class CompRacket : Form {
        private TennisGame tennisGame;
        public CompRacket(TennisGame tennisGame) {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;
            Size = tennisGame.userRacket.Size;
            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (tennisGame.gameStatus == GAME_STATUS.IN_GAME) {
                if (!tennisGame.ball.isStay) {
                    if (Location.X + Size.Width / 2 < tennisGame.ball.Location.X) {
                        if (Location.X + tennisGame.ball.Width < 0) {
                            Location = new Point(tennisGame.TablePanel.Right, Location.Y);
                        }
                        else {
                            Location = new Point(Location.X + tennisGame.ball.Width, Location.Y);
                        }
                    }
                    else if(Location.X + Size.Width / 2 > tennisGame.ball.Location.X) {
                        if (Location.X - tennisGame.ball.Width < 0) {
                            Location = new Point(0, Location.Y);
                        }
                        else {
                            Location = new Point(Location.X - tennisGame.ball.Width, Location.Y);
                        }
                    }
                }
                else {
                    Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);
                    if (!tennisGame.ball.isLastKickUser) {
                        Thread.Sleep(1000);
                        tennisGame.ball.isStay = false;
                    }
                }
                Thread.Sleep(250);
            }
        }
    }
}
