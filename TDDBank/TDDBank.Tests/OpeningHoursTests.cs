using Microsoft.QualityTools.Testing.Fakes;
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

        [Fact]
        public void OpeningHours_IsWeekend()
        {
            var oh = new OpeningHours();

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 21);
                Assert.False(oh.IsWeekend());//mo
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 22);
                Assert.False(oh.IsWeekend());//di
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 23);
                Assert.False(oh.IsWeekend());//mi
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 24);
                Assert.False(oh.IsWeekend());//di
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 25);
                Assert.False(oh.IsWeekend());//fr
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 26);
                Assert.True(oh.IsWeekend());//sa
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 2, 27);
                Assert.True(oh.IsWeekend());//so
            }
        }
    }
}
