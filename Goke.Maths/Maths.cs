using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class Functions
    {
        public static double DegreeToRadian(double degree)
        {
            return degree / 180.0 * Math.PI;
        }
        public static double RadianToDegree(double radian)
        {
            return radian / Math.PI * 180;
        }

        public static double FahrenheitToCentigrade(double fahrenheit)
        {
            // 100.0 / (180 - 32) * (fahrenheit - 32)
            return 100.0 / 148.0 * (fahrenheit - 32);
        }

        public static double CentigradeToFahrenheit(double centigrade)
        {
            // (180 - 32) / 100 * (centigrade) + 32
            return (1.48 * centigrade) + 32;
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
