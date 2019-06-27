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
    public enum FIGURE_ROTATE_TYPE { NORMAL, ROTATE_90, ROTATE_180, ROTATE_270 };
    

    public class Figure {
        public Panel workspace;
        private FIGURE_TYPE figureType;
        public List<Point> cubes;
        public bool isStay = false;
        public int CUBE_SIZE = 20;
        private FIGURE_ROTATE_TYPE rotateType = FIGURE_ROTATE_TYPE.NORMAL;

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
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(3 * CUBE_SIZE, -2 * CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.L: {  
                    cubes.Add(new Point(3 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -CUBE_SIZE));
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));

                    break;
                }
                case FIGURE_TYPE.O: {
                    cubes.Add(new Point(5 * CUBE_SIZE, -2 * CUBE_SIZE));
                    cubes.Add(new Point(4 * CUBE_SIZE, -2 * CUBE_SIZE));
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

        public int GetLeftmostCoordinate() {
            int res = cubes[0].X;
            for (int i = 1; i < cubes.Count; i++) {
                if (cubes[i].X < res) {
                    res = cubes[i].X;
                }
            }
            return res;
        }

        public int GetRightmostCoordinate() {
            int res = cubes[0].X;
            for (int i = 1; i < cubes.Count; i++) {
                if (cubes[i].X > res) {
                    res = cubes[i].X;
                }
            }
            return res + CUBE_SIZE;
        }

        public int GetBottommostCoordinate() {
            int res = cubes[0].Y;
            for (int i = 1; i < cubes.Count; i++) {
                if (cubes[i].Y > res) {
                    res = cubes[i].Y;
                }
            }
            return res + CUBE_SIZE;
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


        public bool Rotate(List<KeyValuePair<Point, Brush>> ocсupiedCubes) {
            switch (figureType) {
                case FIGURE_TYPE.I: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X - 2 * CUBE_SIZE, cubes[0].Y + 3 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 1 * CUBE_SIZE, cubes[3].Y));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if( newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];
                            cubes[3] = newPoints[3];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X + 2 * CUBE_SIZE, cubes[0].Y - 3 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 1 * CUBE_SIZE, cubes[3].Y));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];
                            cubes[3] = newPoints[3];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                case FIGURE_TYPE.J: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 2 * CUBE_SIZE, cubes[2].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 3 * CUBE_SIZE, cubes[1].Y - 2 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 2 * CUBE_SIZE, cubes[2].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 1 * CUBE_SIZE, cubes[3].Y + 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_180;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_180: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 2 * CUBE_SIZE, cubes[2].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 3 * CUBE_SIZE, cubes[3].Y + 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_270;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_270: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 2 * CUBE_SIZE, cubes[2].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 1 * CUBE_SIZE, cubes[3].Y - 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                case FIGURE_TYPE.L: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X + 1 * CUBE_SIZE, cubes[0].Y - 3 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X, cubes[1].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 1 * CUBE_SIZE, cubes[2].Y - 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X + 3 * CUBE_SIZE, cubes[0].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X + 2 * CUBE_SIZE, cubes[1].Y));
                            newPoints.Add(new Point(cubes[2].X + 1 * CUBE_SIZE, cubes[2].Y - 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_180;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_180: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X - 1 * CUBE_SIZE, cubes[0].Y + 3 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X, cubes[1].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 1 * CUBE_SIZE, cubes[2].Y + 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_270;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_270: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[0].X - 3 * CUBE_SIZE, cubes[0].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[1].X - 2 * CUBE_SIZE, cubes[1].Y));
                            newPoints.Add(new Point(cubes[2].X - 1 * CUBE_SIZE, cubes[2].Y + 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[0] = newPoints[0];
                            cubes[1] = newPoints[1];
                            cubes[2] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                case FIGURE_TYPE.O:
                    break;
                case FIGURE_TYPE.S: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 1 * CUBE_SIZE, cubes[3].Y - 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 2 * CUBE_SIZE, cubes[2].Y));
                            newPoints.Add(new Point(cubes[3].X + 3 * CUBE_SIZE, cubes[3].Y + 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_180;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_180: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 1 * CUBE_SIZE, cubes[3].Y + 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_270;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_270: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 2 * CUBE_SIZE, cubes[2].Y));
                            newPoints.Add(new Point(cubes[3].X - 3 * CUBE_SIZE, cubes[3].Y - 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                case FIGURE_TYPE.T: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X, cubes[1].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 1 * CUBE_SIZE, cubes[2].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 2 * CUBE_SIZE, cubes[3].Y));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 2 * CUBE_SIZE, cubes[1].Y));
                            newPoints.Add(new Point(cubes[2].X + 1 * CUBE_SIZE, cubes[2].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X, cubes[3].Y - 2 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_180;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_180: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X, cubes[1].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 1 * CUBE_SIZE, cubes[2].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 2 * CUBE_SIZE, cubes[3].Y));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_270;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_270: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 2 * CUBE_SIZE, cubes[1].Y));
                            newPoints.Add(new Point(cubes[2].X - 1 * CUBE_SIZE, cubes[2].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X, cubes[3].Y + 2 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                case FIGURE_TYPE.Z: {
                    switch (rotateType) {
                        case FIGURE_ROTATE_TYPE.NORMAL: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X - 2 * CUBE_SIZE, cubes[2].Y));
                            newPoints.Add(new Point(cubes[3].X - 3 * CUBE_SIZE, cubes[3].Y + 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_90;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_90: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X - 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y - 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X - 1 * CUBE_SIZE, cubes[3].Y - 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_180;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_180: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y - 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X + 2 * CUBE_SIZE, cubes[2].Y));
                            newPoints.Add(new Point(cubes[3].X + 3 * CUBE_SIZE, cubes[3].Y - 1 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.ROTATE_270;

                            break;
                        }
                        case FIGURE_ROTATE_TYPE.ROTATE_270: {
                            List<Point> newPoints = new List<Point>();
                            newPoints.Add(new Point(cubes[1].X + 1 * CUBE_SIZE, cubes[1].Y + 1 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[2].X, cubes[2].Y + 2 * CUBE_SIZE));
                            newPoints.Add(new Point(cubes[3].X + 1 * CUBE_SIZE, cubes[3].Y + 3 * CUBE_SIZE));

                            for (int i = 0; i < newPoints.Count; i++) {
                                if (newPoints[i].X + CUBE_SIZE > workspace.Width || newPoints[i].X < 0 ||
                                        newPoints[i].Y + CUBE_SIZE > workspace.Height) {
                                    return false;
                                }

                                for (int j = 0; j < ocсupiedCubes.Count; j++) {
                                    if ((newPoints[i].X == ocсupiedCubes[j].Key.X && newPoints[i].Y == ocсupiedCubes[j].Key.Y)) {
                                        return false;
                                    }
                                }
                            }

                            cubes[1] = newPoints[0];
                            cubes[2] = newPoints[1];
                            cubes[3] = newPoints[2];

                            rotateType = FIGURE_ROTATE_TYPE.NORMAL;

                            break;
                        }
                        default:
                            throw new Exception("Wrong rotate type of Figure");
                    }
                    break;
                }
                default:
                    throw new Exception("Wrong type of Figure");
            }
            return true;
        }
    }
}
