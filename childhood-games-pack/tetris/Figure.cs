using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace childhood_games_pack.tetris {
    public enum FIGURE_TYPE { I, J, L, O, S, T, Z };
    

    public class Figure {
        public Panel workspace;
        private FIGURE_TYPE figureType;
        public List<Point> cubes;
        public bool isStay = false;
        public int CUBE_SIZE = 20;

        public Figure(Panel workspace, FIGURE_TYPE figureType) {
            this.workspace = workspace;
            this.figureType = figureType;

            cubes = CreateFigure(figureType);
        }


        private List<Point> CreateFigure(FIGURE_TYPE figureType) {
            List<Point> cubes = new List<Point>();

            switch (figureType) {
                case FIGURE_TYPE.I: {
                    for (int i = 0; i < 4; i++) {
                        cubes.Add(new Point(4* CUBE_SIZE, (i-4)* CUBE_SIZE));
                    }

                    break;
                }
                case FIGURE_TYPE.J: {
                    cubes.Add(new Point(3 * CUBE_SIZE, -2* CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.L: {
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.O: {
                    cubes.Add(new Point(4 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.S: {
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.T: {
                    cubes.Add(new Point(4 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.Z: {
                    cubes.Add(new Point(4 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(6 * CUBE_SIZE, -CUBE_SIZE));

                    break;
                }

                default:
                    throw new Exception("Wrong type of Figure");
            }

            return cubes;
        }


        public Brush GetBrushByFigureType() {
            switch (figureType) {
                case FIGURE_TYPE.I: {
                    return Brushes.Aqua;
                }
                case FIGURE_TYPE.J: {
                    return Brushes.Blue;
                }
                case FIGURE_TYPE.L: {
                    return Brushes.Orange;
                }
                case FIGURE_TYPE.O: {
                    return Brushes.Yellow;
                }
                case FIGURE_TYPE.S: {
                    return Brushes.Green;
                }
                case FIGURE_TYPE.T: {
                    return Brushes.Purple;
                }
                case FIGURE_TYPE.Z: {
                    return Brushes.Red;
                }
                default:
                    throw new Exception("Wrong type of Figure");
            }
        }


        public int GetTopmostCoordinate() {
            int res = cubes[0].Y;
            for(int i = 1; i < cubes.Count; i++) {
                if(cubes[i].Y < res) {
                    res = cubes[i].Y;
                }
            }
            return res;
        }

        private int GetLeftmostCubeIndex() {
            int res = 0;
            for(int i = 1; i < cubes.Count; i++) {
                if(cubes[i].X < cubes[res].X) {
                    res = i;
                }
            }
            return res;
        }


        private int GetRightmostCubeIndex() {
            int res = 0;
            for (int i = 1; i < cubes.Count; i++) {
                if (cubes[i].X > cubes[res].X) {
                    res = i;
                }
            }
            return res;
        }


        private int GetBottommostCubeIndex() {
            int res = 0;
            for (int i = 1; i < cubes.Count; i++) {
                if (cubes[i].Y > cubes[res].Y) {
                    res = i;
                }
            }
            return res;
        }


        public void StepLeft(List<KeyValuePair<Point, Brush>> ocсupiedCubes) {
            int leftIndex = GetLeftmostCubeIndex();
            if (cubes[leftIndex].X <= workspace.Left) {
                return;
            }
            for (int i = 0; i < cubes.Count; i++) {
                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                    if (cubes[i].X - CUBE_SIZE == ocсupiedCubes[j].Key.X && cubes[i].Y == ocсupiedCubes[j].Key.Y) {
                        return;
                    }
                }
            }
            for (int i = 0; i < cubes.Count; i++) {
                cubes[i] = new Point(cubes[i].X - CUBE_SIZE, cubes[i].Y);
            }
        }


        public void StepRight(List<KeyValuePair<Point, Brush>> ocсupiedCubes) {
            int rightIndex = GetRightmostCubeIndex();
            if (cubes[rightIndex].X + 2*CUBE_SIZE >= workspace.Right) {
                return;
            }
            for (int i = 0; i < cubes.Count; i++) {
                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                    if (cubes[i].X + CUBE_SIZE == ocсupiedCubes[j].Key.X && cubes[i].Y == ocсupiedCubes[j].Key.Y) {
                        return;
                    }
                }
            }
            for (int i = 0; i < cubes.Count; i++) {
                cubes[i] = new Point(cubes[i].X + CUBE_SIZE, cubes[i].Y);
            }
        }


        public void StepDown(List<KeyValuePair<Point, Brush>> ocсupiedCubes) {
            int bottomIndex = GetBottommostCubeIndex();
            if(cubes[bottomIndex].Y + 2*CUBE_SIZE >= workspace.Bottom ) {
                isStay = true;
                return;
            }
            for(int i = 0; i < cubes.Count; i++) {
                for(int j = 0; j < ocсupiedCubes.Count; j++) {
                    if(cubes[i].X == ocсupiedCubes[j].Key.X && cubes[i].Y + CUBE_SIZE == ocсupiedCubes[j].Key.Y) {
                        isStay = true;
                        return;
                    }
                }
            }
            for (int i = 0; i < cubes.Count; i++) {
                cubes[i] = new Point(cubes[i].X, cubes[i].Y + CUBE_SIZE);
            }
        }

    }
}
