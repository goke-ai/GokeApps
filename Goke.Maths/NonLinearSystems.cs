using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class NonLinearSystems
    {
        public static double[] Newton(Func<double[], double>[] F, Func<double[], double>[,] J, double errorLimit, int numIteration)
        {
            int size = F.GetLength(0);
            if (size != J.GetLength(0))
            {
                throw new ArgumentException($"Size mismatch with {nameof(F)} and {nameof(J)}.");
            }

            double[] x = [.3, .1, .1, .1, .1, .1, .1, .7];
            double[] x1 = new double[size];

            double[] B = new double[size];
            double[,] A = new double[size, size];

            for (int counter = 0; counter < numIteration; counter++)
            {
                for (int i = 0; i < size; i++)
                {
                    B[i] = F[i](x);
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        A[i, j] = J[i, j](x);
                    }
                }

                // F/J
                double[] y = LinearSystems.Guassian(A, B);

                // xn+1 = xn - Fn/Jn 
                for (int i = 0; i < size; i++)
                {
                    x1[i] = x[i] - y[i];
                }

                // test convergence
                // |xn+1 - xn| 
                var sum = 0.0;
                for (int i = 0; i < size; i++)
                {
                    sum += System.Math.Pow((x1[i] - x[i]), 2);
                }
                var norm = System.Math.Sqrt(sum);

                if (norm <= errorLimit)
                {
                    break;
                }

                // update
                for (int i = 0; i < size; i++)
                {
                    x[i] = x1[i];
                }
            }

            return x;
        }


    }
}
