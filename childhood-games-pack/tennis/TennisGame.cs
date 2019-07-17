using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public enum GAME_STATUS { IN_GAME, STOP, END };
    public partial class TennisGame : Form
    {
        private MainMenuForm mainMenu;
        private Graphics table { get; }
        public UserRacket userRacket { get; }
        public CompRacket compRacket { get; }
        public Ball ball { get; }
        public GAME_STATUS gameStatus { get; set; } = GAME_STATUS.STOP;
        public int UserScore { get; set; }
        public int CompScore { get; set; }

        public TennisGame(MainMenuForm mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;

            table = TablePanel.CreateGraphics();

            userRacket = new UserRacket(this);
            compRacket = new CompRacket(this);
            ball = new Ball(this);

            TablePanel.Controls.Add(userRacket);
            TablePanel.Controls.Add(compRacket);
            TablePanel.Controls.Add(ball);

            userRacket.Show();
            compRacket.Show();
            ball.Show();

            userRacket.Enabled = false;
            RestartButton.Focus();
        }

        private void StartGame()
        {
            gameStatus = GAME_STATUS.IN_GAME;
            UserScore = 0;
            CompScore = 0;
            ball.Location = new Point(userRacket.Left + userRacket.Size.Width / 2 - Size.Width / 2, userRacket.Top - userRacket.Size.Height);

            UserScoreLabel.Text = UserScore.ToString();
            CompScoreLabel.Text = CompScore.ToString();

            compRacket.Enabled = false;
            ball.Enabled = false;

            RestartButton.Enabled = false;
            RestartButton.Hide();

            userRacket.Enabled = true;
            userRacket.Focus();
        }

        private void TennisMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameStatus = GAME_STATUS.END;
            mainMenu.Show();
        }

        private void TablePanel_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 3);
            Point leftPoint = new Point(0, TablePanel.Height / 2);
            Point rightPoint = new Point(TablePanel.Width, TablePanel.Height / 2);
            table.DrawLine(whitePen, leftPoint, rightPoint);
        }

        private void RestartButton_Click(object sender, System.EventArgs e)
        {
            StartGame();
        }
    }

}
