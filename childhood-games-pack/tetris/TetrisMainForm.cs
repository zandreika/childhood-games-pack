using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;

namespace childhood_games_pack.tetris {
    public enum GAME_SPEED { LOW = 300, MEDIUM = 200, HIGH = 100, INCREDIBLE = 50 };

    public partial class TetrisMainForm : Form {
        private MainMenuForm mainMenu;
        private Graphics tetrisGamePanelCanvas;
        private Graphics nextFigurePanelCanvas;

        private Figure currentFigure;
        private Figure nextFigure;

        private List<KeyValuePair<Point, Brush>> cubes = new List<KeyValuePair<Point, Brush>>();
        private byte[,] occupatedMap = new byte[20,10];

        private int gameSpeed;
        private int gameScore;

        public TetrisMainForm(MainMenuForm mainMenu) {
            InitializeComponent();
            this.mainMenu = mainMenu;
            tetrisGamePanelCanvas = tetrisGamePanel.CreateGraphics();
            nextFigurePanelCanvas = nextFigurePanel.CreateGraphics();
        }

        public void StartGame() {
            gameSpeed = (int)GAME_SPEED.LOW;
            gameScore = 0;

            for (int i = 0; i < occupatedMap.GetLength(0); i++) {
                for (int j = 0; j < occupatedMap.GetLength(1); j++) {
                    occupatedMap[i, j] = 0;
                }
            }
            currentFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I);
            nextFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.J);

            restartGameButton.Hide();
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
                        restartGameButton.Show();
                        restartGameButton.Enabled = true;
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
                    // and shift upper cubes
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

                            for (int u = 0; u < cubes.Count; u++) {
                                var item = cubes[u];
                                if (item.Key.Y == i * currentFigure.CUBE_SIZE) {
                                    cubes.RemoveAt(u);
                                    u--;
                                }
                            }
                            for (int u = 0; u < cubes.Count; u++) {
                                var item = cubes[u];
                                if (item.Key.Y < i * currentFigure.CUBE_SIZE) {
                                    cubes[u] = new KeyValuePair<Point, Brush>(new Point(cubes[u].Key.X, cubes[u].Key.Y + currentFigure.CUBE_SIZE), cubes[u].Value);
                                }
                            }

                            for (int f1 = i; f1 > 0; f1--) {
                                for(int f2 = 0; f2 < occupatedMap.GetLength(1); f2++) {
                                    occupatedMap[f1, f2] = occupatedMap[f1 - 1, f2];
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
                    else if(gameSpeed > 3000) {
                        gameSpeed = (int)GAME_SPEED.HIGH;
                    }
                    else if(gameSpeed > 1500) {
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
                    }
                    nextFigurePanel.Invalidate();
                    ScoreLabel.Text = gameScore.ToString();
                }

                currentFigure.StepDown(cubes);

                Point startRedrawPoint = new Point(currentFigure.GetLeftmostCoordinate(), currentFigure.GetTopmostCoordinate() - currentFigure.CUBE_SIZE);
                Size sizeRedraw = new Size(currentFigure.GetRightmostCoordinate() - currentFigure.GetLeftmostCoordinate() + currentFigure.CUBE_SIZE, currentFigure.GetBottommostCoordinate() - currentFigure.GetTopmostCoordinate() + currentFigure.CUBE_SIZE);
                Rectangle invRect = new Rectangle(startRedrawPoint, sizeRedraw);

                tetrisGamePanel.Invalidate(invRect);
                Thread.Sleep(gameSpeed);
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

        private void RestartGameButton_Click(object sender, EventArgs e) {
            cubes = new List<KeyValuePair<Point, Brush>>();
            tetrisGamePanel.Invalidate();
            restartGameButton.Enabled = false;
            StartGame();
        }

        private void NextFigurePanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < nextFigure.cubes.Count; i++) {
                Rectangle rect = new Rectangle(new Point(nextFigure.cubes[i].X - 40, nextFigure.cubes[i].Y + 100), new Size(nextFigure.CUBE_SIZE, nextFigure.CUBE_SIZE));
                nextFigurePanelCanvas.FillRectangle(nextFigure.GetBrushByFigureType(), rect);
                nextFigurePanelCanvas.DrawRectangle(Pens.Black, rect);
            }
        }
    }
}
