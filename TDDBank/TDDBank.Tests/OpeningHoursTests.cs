using System;
using Xunit;

namespace TDDBank.Tests
{
   
    public class OpeningHoursTests
    {
        [Theory]
        [InlineData(2022, 02, 21, 10, 30, true)]//mo
        [InlineData(2022, 02, 21, 10, 29, false)]//mo
        [InlineData(2022, 02, 21, 10, 31, true)] //mo
        [InlineData(2022, 02, 21, 18, 59, true)] //mo
        [InlineData(2022, 02, 21, 19, 00, false)] //mo
        [InlineData(2022, 02, 26, 13, 0, true)] //sa
        [InlineData(2022, 02, 26, 16, 0, false)] //sa
        [InlineData(2022, 02, 27, 20, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            Assert.Equal(result, oh.IsOpen(dt));
        }
    }
}
