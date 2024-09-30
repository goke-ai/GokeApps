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
    public class LinearSystemsTests
    {
        [TestMethod()]
        [DataRow(new double[] { -1, 1 }, new double[] { 1, 2 }, new double[] { 1.0, 2.0 }, new double[] { 0.0, 2.0 })]
        [DataRow(new double[] { -14.9, -29.5, 19.8 }, new double[] { -.01, .67, -.44 }, new double[] { 0.3, 0.52, 1 }, new double[] { .5, 1, 1.9 }, new double[] { .1, .3, .5 })]
        public void Guassian_AandB_ReturnX(double[] expected, double[] b, params double[][] AA)
        {

            // LinearSystems linearSystems = new();
            double[,] A = new double[AA.Length, AA[0].Length];
            for (int i = 0; i < AA.Length; i++)
            {
                for (int j = 0; j < AA[i].Length; j++)
                {
                    A[i, j] = AA[i][j];
                }

            }
            var actual = LinearSystems.Guassian(A, b);


            // Assert.IsNotNull(linearSystems);
            Assert.AreEqual(A.GetLength(0), actual.Length);
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                var a = Math.Abs((expected[i] - actual[i]));
                Assert.IsTrue(a < 0.1);
            }
        }

        
    }
}