using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths
{
    public class LinearSystems
    {
        public static double[] Guassian(double[,] A, double[] b)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            if (rows != b.Length)
            {
                return [];
            }

            //solve Guassian
            // arguemented matrix
            double[,] M = Matrix.AppendHorizontally(A, b);

            // triangle matrix
            Matrix.Triangularize(M);

            // guassian elimination
            double[] X = new double[cols];
            for (int i = rows - 1; i >= 0; i--)
            {
                var sum = 0.0;
                for (int j = i + 1; j < cols; j++)
                {
                    sum += M[i, j] * X[j];
                }
                X[i] = (M[i, rows] - sum) / M[i, i];
            }

            return X;
        }

        

        
    }
}
