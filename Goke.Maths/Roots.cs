using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class Roots
    {
        /// <summary>
        /// Method <c>Formula</c> Retuns quadratic equation roots (real and imaginary)
        /// </summary>
        /// <param name="a">coefficient of x^2</param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>
        /// <para>r1- real root 1</para>    
        /// <para>r2: real root 2</para> 
        /// <para>i1- imagnary root 1</para> 
        /// <para>i2: imagnary root 2</para> 
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public static (double r1, double r2,double i1,double i2) Formula(double a, double b, double c)
        {
            double r1 = 0, r2 = 0, i1 = 0, i2= 0;
            if (a == 0)
            {
                if (b != 0)
                { 
                    r1 = -c/b;
                }
                else
                {
                    throw new ArgumentException("Trivial solution.");
                }
            }
            else
            {
                var D = Math.Pow(b, 2) - 4 * a * c;
                if (D <= 0)
                {
                    r1 = -b / 2 / a;
                    r2 = r1;
                    i1 = Math.Sqrt(Math.Abs(D)) / 2 / a;
                    i2 = -i1;
                    // throw new ArithmeticException("Complex root expected");
                }

                r1 = (-b - Math.Sqrt(D)) / 2 / a;
                r2 = (-b + Math.Sqrt(D)) / 2 / a;
            }
            return (r1, r2, i1, i2);
        }
    }
}
