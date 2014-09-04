
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul.Algorithms
{
    public class WallFollower : IAlgorithm
    {
        private bool _left_wall = false;

        public WallFollower(bool pLeftWall)
            : base()
        {
            this._left_wall = pLeftWall;
        }

        public override enumRobotAction Think(MicroMouse.Robot robot, MicroMouse.Cell cell)
        {
            RobotChoices choices = cell.CanGo(robot.Direction);
            if (_left_wall)
            {
                if (choices.Left)
                    return enumRobotAction.TurnLeft;
                else if (choices.Front)
                    return enumRobotAction.GoStraight;
                else if (choices.Right)
                    return enumRobotAction.TurnRight;
                else
                    return enumRobotAction.TurnBack;
            }
            else
            {
                if (choices.Right)
                    return enumRobotAction.TurnRight;
                else if (choices.Front)
                    return enumRobotAction.GoStraight;
                else if (choices.Left)
                    return enumRobotAction.TurnLeft;
                else
                    return enumRobotAction.TurnBack;
            }
        }

        public override string GetCellData(int i, int j)
        {
            return "";
        }


    }
}
