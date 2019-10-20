using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class Pythagoras
    {
        public static double GetHypotenuse(double a, double b)//Przeciwprostokatna
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
        public static double GetCathetus(double a, double c)//Przyprostokatna
        {
            return Math.Sqrt(Math.Pow(c, 2) - Math.Pow(a, 2));
        }
    }
}
