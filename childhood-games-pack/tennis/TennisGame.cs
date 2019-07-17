using System.Drawing;
using System.Windows.Forms;

namespace childhood_games_pack.tennis
{
    public enum GAME_STATUS { IN_GAME, STOP, END };
    public partial class TennisGame : Form
    {
        readonly MainMenuForm mainMenu;
        Graphics Table { get; }
        public UserRacket UserRacket { get; }
        public CompRacket CompRacket { get; }
        public Ball Ball { get; }
        public GAME_STATUS GameStatus { get; set; } = GAME_STATUS.STOP;
        public int UserScore { get; set; }
        public int CompScore { get; set; }

        public TennisGame(MainMenuForm mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;

            Table = TablePanel.CreateGraphics();

            UserRacket = new UserRacket(this);
            CompRacket = new CompRacket(this);
            Ball = new Ball(this);

            TablePanel.Controls.Add(UserRacket);
            TablePanel.Controls.Add(CompRacket);
            TablePanel.Controls.Add(Ball);

            UserRacket.Show();
            CompRacket.Show();
            Ball.Show();

            UserRacket.Enabled = false;
            RestartButton.Focus();
        }

        private void StartGame()
        {
            GameStatus = GAME_STATUS.IN_GAME;
            UserScore = 0;
            CompScore = 0;
            Ball.Location = new Point(UserRacket.Left + UserRacket.Size.Width / 2 - Size.Width / 2, UserRacket.Top - UserRacket.Size.Height);

            UserScoreLabel.Text = UserScore.ToString();
            CompScoreLabel.Text = CompScore.ToString();

            CompRacket.Enabled = false;
            Ball.Enabled = false;

            RestartButton.Enabled = false;
            RestartButton.Hide();

            UserRacket.Enabled = true;
            UserRacket.Focus();
        }

        private void TennisMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameStatus = GAME_STATUS.END;
            mainMenu.Show();
        }

        private void TablePanel_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 3);
            Point leftPoint = new Point(0, TablePanel.Height / 2);
            Point rightPoint = new Point(TablePanel.Width, TablePanel.Height / 2);
            Table.DrawLine(whitePen, leftPoint, rightPoint);
        }

        private void RestartButton_Click(object sender, System.EventArgs e)
        {
            StartGame();
        }
    }

}
