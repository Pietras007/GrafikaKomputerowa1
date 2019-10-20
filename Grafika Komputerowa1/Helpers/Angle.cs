using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class Angle
    {
        public static double GetCosinusFromBetween((double, double) line1, (double, double) line2)
        {
            if(line1.Item1 > line2.Item1)
            {
                return Math.Cos(Math.Atan(line1.Item1) - Math.Atan(line2.Item1));
            }
            else
            {
                return Math.Cos(Math.Atan(line2.Item1) - Math.Atan(line1.Item1));
            }
        }
    }
}
