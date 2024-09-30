using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class Matrix
    {
        public static double[,] AppendHorizontally(double[,] A, double[] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            if (rows != B.Length)
                throw new ArgumentException($"Size mismatch between {nameof(A)} and {nameof(B)}");

            double[,] M = new double[rows, cols + 1];

            //combine A|B
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    M[i, j] = A[i, j];
                }
                M[i, cols] = B[i];
            }
            return M;
        }

        public static void Triangularize(double[,] M)
        {
            int rows = M.GetLength(0);
            int cols = M.GetLength(1);

            for (int p = 0; p < rows - 1; p++)
            {
                var app = M[p, p];
                // check pivot for zero
                if (app == 0)
                {
                    // search for replacement row below
                    for (var i = p + 1; i < rows; i++)
                    {
                        if (M[i, p] != 0)
                        {
                            // swap and break out
                            var tmp = 0.0;
                            for (int j = 0; j < cols; j++)
                            {
                                tmp = M[p, j];
                                M[p, j] = M[i, j];
                                M[i, j] = tmp;
                            }
                            break;
                        }
                    }
                }
                app = M[p, p];
                if (app == 0)
                {
                    break;
                }

                // reduce rows
                for (int i = p + 1; i < rows; i++)
                {
                    for (int j = p + 1; j < cols; j++)
                    {
                        M[i, j] -= M[i, p] * M[p, j] / M[p, p];
                    }
                    M[i, p] = 0;
                }
            }
        }

        public static void Diagonalize(double[,] M)
        {
            int rows = M.GetLength(0);
            int cols = M.GetLength(1);

            Triangularize(M);

            for (int p = rows - 1; p > 0; p--)
            {
                // reduce rows
                for (int i = p - 1; i >= 0; i--)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        M[i, j] -= M[i, p] * M[p, j] / M[p, p];
                    }
                    M[i, p] = 0;
                }
            }

            //

        }
    }
}
