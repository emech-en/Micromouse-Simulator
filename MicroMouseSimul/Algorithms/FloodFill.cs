using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul.Algorithms
{
    class FloodFill : IAlgorithm
    {
        Cell[,] cells = new Cell[16, 16];
        int[,] cellVlas = new int[16, 16];
        public FloodFill()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
            //int[,] _iWalls = new int[16, 16];
            for (int i = 0; i < 16; i++)
            {
                cells[i, 0].SetWestWall();
                cells[i, 15].SetEastWall();
                cells[0, i].SetSouthWall();
                cells[15, i].SetNorthWall();
            }
            cells[0, 0].SetEastWall();
            cells[0, 1].SetWestWall();

            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    cellVlas[i, j] = 255;
            cellVlas[7, 7] = 0;
            cellVlas[7, 8] = 0;
            cellVlas[8, 7] = 0;
            cellVlas[8, 8] = 0;

        }
        public override enumRobotAction Think(MicroMouse.Robot robot, MicroMouse.Cell cell)
        {
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    cellVlas[i,j] = 255;
            cellVlas[7,7] = 0;
            cellVlas[7,8] = 0;
            cellVlas[8,7] = 0;
            cellVlas[8,8] = 0;
 
            if (cell.EastWall)
                cells[robot.YLocation, robot.XLocation].SetEastWall();
            if (cell.WestWall)
                cells[robot.YLocation, robot.XLocation].SetWestWall();
            if (cell.SouthWall)
                cells[robot.YLocation, robot.XLocation].SetSouthWall();
            if (cell.NorthWall)
                cells[robot.YLocation, robot.XLocation].SetNorthWall();


            for (int n = 0; n < 256; n++)
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if (!cells[i,j].NorthWall)
                            //if (!cells[i+1,j].SouthWall)
                                if (cellVlas[i+1,j] < cellVlas[i,j])
                                    cellVlas[i,j] = cellVlas[i+1,j] + 1;
                        if (!cells[i,j].EastWall)
                            //if ((cell[Divareha][i + 1][j] / DEAD) % 2 == 0)
                                if (cellVlas[i ,j+1] < cellVlas[i,j])
                                    cellVlas[i, j] = cellVlas[i, j + 1] + 1;
                        if (!cells[i,j].SouthWall)
                            //if ((cell[Divareha][i][j - 1] / DEAD) % 2 == 0)
                                if (cellVlas[i-1,j] < cellVlas[i,j])
                                    cellVlas[i,j] = cellVlas[i-1,j ] + 1;
                        if (!cells[i,j].WestWall)
                            //if ((cell[Divareha][i - 1][j] / DEAD) % 2 == 0)
                                if (cellVlas[i ,j-1] < cellVlas[i,j])
                                    cellVlas[i, j] = cellVlas[i, j - 1] + 1;
                    }
                }
            }

            var choices = cell.CanGo(robot.Direction);
            var choices2 = choices.GetChoices();
            
            List<enumSide> ndeChoices = new List<enumSide>();
            for (int i = 0; i < choices2.Length; i++)
            {
                    ndeChoices.Add(choices2[i]);
            }
            var ndeChoices2 = ndeChoices.ToArray();

            var dest_choice = new IAlgorithm.DestSidePair[ndeChoices2.Length];
            for (int i = 0; i < ndeChoices2.Length; i++)
            {
                var nextLocations = robot.GetNextCellLoc(ndeChoices2[i]);
                var destances = GetPerfectLengthToGoal(nextLocations[0], nextLocations[1]);
                dest_choice[i] = new DestSidePair() { CellData =cellVlas[nextLocations[0], nextLocations[1]], Distance = destances, SideToTurn = ndeChoices2[i] };
            }
            dest_choice = dest_choice.OrderBy(x => (int)x.CellData).ThenBy(x => x.Distance).ToArray();
            if (dest_choice.Select(x => x.SideToTurn).Any())
                switch (dest_choice.Select(x => x.SideToTurn).First())
                {
                    case enumSide.Front:
                        return enumRobotAction.GoStraight;
                    case enumSide.Right:
                        return enumRobotAction.TurnRight;
                    case enumSide.Left:
                        return enumRobotAction.TurnLeft;
                    default:
                        return enumRobotAction.TurnBack;
                }
            else
            {
                return enumRobotAction.TurnBack;
            }
        }

        public override string GetCellData(int i, int j)
        {
            return cellVlas[i, j].ToString();
        }

        //public class FFCell : Cell
        //{
        //    public int Value;
        //}
    }
}
