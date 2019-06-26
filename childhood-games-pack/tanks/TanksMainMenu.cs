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
    public enum TANK_TYPE : int {
        NONE = 0,
        LIGHT = 1,
        MEDIUM = 2,
        HEAVY = 3
    }

    public enum SPEED_LEVEL : int {
        NONE = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGHT = 3
    }

    public enum DIRECTION : int {
        U   = 1,
        D   = 2,
        L   = 3,
        R   = 4
    }

    public partial class TanksMainMenu : Form {
        private enum GAME_STATUS : int {
            LEVEL_SELECT = 1,
            GAME = 2
        }
        
        private MainMenuForm mainMenu;
        private GAME_STATUS gameStatus;
        private List<Button> buttons = new List<Button>();

        private Point userSpot = new Point(500, 600);
        private Point compSpot = new Point(500, 0);
        
        public TanksMainMenu(MainMenuForm mainMenu) {
            InitializeComponent();

            buttons.Add(level1Button);
            buttons.Add(level2Button);
            buttons.Add(level3Button);
            buttons.Add(level4Button);
            this.mainMenu = mainMenu;

            gameStatus = GAME_STATUS.LEVEL_SELECT;
            Size = new Size(350, 150);
        }

        private void configureGameField(int level) {
            gameStatus = GAME_STATUS.GAME;
            Size = new Size(1260, 660);

            foreach (Button b in buttons) {
                b.Hide();
            }

            switch (level) {
                case 1:
                    levelOneConfigure();
                    break;
            }
        }

        private void levelOneConfigure() {
            UserTank userTank = new UserTank(TANK_TYPE.HEAVY, SPEED_LEVEL.LOW, userSpot, this);
            CompTank comTank = new CompTank(TANK_TYPE.LIGHT, SPEED_LEVEL.HIGHT, compSpot);

            Controls.Add(userTank);
            Controls.Add(comTank);

            comTank.Show();
            userTank.Show();

            userTank.Focus();
        }

        private void TanksMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            mainMenu.Show();
        }

        private void level1Button_Click(object sender, EventArgs e) {
            configureGameField(1);
        }
    }
}
