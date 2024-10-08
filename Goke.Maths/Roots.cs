using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                if (D > 0)
                {
                    r1 = (-b - Math.Sqrt(D)) / 2 / a;
                    r2 = (-b + Math.Sqrt(D)) / 2 / a;
                }
                else
                {
                    r1 = -b / 2 / a;
                    r2 = r1;
                    i1 = Math.Sqrt(Math.Abs(D)) / 2 / a;
                    i2 = -i1;
                    // throw new ArithmeticException("Complex root expected");
                }
            }
            return (r1, r2, i1, i2);
        }

        public static (double r1, double r2, double i1, double i2) Quadratic(double a, double b, double c)
        {
            return Formula(a, b, c);
        }

        public static (double[] x, double[] y) QuadraticData(double a, double b, double c)
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

        public static double Bisection(Func<double, double> f, double xl, double xu, double tolerance = 1e-6, int maxIteration = 100)
        {
            double xr = 0;
            double xrold = 0;
            double error = 1;
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
                    error = Math.Abs(error);
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

            } while (i < maxIteration && error > tolerance);

            return xr;
        }

        public static double FalsePosition(Func<double, double> f, double xl, double xu, double tolerance = 1e-6, int maxIteration = 100)
        {
            double xr = 0;
            double xrold = 0;
            double error = 1;

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
                    error = Math.Abs(error);
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

            } while (i < maxIteration && error > tolerance);

            return xr;
        }

        public static double FixPoint(Func<double, double> f, double x0, double tolerance = 1e-6, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = 0;
            double error = 1;
            int i = 0;
            do
            {
                xrold = xr;
                xr = f(xrold);
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error);
                }

            } while (i < maxIteration && error > tolerance);
            return xr;
        }

        public static double Secant(Func<double, double> f, double x_1, double x0, double tolerance = 1e-6, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = x_1;
            double error = 1;
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
                    error = Math.Abs(error);
                }

            } while (i < maxIteration && error > tolerance);
            return xr;
        }
        
        public static double Secant(Func<double, double> f, double x0, double tolerance = 1e-6, int maxIteration = 100)
        {
            double d = 0.01;

            double xr = x0;
            double xrold = 0;
            double error = 1;
            int i = 0;
            do
            {
                xrold = xr;
                xr = xr - ((d * xrold * f(xrold)) / (f(xrold + d * xrold) - f(xrold)));
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error);
                }

            } while (i < maxIteration && error > tolerance);
            return xr;
        }

        public static double NewtonRaphson(Func<double, double> f, Func<double, double> ff, double x0, double tolerance = 1e-6, int maxIteration = 100)
        {
            double xr = x0;
            double xrold = 0;
            double error = 1;
            int i = 0;
            do
            {
                xrold = xr;
                xr = xr - f(xrold) / ff(xrold);
                i++;

                if (xr != 0)
                {
                    error = (xr - xrold) / xr;
                    error = Math.Abs(error);
                }

            } while (i < maxIteration && error > tolerance);
            return xr;
        }

        public static double FZeros(Func<double, double> f, double xl, double xu)
        {          
            double tolerance = 1e-6;
            double eps = 2.22044604925031e-16;

            double a = xl;
            double b = xu;
            double fa = f(a);
            double fb = f(b);
            double c = a;
            double fc = fa;
            double d = b - c;
            double e = d;

            while (true)
            {
                if (fb == 0)
                    break;

                if (Functions.Sign(fa) == Functions.Sign(fb))
                {
                    a = c;
                    fa = fc;
                    d = b - c;
                    e = d;
                }
                if (Math.Abs(fa) < Math.Abs(fb))
                {
                    c = b;
                    b = a;
                    a = c;
                    fc = fb;
                    fb = fa;
                    fa = fc;
                }
                double m = 0.5 * (a - b);
                tolerance = 2 * eps * (Math.Abs(b) > 1 ? Math.Abs(b) : 1.0);
                if (Math.Abs(m) < tolerance || fb == 0)
                {
                    break;
                }
                // open method or bisection
                if (Math.Abs(e) >= tolerance && Math.Abs(fc) > Math.Abs(fb))
                {
                    double s = fb / fc;
                    double p;
                    double q;
                    if (a == c) // secant
                    {
                        p = 2 * m * s;
                        q = 1 - s;
                    }
                    else
                    {
                        q = fc / fa;
                        double r = fb / fa;
                        p = s * (2 * m * q * (q - r) - (b - c) * (r - 1));
                        q = (q - 1) * (r - 1) * (s - 1);
                    }
                    if (p > 0)
                    {
                        q = -q;
                    }
                    else
                    {
                        p = -p;
                    }
                    if (2 * p < 3 * m * q - Math.Abs(tolerance * q) && p < Math.Abs(0.5 * e * q))
                    {
                        e = d;
                        d = p / q;
                    }
                    else
                    {
                        d = m;
                        e = m;
                    }
                }
                else
                {
                    d = m;
                    e = m;
                }
                c = b;
                fc = fb;

                if (Math.Abs(d) > tolerance)
                {
                    b = b + d;
                }
                else
                {
                    b = b - Functions.Sign(b - a) * tolerance;
                }
                fb = f(b);
            }
            return b;
        }

        public static void PolyDivideByRoot(double[] a, double root)
        {
            int n = a.Length;
            double r = a[n - 1];
            a[n - 1] = 0;

            for (int i = n - 2; i >= 0; i--)
            {
                double s = a[i];
                a[i] = r;
                r = s + r * root;
            }
        }

        public static (double[] q, double[] r) PolyDivide(double[] a, double[] d)
        {
            int n = a.Length;
            int m = d.Length;

            double[] r = new double[n];
            double[] q = new double[n];

            for (int i = 0; i < n; i++)
            {
                r[i] = a[i];
                q[i] = 0.0;
            }

            for (int k = (n-m); k >= 0; k--)
            {
                q[k+1] = r[(m-1) + k] / d[m-1];
                for (int j = m+k-1; j >= k; j--)
                {
                    r[j] = r[j] - q[k + 1] * d[j - k];
                }
            }
            for (int j = m; j < n; j++)
            {
                r[j] = 0;
            }
            n = n - m;

            return (q, r);
        }

        public static (double re, double im)[] Bairstow(double[] a, double tolerance=1, int maxIteration=100)
        {
            int n = a.Length;
            double[] re = new double[n];
            double[] im = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];

            double r = -1;
            double s = -1;

            int iter = 0;
            double ea1 = 1,  ea2 = 1;
            while(true)
            {
                if (n < 3 || iter >= maxIteration)
                {
                    break;
                }
                iter = 0;

                while (true)
                {
                    iter++;

                    b[n - 1] = a[n - 1];
                    b[n - 2] = a[n - 2] + r * b[n - 1];
                    c[n - 1] = b[n - 1];
                    c[n - 2] = b[n - 2] + r * c[n - 1];

                    for (int i = (n-2) - (1); i >= 0; i--)
                    {
                        b[i] = a[i] + r * b[i + 1] + s * b[i + 2];
                        c[i] = b[i] + r * c[i + 1] + s * c[i + 2];
                    }

                    var det = c[2] * c[2] - c[3] * c[1];

                    if(det != 0)
                    {
                        var dr = (-b[1] * c[2] + b[0] * c[3]) / det;
                        var ds = (-b[0] * c[2] + b[1] * c[1]) / det;

                        r += dr;
                        s += ds;
                        if (r != 0)
                        {
                            ea1 = Math.Abs(dr / r) * 100;
                        }
                        if (s != 0)
                        {
                            ea2 = Math.Abs(ds / s) * 100;
                        }
                    }
                    else
                    {
                        r++;
                        s++;
                        iter = 0;
                    }

                    if (ea1 <= tolerance && ea2 <= tolerance || iter >= maxIteration)
                    {
                        break;
                    }
                }

                (double r1, double r2, double i1, double i2) = Quadratic(1, -r, -s);
                re[n - 1] = r1;
                im[n - 1] = i1;
                re[n - 2] = r2;
                im[n - 2] = i2;

                n = n - 2;

                for (int i = 0; i < n; i++)
                {
                    a[i] = b[i + 2];
                }
            }

            if (iter < maxIteration)
            {
                if ((n-1) == 2)
                {
                    r = -a[1] / a[2];
                    s = -a[0] / a[2];

                    (double r1, double r2, double i1, double i2) = Quadratic(1, -r, -s);
                    re[n - 1] = r1;
                    im[n - 1] = i1;
                    re[n - 2] = r2;
                    im[n - 2] = i2;
                }
                else
                {
                    re[n - 1] = -a[0] / a[1];
                    im[n - 1] = 0;
                }
            }
            else
            {
                iter = 1;
            }


        //Exit:
            return re.Zip(im).ToArray();
        }

        public static (double re, double im)[] Polynomial(double[] a, double tolerance = 1, int maxIteration = 100)
        {
            (double re, double im)[] y = Roots.Bairstow(a);
            return y[1..];
        }


    }
}
