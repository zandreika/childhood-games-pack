using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace childhood_games_pack.tennis {
    public partial class CompRacket : Form {
        private TennisGame tennisGame;
        public CompRacket(TennisGame tennisGame) {
            InitializeComponent();
            SetTopLevel(false);

            this.tennisGame = tennisGame;

            Location = new Point(tennisGame.TablePanel.Width / 2 - Size.Width / 2, 0);

        }
    }
}
