using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace childhood_games_pack.snake {
    public partial class SnakeGame : Form {
        private MainMenuForm mainMenu;
        private Point foodPoint;
        private ArrayList snakeBlocks;

        Random random = new Random();

        private Graphics snakePanelCanvas;

        private int BLOCK_SIZE = 20;

        public SnakeGame(MainMenuForm mainMenu) {
            InitializeComponent();

            this.mainMenu = mainMenu;

            snakePanel.Hide();
            scoreLabel.Hide();
            resLabel.Hide();
            pauseGameButton.Hide();
            endGameButton.Hide();
        }

        private void startGameButton_Click(object sender, EventArgs e) {
            startGameButton.Hide();

            snakePanel.Show();
            scoreLabel.Show();
            resLabel.Show();
            pauseGameButton.Show();
            endGameButton.Show();

            snakePanelCanvas = snakePanel.CreateGraphics();

            Point startBlock = createStartSnakeBlocks();
            Rectangle snake;
            snakeBlocks = new ArrayList();
            for (int i = 0; i < 3; i++) {
                startBlock.X = startBlock.X - BLOCK_SIZE;
                snakeBlocks.Add(startBlock);//храним координаты блоков

                snake = new Rectangle(startBlock, new Size(BLOCK_SIZE, BLOCK_SIZE));
                //snakePanelCanvas.FillRectangle(Brushes.DarkCyan, snake);
                snakePanelCanvas.DrawRectangle(Pens.DarkCyan, snake);
            }

            createFood();
        }

        private void SnakeGame_FormClosing(object sender, FormClosingEventArgs e) {
            mainMenu.Show();
        }

        private bool сheckStartSnake(Point head) {
            if (head.X - 2 * BLOCK_SIZE < snakePanel.Left ||
                head.X + BLOCK_SIZE > snakePanel.Right ||
                head.Y + BLOCK_SIZE > snakePanel.Bottom ||
                head.Y < snakePanel.Top) {
                return false;
            }
            return true;
        }

        private Point createStartSnakeBlocks() {
            Point block = new Point(random.Next() % (snakePanel.Width - BLOCK_SIZE), random.Next() % (snakePanel.Height - BLOCK_SIZE));
            while (!сheckStartSnake(block)) {
                block.X = random.Next() % snakePanel.Width;
                block.Y = random.Next() % snakePanel.Height;
            }

            return block;
        }

        private void createFood() {
            foodPoint = new Point(random.Next() % (snakePanel.Width - BLOCK_SIZE), random.Next() % (snakePanel.Height - BLOCK_SIZE));

            while (snakeBlocks.Contains(foodPoint)) {
                foodPoint.X = random.Next() % (snakePanel.Width - BLOCK_SIZE);
                foodPoint.Y = random.Next() % (snakePanel.Height - BLOCK_SIZE);
            }

            Rectangle food = new Rectangle(foodPoint, new Size(BLOCK_SIZE, BLOCK_SIZE));

            snakePanelCanvas.FillEllipse(Brushes.Red, food);
            snakePanelCanvas.DrawEllipse(Pens.Red, food);
        }
    }
}