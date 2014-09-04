using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul.Algorithms
{
    class DeadEnd : IAlgorithm
    {
        public int[,] cellWatched = new int[16, 16];

        public DeadEnd()
            : base()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    cellWatched[i, j] = 0;
                }
            }
            cellWatched[0, 0] = -1;
        }

        public override enumRobotAction Think(Robot robot, Cell cell)
        {
            RobotChoices choices = cell.CanGo(robot.Direction);
            var ndeChoices = NotDeadEndChoices(robot, choices);
            var orederdChoices = ReorderChoices(robot, ndeChoices);
            var ndeCount = ndeChoices.Length;
            if (ndeCount == 0)
            {
                cellWatched[robot.YLocation, robot.XLocation] = -1;
                return enumRobotAction.TurnBack;
            }
            else if (ndeCount == 1)
            {
                if (robot.GetPrevCell<int>(cellWatched) == -1)
                    cellWatched[robot.YLocation, robot.XLocation] = -1;
                else
                    cellWatched[robot.YLocation, robot.XLocation]++;

                switch (orederdChoices[0])
                {
                    case enumSide.Front:
                        return enumRobotAction.GoStraight;

                    case enumSide.Right:
                        return enumRobotAction.TurnRight;

                    case enumSide.Left:
                        return enumRobotAction.TurnLeft;

                    default:
                        throw new Exception();
                }
            }
            else if (ndeCount == 2)
            {
                cellWatched[robot.YLocation, robot.XLocation]++;
                switch (orederdChoices[0])
                {
                    case enumSide.Front:
                        return enumRobotAction.GoStraight;
                    case enumSide.Right:
                        return enumRobotAction.TurnRight;
                    case enumSide.Left:
                        return enumRobotAction.TurnLeft;
                    default:
                        throw new Exception();
                }
            }
            else if (ndeCount == 3)
            {
                cellWatched[robot.YLocation, robot.XLocation]++;
                switch (orederdChoices[0])
                {
                    case enumSide.Front:
                        return enumRobotAction.GoStraight;
                    case enumSide.Right:
                        return enumRobotAction.TurnRight;
                    case enumSide.Left:
                        return enumRobotAction.TurnLeft;
                    default:
                        throw new Exception();
                }
            }
            return enumRobotAction.GoStraight;
        }

        public enumSide[] NotDeadEndChoices(Robot robot, RobotChoices rChoices)
        {
            var choices = rChoices.GetChoices();

            List<enumSide> ndeChoices = new List<enumSide>();
            for (int i = 0; i < choices.Length; i++)
            {
                if (robot.GetNextCell<int>(cellWatched, choices[i]) != -1)
                {
                    ndeChoices.Add(choices[i]);
                }
            }

            return ndeChoices.ToArray();
        }

        public enumSide[] ReorderChoices(Robot robot, enumSide[] Choices)
        {
            var dest_choice = new IAlgorithm.DestSidePair[Choices.Length];
            for (int i = 0; i < Choices.Length; i++)
            {
                var nextLocations = robot.GetNextCellLoc(Choices[i]);
                var destances = GetPerfectLengthToGoal(nextLocations[0], nextLocations[1]);
                dest_choice[i] = new DestSidePair() { CellData = cellWatched[nextLocations[0], nextLocations[1]], Distance = destances, SideToTurn = Choices[i] };
            }
            dest_choice = dest_choice.OrderBy(x => (int)x.CellData).ThenBy(x => x.Distance).ToArray();
            return dest_choice.Select(x => x.SideToTurn).ToArray();
        }

        public override string GetCellData(int i, int j)
        {
            return cellWatched[i, j].ToString();
        }
    }
}
