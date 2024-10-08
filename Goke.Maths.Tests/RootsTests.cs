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
    public class RootsTests
    {
        [TestMethod()]
        [DataRow(1.0, -3.0, 2.0, new double[] { 1, 2, 0, 0 })]
        public void Formula_Test(double a, double b, double c, double[] expected)
        {
            var actual = Roots.Formula(a, b, c);

            Assert.AreEqual(expected[0], actual.r1);
            Assert.AreEqual(expected[1], actual.r2);
            Assert.AreEqual(expected[2], actual.i1);
            Assert.AreEqual(expected[3], actual.i2);
        }

        [TestMethod()]
        public void FixPoint_Test()
        {
            Func<double, double> f = (x) => Math.Pow(Math.E, -x);
            double x0=0;
            double errorLimit = 1.0;
            int maxIteration = 10;

            var actual = Roots.FixPoint(f, x0, errorLimit, maxIteration);

            Assert.AreEqual(0.564879, actual);
        }

        [TestMethod()]
        public void Secant_Test()
        {
            Func<double, double> f = (x) => Math.Pow(Math.E, -x) - x;
            double x_1 = 0, x0 = 1;
            double errorLimit = 1.0;
            int maxIteration = 10;

            var actual = Roots.Secant(f, x_1, x0, errorLimit, maxIteration);

            Assert.AreEqual(0.564879, actual);
        }
    }
}