using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace MicroMouseSimul.MicroMouse
{
    [Serializable]
    public class MouseWorld
    {
        public Maze _maze;
        public Robot _robot;

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
    }
}
