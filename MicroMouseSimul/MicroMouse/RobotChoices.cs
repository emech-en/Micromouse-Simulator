using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroMouseSimul.MicroMouse
{
    public class RobotChoices
    {
        public bool Front;
        public bool Right;
        public bool Left;
        
        public int GetChoicesCount()
        {
            var ways = 0;
            if (Front) ways++;
            if (Right) ways++;
            if (Left) ways++;
            return ways;
        }

        public enumSide[] GetChoices()
        {
            List<enumSide> choices = new List<enumSide>();
            if (Front)
                choices.Add(enumSide.Front);
            if (Right)
                choices.Add(enumSide.Right);
            if (Left)
                choices.Add(enumSide.Left);
            return choices.ToArray();
        }

        internal enumSide GetFirstChoice()
        {
            if (Front)
                return enumSide.Front;
            else if (Right)
                return enumSide.Right;
            else if (Left)
                return enumSide.Left;
            else
                throw new Exception();
        }
    }
}
