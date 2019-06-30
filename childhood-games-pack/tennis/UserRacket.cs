using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace childhood_games_pack.tennis {
    public partial class UserRacket : Form {
        private TennisGame tennisGame;

        public UserRacket(TennisGame tennisGame) {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;

            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, tennisGame.TablePanel.Height - Size.Height);
            
        }

        private void Racket_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                case Keys.A: {
                    Point newLocation = new Point(Location.X - 10, Location.Y);
                    if(newLocation.X < 0) {
                        newLocation.X = 0;
                    }
                    Location = newLocation;
                    break;
                }
                case Keys.Right:
                case Keys.D: {
                    Point newLocation = new Point(Location.X + 10, Location.Y);
                    if (newLocation.X + Size.Width > tennisGame.TablePanel.Width) {
                        newLocation.X = tennisGame.TablePanel.Width - Size.Width;
                    }
                    Location = newLocation;
                    break;
                }
                case Keys.Up:
                case Keys.W: {
                    Shoot();
                    break;
                }
            }
        }

        private void Shoot() {
            Point oldLocation = Location;
            Point newLocation = new Point(Location.X, Location.Y - 20);
            Location = newLocation;

            if (tennisGame.ball.isStay) {
                tennisGame.ball.isStay = false;
            }
            tennisGame.ball.Location  = new Point(Location.X + Size.Width / 2, Location.Y - Size.Height);
            Thread.Sleep(200);
            Location = oldLocation;
        }
    }
}
