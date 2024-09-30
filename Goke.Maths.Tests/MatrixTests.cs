using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goke.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Maths.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void Matrix_CreateMatrix_ReturnValidMatrix()
        {
            Matrix m = new();
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        [DataRow(new double[] { 1.0, 2.0 }, new double[] { 0.0, 2.0 })]
        [DataRow(new double[] { 0.3, 0.52, 1 }, new double[] { .5, 1, 1.9 }, new double[] { .1, .3, .5 })]
        public void Triangularize_A_ReturnATriangular(params double[][] AA)
        {
            double[,] A = new double[AA.Length, AA[0].Length];
            for (int i = 0; i < AA.Length; i++)
            {
                for (int j = 0; j < AA[i].Length; j++)
                {
                    A[i, j] = AA[i][j];
                }

            }

            Matrix.Triangularize(A);

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < A.GetLength(1); j++)
                {
                    if (j >= i )
                    {
                        // Assert.AreNotEqual(A[i, j], 0.0);
                        continue;
                    }
                    Assert.AreEqual(A[i, j], 0.0);
                }
            }
        }

        [TestMethod()]
        [DataRow(new double[] { 1.0, 2.0 }, new double[] { 0.0, 2.0 })]
        [DataRow(new double[] { 0.3, 0.52, 1 }, new double[] { .5, 1, 1.9 }, new double[] { .1, .3, .5 })]
        public void Diagonalize_A_ReturnADiagonal(params double[][] AA)
        {
            double[,] A = new double[AA.Length, AA[0].Length];
            for (int i = 0; i < AA.Length; i++)
            {
                for (int j = 0; j < AA[i].Length; j++)
                {
                    A[i, j] = AA[i][j];
                }

            }

            Matrix.Diagonalize(A);

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < A.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        // Assert.AreNotEqual(A[i, j], 0.0);
                        continue;
                    }
                    Assert.AreEqual(A[i, j], 0.0);
                }
            }


        }
    }
}
