using FluentAssertions;
using Xunit;

namespace Calculator.Tests_xUnit
{
    public class CalcTests_xUnit
    {
        [Theory]
        [InlineData(1, 2, -1)]
        [InlineData(-5, -2, -3)]
        public void Calc_Substract(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Substract(a, b);

            Assert.Equal(exp, result);
        }

        [Fact]
        public void Calc_Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Calc_Sum_4_and_3_results_7()
        {

            var calc = new Calc();

            calc.Sum(4, 3).Should().Be(7, "Ist nicht, weil isso");
            calc.Sum(4, 3).Should().BeGreaterThan(3);
            calc.Sum(4, 3).Should().BeInRange(3, 5000);


        }
    }
}