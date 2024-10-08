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
            double errorLimit = 0.0001;
            int maxIteration = 10;

            var actual = Roots.FixPoint(f, x0, errorLimit, maxIteration);

            Assert.IsTrue(Math.Abs(0.564879-actual) < errorLimit);
        }

        [TestMethod()]
        public void Secant_Test()
        {
            Func<double, double> f = (x) => Math.Pow(Math.E, -x) - x;
            double x_1 = 0, x0 = 1;
            double errorLimit = 1.0;
            int maxIteration = 10;

            var actual = Roots.Secant(f, x_1, x0, errorLimit, maxIteration);

            Assert.IsTrue(Math.Abs(0.564879 - actual) < errorLimit);
        }

        [TestMethod()]
        public void PolyDivideByRoot_Test()
        {
            double[] a = [-24, 2, 1];
            Roots.PolyDivideByRoot(a, 4);

            Assert.AreEqual(6, a[0]);
            Assert.AreEqual(1, a[1]);
        }

        [TestMethod()]
        public void PolyDivide_Test()
        {
            double[] a = [-24, 2, 1];
            double[] d = [-4, 1];
            (var q, var r) = Roots.PolyDivide(a, d);

            Assert.AreEqual(0, q[0]);
            Assert.AreEqual(6, q[1]);
            Assert.AreEqual(1, q[2]);
        }
    }
}