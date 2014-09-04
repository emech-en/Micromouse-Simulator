using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul.Algorithms
{
    abstract public class IAlgorithm
    {
        public abstract enumRobotAction Think(Robot robot, Cell cell);
        public abstract string GetCellData(int i, int j);

        public double GetPerfectLengthToGoal(int i, int j)
        {
            int iCenter = 7,
                jCenter = 7;
            if (i > 7)
                iCenter = 8;
            if (j > 7)
                jCenter = 8;

            return Math.Sqrt(Math.Pow(i - iCenter, 2) + Math.Pow(j - jCenter, 2));
        }

        public class DestSidePair
        {
            public double Distance;
            public enumSide SideToTurn;
            public object CellData;
        }
    }
}