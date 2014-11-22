using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MicroMouseSimul.Algorithms;


namespace MicroMouseSimul.MicroMouse
{
    [Serializable]
    public class MouseWorld
    {
        public Maze _maze;
        public Robot _robot;
        public IAlgorithm _algorithm;
        private bool[,] cellVisited = new bool[16, 16];

        public MouseWorld()
        {
            int[,] _iWalls = new int[16, 16];
            for (int i = 0; i < 16; i++)
            {
                _iWalls[i, 0] += 8;
                _iWalls[i, 15] += 2;
                _iWalls[0, i] += 4;
                _iWalls[15, i] += 1;
            }
            _iWalls[0, 0] = 14;
            _iWalls[0, 1] = 12;

            _iWalls[7, 7] = 12;
            _iWalls[7, 6] = 2;
            _iWalls[6, 7] = 1;

            _iWalls[7, 8] = 6;
            _iWalls[7, 9] = 8;
            _iWalls[6, 8] = 1;

            _iWalls[8, 7] = 9;
            _iWalls[8, 6] = 2;
            _iWalls[9, 7] = 4;

            _iWalls[8, 8] = 1;
            _iWalls[9, 8] = 4;

            _maze = new Maze(_iWalls);
            _robot = new Robot(0, 0, enumDirection.North);
        }

        public MouseWorld(int[,] pWalls)
        {
            this._maze = new Maze(pWalls);
            this._robot = new Robot();
        }

        public MouseWorld(string pMazePathFile)
        {
            try
            {
                var loadedMaze = ObjectSaver.Load(pMazePathFile) as MouseWorld;
                if (loadedMaze != null)
                {
                    this._maze = loadedMaze._maze;
                    this._robot = loadedMaze._robot;
                }
                else
                {
                    throw new FileLoadException("File not found or is not valid.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cell[,] GetMazeCells()
        {
            return _maze.GetCells();
        }

        public void SetMaze(int[,] pCellWalls)
        {
            this._maze = new Maze(pCellWalls);
        }

        public void ValidateCells()
        {
            this._maze.ValidateCells();
        }

        public bool NotFinished()
        {
            return (_robot.XLocation < 7 || _robot.XLocation > 8) || (_robot.YLocation < 7 || _robot.YLocation > 8);
        }

        internal Cell getCurrentCell()
        {
            return _maze.GetCells()[_robot.YLocation, _robot.XLocation];
        }

        bool isNotStarted = false;
        public int count = 0;
        public int turnCount = 0;
        public int cellVisitedCount = 0;
        private Dictionary<int, int> uniqueCells = new Dictionary<int, int>();
        public bool Go_go_go()
        {
            if (_algorithm == null)
            {
                throw new Exception("Algorithm is not instantiated.");
            }
            var action = _algorithm.Think(_robot, getCurrentCell());
            switch (action)
            {
                case enumRobotAction.TurnLeft:
                    this._robot.TrunLeft();
                    turnCount++;
                    break;
                case enumRobotAction.TurnRight:
                    this._robot.TrunRight();
                    turnCount++;
                    break;
                case enumRobotAction.TurnBack:
                    this._robot.TrunBack();
                    turnCount++;
                    break;
                default:
                    break;
            }
            _robot.Go();
            cellVisited[_robot.YLocation, _robot.XLocation] = true;
            count++;
            cellVisitedCount++;
            if (!uniqueCells.ContainsKey(_robot.XLocation * 100 + _robot.YLocation))
            {
                uniqueCells.Add(_robot.XLocation * 100 + _robot.YLocation, 0);
            }
            uniqueCells[_robot.XLocation * 100 + _robot.YLocation]++;
            cellVisitedCount = uniqueCells.Keys.Count;
            return this.NotFinished();
        }
        public string GetCellData(int x, int y)
        {
            if (_algorithm == null)
                return "";
            else
                return this._algorithm.GetCellData(x, y);
        }

        public void Start(IAlgorithm alg)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    cellVisited[i, j] = false;
                }
            }

            cellVisited[0, 0] = true;
            this._robot = new Robot();
            this._algorithm = alg;
            this.cellVisitedCount = 0;
            this.count = 0;
            this.turnCount = 0;
            this.uniqueCells = new Dictionary<int, int>();
        }
        public bool[,] GetVisitedCells()
        {
            return cellVisited;
        }
    }
}
