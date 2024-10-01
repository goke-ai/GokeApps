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
        [DataRow(1.0, -3.0, 2.0, new double[] { 1, 2, 0,0 })]
        public void FormulaTest(double a, double b, double c, double[] expected)
        {
            var actual = Roots.Formula(a,b,c);

            Assert.AreEqual(expected[0], actual.r1);
            Assert.AreEqual(expected[1], actual.r2);
            Assert.AreEqual(expected[2], actual.i1);
            Assert.AreEqual(expected[3], actual.i2);
        }
    }
}