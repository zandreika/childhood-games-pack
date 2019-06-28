using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;

namespace childhood_games_pack.tetris {
    public enum GAME_SPEED { LOW = 250, MEDIUM = 200, HIGH = 150, INCREDIBLE = 100 };

    public partial class TetrisMainForm : Form {
        private MainMenuForm mainMenu;
        private Graphics tetrisGamePanelCanvas;
        private Graphics nextFigurePanelCanvas;

        private Figure currentFigure;
        private Figure nextFigure;

        private List<KeyValuePair<Point, Brush>> cubes;
        private byte[,] occupatedMap = new byte[20,10];

        private int gameSpeed;
        private int gameScore;

        public TetrisMainForm(MainMenuForm mainMenu) {
            InitializeComponent();
            this.mainMenu = mainMenu;

            tetrisGamePanelCanvas = tetrisGamePanel.CreateGraphics();
            nextFigurePanelCanvas = nextFigurePanel.CreateGraphics();

            tetrisGamePanel.Hide();
            nextFigurePanel.Hide();
            ScoreHeaderLabel.Hide();

            StartGameButton.Show();
            InfoButton.Show();

            this.Size = new Size(300, 300);
        }
        
        public void StartGame() {
            this.Size = new Size(500, 500);
            gameSpeed = (int)GAME_SPEED.LOW;
            gameScore = 0;

            for (int i = 0; i < occupatedMap.GetLength(0); i++) {
                for (int j = 0; j < occupatedMap.GetLength(1); j++) {
                    occupatedMap[i, j] = 0;
                }
            }

            Random random = new Random();
            currentFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I + random.Next() % Enum.GetNames(typeof(FIGURE_TYPE)).Length);
            nextFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I + random.Next() % Enum.GetNames(typeof(FIGURE_TYPE)).Length);

            StartGameButton.Hide();
            nextFigurePanel.Invalidate();
            ScoreLabel.Text = gameScore.ToString();

            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                if (backgroundWorker1.CancellationPending) {
                    return;
                }

                if (currentFigure.isStay) {
                    if(currentFigure.GetTopmostCoordinate() <= 0) {
                        MessageBox.Show("You lose");
                        StartGameButton.Location = new Point(nextFigurePanel.Location.X, nextFigurePanel.Location.Y + nextFigurePanel.Height + 20);
                        StartGameButton.Enabled = true;
                        StartGameButton.Show();
                        return;
                    }

                    int rows_to_del = 0;

                    // add current figure cubes into panel cubes list
                    for(int i = 0; i < currentFigure.cubes.Count; i++) {
                        KeyValuePair<Point, Brush> cube = new KeyValuePair<Point, Brush>(currentFigure.cubes[i], currentFigure.GetBrushByFigureType());
                        occupatedMap[cube.Key.Y / currentFigure.CUBE_SIZE, cube.Key.X / currentFigure.CUBE_SIZE] = 1;
                        cubes.Add(cube);
                    }

                    // find 10 cubes in a row
                    // and shift upper cubes down
                    for (int i = occupatedMap.GetLength(0) - 1; i > 0; i--) {
                        int cubes_in_row = 0;
                        for (int j = 0; j < occupatedMap.GetLength(1); j++) {
                            if (occupatedMap[i, j] == 1)
                                cubes_in_row++;
                            else
                                break;
                        }
                        if (cubes_in_row == occupatedMap.GetLength(1)) {
                            rows_to_del++;

                            for (int k = 0; k < occupatedMap.GetLength(1); k++) {
                                occupatedMap[i, k] = 0;
                                occupatedMap[0, k] = 0;
                            }

                            //delete cubes from line
                            for (int l = 0; l < cubes.Count; l++) {
                                var item = cubes[l];
                                if (item.Key.Y == i * currentFigure.CUBE_SIZE) {
                                    cubes.RemoveAt(l);
                                    l--;
                                }
                            }

                            //shift upper cubes down
                            for (int u = 0; u < cubes.Count; u++) {
                                var item = cubes[u];
                                if (item.Key.Y < i * currentFigure.CUBE_SIZE) {
                                    cubes[u] = new KeyValuePair<Point, Brush>(new Point(cubes[u].Key.X, cubes[u].Key.Y + currentFigure.CUBE_SIZE), cubes[u].Value);
                                }
                            }

                            //change map
                            for (int x = i; x > 0; x--) {
                                for(int y = 0; y < occupatedMap.GetLength(1); y++) {
                                    occupatedMap[x, y] = occupatedMap[x - 1, y];
                                }
                            }

                            i++;
                        }
                            
                    }

                    switch (rows_to_del) {
                        case 1:
                            gameScore += 100;
                            break;
                        case 2:
                            gameScore += 300;
                            break;
                        case 3:
                            gameScore += 700;
                            break;
                        case 4:
                            gameScore += 1500;
                            break;
                        default:
                            break;
                    }

                    if(gameScore > 5000) {
                        gameSpeed = (int)GAME_SPEED.INCREDIBLE;
                    }
                    else if(gameScore >= 3000) {
                        gameSpeed = (int)GAME_SPEED.HIGH;
                    }
                    else if(gameScore >= 1000) {
                        gameSpeed = (int)GAME_SPEED.MEDIUM;
                    }
                    else {
                        gameSpeed = (int)GAME_SPEED.LOW;
                    }

                    Random random = new Random();
                    currentFigure = nextFigure;
                    nextFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I + random.Next() % Enum.GetNames(typeof(FIGURE_TYPE)).Length);

                    if (rows_to_del > 0) {
                        tetrisGamePanel.Invalidate();
                        ScoreLabel.Text = gameScore.ToString();
                    }
                    nextFigurePanel.Invalidate();  
                }

                Thread.Sleep(gameSpeed);
                currentFigure.StepDown(cubes);

                Point startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate(), currentFigure.GetTopmostCoordinate() - currentFigure.CUBE_SIZE);
                Size sizeRedraw = new Size(currentFigure.GetRightmostCoordinate() - currentFigure.GetLeftmostCoordinate() + currentFigure.CUBE_SIZE, currentFigure.GetBottommostCoordinate() - currentFigure.GetTopmostCoordinate() + currentFigure.CUBE_SIZE);
                Rectangle invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                tetrisGamePanel.Invalidate(invRect);
            }

        }

        private void TetrisMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            backgroundWorker1.CancelAsync();
            mainMenu.Show();
        }


        private void TetrisMainForm_KeyDown(object sender, KeyEventArgs e) {
            if (!currentFigure.isStay) {
                Point startRedrawPoint;
                Size sizeRedraw;
                Rectangle invRect;

                switch (e.KeyCode) {
                    case Keys.A:
                    case Keys.Left:
                        currentFigure.StepLeft(cubes);

                        startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate(), currentFigure.GetTopmostCoordinate());
                        sizeRedraw = new Size(currentFigure.GetRightmostCoordinate() - currentFigure.GetLeftmostCoordinate() + 2*currentFigure.CUBE_SIZE, currentFigure.GetBottommostCoordinate() - currentFigure.GetTopmostCoordinate() + currentFigure.CUBE_SIZE);
                        invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                        tetrisGamePanel.Invalidate(invRect);
                        break;

                    case Keys.D:
                    case Keys.Right:
                        currentFigure.StepRight(cubes);

                        startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate() - currentFigure.CUBE_SIZE, currentFigure.GetTopmostCoordinate());
                        sizeRedraw = new Size(currentFigure.GetRightmostCoordinate() - currentFigure.GetLeftmostCoordinate() + currentFigure.CUBE_SIZE, currentFigure.GetBottommostCoordinate() - currentFigure.GetTopmostCoordinate() + currentFigure.CUBE_SIZE);
                        invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                        tetrisGamePanel.Invalidate(invRect);
                        break;

                    case Keys.S:
                    case Keys.Down:
                        currentFigure.StepDown(cubes);

                        startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate(), currentFigure.GetTopmostCoordinate() - currentFigure.CUBE_SIZE);
                        sizeRedraw = new Size(currentFigure.GetRightmostCoordinate() - currentFigure.GetLeftmostCoordinate() + currentFigure.CUBE_SIZE, currentFigure.GetBottommostCoordinate() - currentFigure.GetTopmostCoordinate() + currentFigure.CUBE_SIZE);
                        invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                        tetrisGamePanel.Invalidate(invRect);
                        break;

                    case Keys.W:
                    case Keys.Up:
                        if (currentFigure.Rotate(cubes)) {
                            startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate() - 2 * currentFigure.CUBE_SIZE, currentFigure.GetTopmostCoordinate() - 3 * currentFigure.CUBE_SIZE);
                            sizeRedraw = new Size(8 * currentFigure.CUBE_SIZE, 8 * currentFigure.CUBE_SIZE);
                            invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                            tetrisGamePanel.Invalidate(invRect);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void TetrisGamePanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < currentFigure.cubes.Count; i++) {
                Rectangle rect = new Rectangle(currentFigure.cubes[i], new Size(currentFigure.CUBE_SIZE, currentFigure.CUBE_SIZE));
                tetrisGamePanelCanvas.FillRectangle(currentFigure.GetBrushByFigureType(), rect);
                tetrisGamePanelCanvas.DrawRectangle(Pens.Black, rect);  
            }
            for (int i = 0; i < cubes.Count; i++) {
                Rectangle rect = new Rectangle(cubes[i].Key, new Size(currentFigure.CUBE_SIZE, currentFigure.CUBE_SIZE));
                tetrisGamePanelCanvas.FillRectangle(cubes[i].Value, rect);
                tetrisGamePanelCanvas.DrawRectangle(Pens.Black, rect);
            }
        }

        private void NextFigurePanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < nextFigure.cubes.Count; i++) {
                Rectangle rect = new Rectangle(new Point(nextFigure.cubes[i].X - 40, nextFigure.cubes[i].Y + 100), new Size(nextFigure.CUBE_SIZE, nextFigure.CUBE_SIZE));
                nextFigurePanelCanvas.FillRectangle(nextFigure.GetBrushByFigureType(), rect);
                nextFigurePanelCanvas.DrawRectangle(Pens.Black, rect);
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e) {
            tetrisGamePanel.Show();
            nextFigurePanel.Show();
            ScoreHeaderLabel.Show();

            StartGameButton.Enabled = false;
            InfoButton.Enabled = false;
            StartGameButton.Hide();
            InfoButton.Hide();

            cubes = new List<KeyValuePair<Point, Brush>>();
            tetrisGamePanel.Invalidate();
            StartGame();
        }

        private void InfoButton_Click(object sender, EventArgs e) {
            String information = "Control:\n";
            information += "to move the figure left press A or left Arrow\n";
            information += "to move the figure right press D or right Arrow\n";
            information += "to speed up figure press S or arrow down\n";
            information += "to rotate figeure press W or arrow up\n\n";
            information += "Adding points:\n";
            information += "If you collect 1 row - to score added 100 points\n";
            information += "If you collect 2 rows - to score added 300 points\n";
            information += "If you collect 3 rows - to score added 700 points\n";
            information += "If you collect 4 rows - to score added 1500 points\n";

            MessageBox.Show(information);
        }
    }
}
