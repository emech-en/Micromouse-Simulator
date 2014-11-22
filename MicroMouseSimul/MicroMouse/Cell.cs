using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MicroMouseSimul.MicroMouse
{
    [Serializable]
    public class Cell
    {
        private bool _westWall;
        public bool WestWall
        {
            get { return _westWall; }
        }

        private bool _eastWall;
        public bool EastWall
        {
            get { return _eastWall; }
        }

        private bool _northWall;
        public bool NorthWall
        {
            get { return _northWall; }
        }

        private bool _southWall;
        public bool SouthWall
        {
            get { return _southWall; }
        }

        private void construct(int pWalls)
        {
            _northWall = (pWalls % 2 != 0);
            _eastWall = ((pWalls / 2) % 2 != 0);
            _southWall = ((pWalls / 4) % 2 != 0);
            _westWall = ((pWalls / 8) % 2 != 0);
        }
        public Cell(int pWalls)
        {
            construct(pWalls);
        }
        public Cell()
        {
            construct(0);
        }

        /// <summary>
        /// </summary>
        /// <param name="pRobotDirection"></param>
        /// <returns></returns>
        public RobotChoices CanGo(enumDirection pRobotDirection)
        {
            RobotChoices choices = new RobotChoices();
            switch (pRobotDirection)
            {
                case enumDirection.North:
                    choices.Front = !_northWall;
                    choices.Right = !_eastWall;
                    choices.Left = !_westWall;
                    break;
                case enumDirection.South:
                    choices.Front = !_southWall;
                    choices.Right = !_westWall;
                    choices.Left = !_eastWall;
                    break;
                case enumDirection.East:
                    choices.Front = !_eastWall;
                    choices.Right = !_southWall;
                    choices.Left = !_northWall;
                    break;
                case enumDirection.West:
                    choices.Front = !_westWall;
                    choices.Right = !_northWall;
                    choices.Left = !_southWall;
                    break;
                default:
                    throw new Exception("");
            }
            return choices;
        }

        public void SetSouthWall()
        {
            _southWall = true;
        }
        public void SetEastWall()
        {
            _eastWall = true;
        }
        public void SetNorthWall()
        {
            _northWall = true;
        }
        public void SetWestWall()
        {
            _westWall = true;
        }
    }
}
