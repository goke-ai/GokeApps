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
        public static (double r1, double r2, double i1, double i2) Formula(double a, double b, double c)
        {
            double r1 = 0, r2 = 0, i1 = 0, i2 = 0;
            if (a == 0)
            {
                if (b != 0)
                {
                    r1 = -c / b;
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
                else
                {
                    r1 = (-b - Math.Sqrt(D)) / 2 / a;
                    r2 = (-b + Math.Sqrt(D)) / 2 / a;
                }
            }
            return (r1, r2, i1, i2);
        }

        public static (double[] x, double[] y) Quadratic(double a, double b, double c)
        {
            int intervals = 10;
            double h = Math.Abs(c) / intervals;

            var count = (intervals * 2) + 1;
            var v = -Math.Abs(c) - h;
            double[] x = Enumerable.Range(0, count).Select(s => v + (s * h)).ToArray();
            double[] y = Enumerable.Range(0, count).Select(s => 0.0).ToArray();

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = a * (x[i] * x[i]) + b * x[i] + c;
            }
            return (x, y);
        }

        public static double Bisection(Func<double, double> f, double xl, double xu, double percentErrorTolerance = 0.1, int maxIteration = 100)
        {
            double xr = 0;
            double xrold = 0;
            double error = 100;
            double test = 0;
            int i = 0;
            do
            {
                xrold = xr;
                xr = (xl+xu)/2;
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error) * 100;
                }

                test = f(xl) * f(xr);
                if (test < 0)
                {
                    xu = xr;
                }
                else if(test > 0)
                {
                    xl = xr;
                }
                else
                {
                    error = 0;
                }

            } while (i < maxIteration && error > percentErrorTolerance);

            return xr;
        }

        public static double FalsePosition(Func<double, double> f, double xl, double xu, double percentErrorTolerance = 0.1, int maxIteration = 100)
        {
            double xr = 0;
            double xrold = 0;
            double error = 100;

            double fl = f(xl);
            double fu = f(xu);
            double fr = 0;

            double test = 0;

            int iu = 0, il = 0;

            int i = 0;
            do
            {
                xrold = xr;
                xr = xu - fu * (xl - xu) / (fl - fu);
                fr = f(xr);

                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error) * 100;
                }

                test = f(xl) * f(xr);
                if (test < 0)
                {
                    xu = xr;
                    fu = f(xu);
                    iu = 0;
                    il = il + 1;
                    if (il >= 2)
                    {
                        fl /= 2;
                    }
                }
                else if (test > 0)
                {
                    xl = xr;
                    fl= f(xl);

                    il = 0;
                    iu = iu + 1;
                    if (iu >= 2)
                    {
                        fu /= 2;
                    }
                }
                else
                {
                    error = 0;
                }

            } while (i < maxIteration && error > percentErrorTolerance);

            return xr;
        }


        public static double FixPoint(Func<double, double> f, double x0, double percentErrorTolerance = 0.1, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = 0;
            double error = 100;
            int i = 0;
            do
            {
                xrold = xr;
                xr = f(xrold);
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error) * 100;
                }

            } while (i < maxIteration && error > percentErrorTolerance);
            return xr;
        }

        public static double Secant(Func<double, double> f, double x_1, double x0, double percentErrorTolerance = 0.1, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = x_1;
            double error = 100;
            double temp = 0;
            int i = 0;
            do
            {
                temp = xr - ((f(xr) * (xrold - xr)) / (f(xrold) - f(xr)));
                xrold = xr;
                xr = temp;

                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error) * 100;
                }

            } while (i < maxIteration && error > percentErrorTolerance);
            return xr;
        }

        public static double NewtonRaphson(Func<double, double> f, Func<double, double> ff, double x0, double percentErrorTolerance = 0.1, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = 0;
            double error = 100;
            int i = 0;
            do
            {
                xrold = xr;
                xr = xr - f(xrold) / ff(xrold);
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error) * 100;
                }

            } while (i < maxIteration && error > percentErrorTolerance);
            return xr;
        }


    }
}
