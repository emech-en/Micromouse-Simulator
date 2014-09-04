using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroMouseSimul.MicroMouse
{
    [Serializable]
    public class Maze
    {
        /// <summary>
        /// _cell[i,j] is the cell at row i and col j
        /// </summary>
        private Cell[,] _cells = new Cell[16, 16];

        /// <summary>
        /// Get the cell walls array of the maze and create new Maze.
        /// </summary>
        /// <param name="pWalls">
        /// pWalls[i,j] means walls of the Cell at the row i and col j. 
        /// 4bite paeen har adad neshan dahande divare haye har Cell mibashad. 
        /// bit 0 -> North Wall
        /// bit 1 -> East Wall
        /// bit 2 -> South Wall
        /// bit 3 -> West Wall
        /// bit 4 -> NOT IMPORTANT! 
        /// and so on...
        /// </param>
        public Maze(int[,] pWalls)
        {
            if (pWalls == null)
            {
                throw new ArgumentNullException("pWalls");
            }
            if (pWalls.GetLength(0) != 16 || pWalls.GetLength(1) != 16)
            {
                throw new ArgumentOutOfRangeException("pWalls", "Should be int[16,16] array.");
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    _cells[i, j] = new Cell(pWalls[i, j]);
                }
            }
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }

        public void ValidateCells()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (_cells[i, j].NorthWall && i < 15)
                        _cells[i + 1, j].SetSouthWall();
                    if (_cells[i, j].EastWall && j < 15)
                        _cells[i, j + 1].SetWestWall();

                    if (_cells[i, j].SouthWall && i > 0)
                        _cells[i - 1, j].SetNorthWall();
                    if (_cells[i, j].WestWall && j > 0)
                        _cells[i, j - 1].SetEastWall();                    
                }
            }
        }

        public void SetWalls(int xIndex, int yIdex, enumDirection[] directions)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                switch (directions[i])
                {
                    case enumDirection.North:
                        _cells[xIndex, yIdex].SetNorthWall();
                        break;
                    case enumDirection.South:
                        _cells[xIndex, yIdex].SetSouthWall();
                        break;
                    case enumDirection.East:
                        _cells[xIndex, yIdex].SetEastWall();
                        break;
                    case enumDirection.West:
                        _cells[xIndex, yIdex].SetWestWall();
                        break;
                    default:
                        break;
                }
            }
            ValidateCells();
        }
    }
}
