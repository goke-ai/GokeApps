using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class Maths
    {
        public static double DegreeToRadian(double degree)
        {
            return degree / 180.0 * Math.PI;
        }

        public static double Factorial(int x)
        {
            double f = 1;
            for (int i = 2; i <= x; i++)
            {
                f *= i;
            }

            return f;
        }
    }
}
