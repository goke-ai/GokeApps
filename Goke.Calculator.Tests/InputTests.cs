using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goke.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Calculator.Tests
{
    [TestClass()]
    public class InputTests
    {
        [TestMethod()]
        public void Input_CreateObject()
        {
            Input input = new();


            //Assert.AreEqual(0, active.PreviousValue);
            //Assert.AreEqual(0, active.CurrentValue);
            //Assert.AreEqual("0", active.Text);
            //Assert.AreEqual("", active.ExpressionText);
            //Assert.AreEqual(Operator.None, active.PreviousOperator);
            //Assert.AreEqual(Operator.None, active.CurrentOperator);

            Assert.IsNotNull(input);
            Assert.AreEqual(0, input.PreviousValue);
            Assert.AreEqual(0, input.CurrentValue);
            Assert.AreEqual("0", input.Text);
            Assert.AreEqual("", input.ExpressionText);
            Assert.AreEqual(Key.None, input.PreviousOperator);
            Assert.AreEqual(Key.None, input.CurrentOperator);

        }

        [TestMethod()]
        public void Number_SingleNumber_Text()
        {
            Input input = new();

            input.SendKey(Key.One);

            Assert.AreEqual(0, input.PreviousValue);
            Assert.AreEqual(1, input.CurrentValue);
            Assert.AreEqual("1", input.Text);
            Assert.AreEqual("", input.ExpressionText);
            Assert.AreEqual(Key.None, input.PreviousOperator);
            Assert.AreEqual(Key.None, input.CurrentOperator);

        }        
    }
}