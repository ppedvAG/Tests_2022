
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Calculator.Tests_NUnit")]
[assembly: InternalsVisibleTo("Calculator.Tests_xUnit")]

namespace Calculator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {
            return checked(a + b);
        }

        internal int Substract(int a, int b)
        {


            return checked(a - b);
        }
    }
}