using FluentAssertions;
using ppedv.Hotelmanager2022.Model;
using System;
using Xunit;

namespace ppedv.Hotelmanager2022.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_NULL_throws_ArgumentNullEx()
        {
            var core = new Core();

            Assert.Throws<ArgumentNullException>(() => core.CalcBuchungsGesamtPreis(null));
        }

        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_with_no_Room_throws_ArgumentEx()
        {
            var core = new Core();
            var b = new Buchung() { Von = DateTime.Now, Bis = DateTime.Now.AddDays(1), Raum = null };

            //Assert.Throws<ArgumentException>(() => core.CalcBuchungsGesamtPreis(b));

            new Action(() => core.CalcBuchungsGesamtPreis(b)).Should().Throw<ArgumentException>();

        }

        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_Von_Bis_Should_not_be_the_same_throws_ArgumentEx()
        {
            var core = new Core();
            var dt = new DateTime(2020, 1, 1, 1, 0, 0);
            var b = new Buchung() { Von = dt, Bis = dt, Raum = new Raum() };

            //gleiche Zeit
            new Action(() => core.CalcBuchungsGesamtPreis(b)).Should()
                .Throw<ArgumentException>(because:"gleiche Zeit")
                .WithMessage("*Von*Bis*");

            //bis ist vor von
            b.Bis = b.Von.AddSeconds(-1);
            new Action(() => core.CalcBuchungsGesamtPreis(b)).Should()
                .Throw<ArgumentException>(because: "bis ist vor von")
                .WithMessage("*Von*Bis*");

        }


        [Theory]
        [InlineData(1, 12.5, 12.5)]
        [InlineData(2, 12.5, 25)]
        [InlineData(2, -12.5, -25)]
        [InlineData(3, 12.5, 37.5)]
        public void CalcBuchungsGesamtPreis(int tage, decimal preisProTag, decimal expected)
        {
            var r = new Raum() { PreisProTag = preisProTag };
            var b = new Buchung() { Von = DateTime.Now, Bis = DateTime.Now.AddDays(tage), Raum = r };
            //r.Buchungen.Add(b);
            var core = new Core();

            var result = core.CalcBuchungsGesamtPreis(b);

            result.Should().Be(expected);

        }

    }
}