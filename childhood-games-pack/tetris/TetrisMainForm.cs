using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;

namespace childhood_games_pack.tetris {
    public partial class TetrisMainForm : Form {
        private MainMenuForm mainMenu;
        private Graphics tetrisGamePanelCanvas;
        
        private Figure currentFigure;

        //private List<Point> cubes = new List<Point>();

        private List<KeyValuePair<Point, Brush>> cubes = new List<KeyValuePair<Point, Brush>>();

        public TetrisMainForm(MainMenuForm mainMenu) {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.tetrisGamePanelCanvas = tetrisGamePanel.CreateGraphics();
        }

        public void StartGame() {
            currentFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I);
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                if (currentFigure.isStay) {
                    if(currentFigure.GetTopmostCoordinate() <= 0) {
                        MessageBox.Show("You lose");
                        return;
                    }

                    for(int i = 0; i < currentFigure.cubes.Count; i++) {
                        KeyValuePair<Point, Brush> cube = new KeyValuePair<Point, Brush>(currentFigure.cubes[i], currentFigure.GetBrushByFigureType());
                        cubes.Add(cube);
                    }
                    Random random = new Random();
                    currentFigure = new Figure(tetrisGamePanel, FIGURE_TYPE.I + random.Next() % Enum.GetNames(typeof(FIGURE_TYPE)).Length);
                }
                currentFigure.StepDown(cubes);
                tetrisGamePanel.Refresh();
                Thread.Sleep(500);
            }

        }

        private void TetrisMainForm_FormClosed(object sender, FormClosedEventArgs e) {
            mainMenu.Show();
        }


        private void TetrisMainForm_KeyDown(object sender, KeyEventArgs e) {
            if (!currentFigure.isStay) {
                switch (e.KeyCode) {
                    case Keys.A:
                    case Keys.Left:
                        currentFigure.StepLeft(cubes);
                        tetrisGamePanel.Invalidate();
                        break;

                    case Keys.D:
                    case Keys.Right:
                        currentFigure.StepRight(cubes);
                        tetrisGamePanel.Invalidate();
                        break;

                    case Keys.S:
                    case Keys.Down:
                        currentFigure.StepDown(cubes);
                        tetrisGamePanel.Invalidate();
                        break;

                    default:
                        break;
                }
            }
        }

        private void TetrisGamePanel_Paint(object sender, PaintEventArgs e) {

            for (int i = 0; i < currentFigure.cubes.Count; i++) {
                Rectangle rect = new Rectangle(currentFigure.cubes[i], new Size(currentFigure.CUBE_SIZE, currentFigure.CUBE_SIZE));
                tetrisGamePanelCanvas.DrawRectangle(Pens.Black, rect);
                tetrisGamePanelCanvas.FillRectangle(currentFigure.GetBrushByFigureType(), rect);
            }
            for (int i = 0; i < cubes.Count; i++) {
                Rectangle rect = new Rectangle(cubes[i].Key, new Size(currentFigure.CUBE_SIZE, currentFigure.CUBE_SIZE));
                tetrisGamePanelCanvas.DrawRectangle(Pens.Black, rect);
                tetrisGamePanelCanvas.FillRectangle(cubes[i].Value, rect);
            }
        }
    }
}
