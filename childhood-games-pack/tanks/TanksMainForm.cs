using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace childhood_games_pack.tanks {
    public partial class TanksMainForm : Form {
        private MainMenuForm mainMenu;

        public TanksMainForm(MainMenuForm mainMenu) {
            InitializeComponent();

            this.mainMenu = mainMenu;
        }

        private void TanksMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            mainMenu.Show();
        }
    }
}
