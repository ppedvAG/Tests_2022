using NUnit.Framework;

namespace Calculator.Tests_NUnit
{
    [TestFixture]
    public class CalcTests_NUnit
    {

        [Test]
        [TestCase(1, 2, -1)]
        [TestCase(-5, -2, -3)]
        public void Calc_Substract(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Substract(a, b);

            Assert.AreEqual(exp, result);
        }
    }
}