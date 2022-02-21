using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("ExceptionTests")]
        public void Calc_Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(3, 5, 8)]
        [DataRow(276, 4, 280)]
        [DataRow(-10, 4, -6)]
        [DataRow(-10, -4, -14)]
        public void Calc_Sum(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.AreEqual(exp, result);
        }

        [TestMethod]
        [TestCategory("ExceptionTests")]
        [DataRow(int.MaxValue, 1)]
        [DataRow(1, int.MaxValue)]
        [DataRow(int.MaxValue - 1, 2)]
        [DataRow(int.MinValue, -1)]
        [DataRow(-1, int.MinValue)]
        public void Calc_Sum_throws_OverflowException(int a, int b)
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        //[TestMethod]
        //public void MyTestMethod()
        //{
        //    throw new NotImplementedException();
        //}

    }
}