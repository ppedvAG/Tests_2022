using FluentAssertions;
using Moq;
using ppedv.Hotelmanager2022.Model;
using ppedv.Hotelmanager2022.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ppedv.Hotelmanager2022.Logic.Tests
{
    public class CoreTests
    {


        [Fact]
        public void GetTeuersteBuchung_ensure_CalcBuchungsGesamtPreis_is_used()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Buchung>())
                .Returns(() =>
                {
                    var dt = new DateTime(2020, 1, 1, 1, 0, 0);

                    var b1 = new Buchung()
                    {
                        Id = 1,
                        Von = dt,
                        Bis = dt.AddDays(3),
                        Raum = new Raum() { PreisProTag = 2m }
                    };

                    var b2 = new Buchung()
                    {
                        Id = 2,
                        Von = dt,
                        Bis = dt.AddDays(4),
                        Raum = new Raum() { PreisProTag = 2m }
                    };
                    return new[] { b1, b2 }.AsQueryable();
                });

            var coreMock = new Mock<Core>(mock.Object);


            Buchung? result = coreMock.Object.GetTeuersteBuchung();

            coreMock.Verify(x => x.CalcBuchungsGesamtPreis(It.IsAny<Buchung>()), Times.Exactly(2));
        }

        [Fact]
        public void GetTeuersteBuchung_no_buchung_returns_null()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Buchung>())
                .Returns(() => new List<Buchung>().AsQueryable());

            var core = new Core(mock.Object);

            Buchung? result = core.GetTeuersteBuchung();

            result.Should().BeNull();
        }


        [Fact]
        public void GetTeuersteBuchung_both_same_value_newer_wins()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Buchung>())
                .Returns(() =>
                {
                    var dt1 = new DateTime(2020, 1, 1, 1, 0, 0);
                    var dt2 = new DateTime(2021, 1, 1, 1, 0, 0);
                    var b1 = new Buchung()
                    {
                        Id = 1,
                        Von = dt1,
                        Bis = dt1.AddDays(3),
                        Raum = new Raum() { PreisProTag = 2m }
                    };

                    var b2 = new Buchung()
                    {
                        Id = 2,
                        Von = dt2,
                        Bis = dt2.AddDays(3),
                        Raum = new Raum() { PreisProTag = 2m }
                    };
                    return new[] { b1, b2 }.AsQueryable();
                });
            var core = new Core(mock.Object);

            Buchung? result = core.GetTeuersteBuchung();

            result.Id.Should().Be(2);
        }

        [Fact]
        public void GetTeuersteBuchung_second_buchung_is_more_expensive_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Buchung>())
                .Returns(() =>
                {
                    var dt = new DateTime(2020, 1, 1, 1, 0, 0);
                    var b1 = new Buchung()
                    {
                        Id = 1,
                        Von = dt,
                        Bis = dt.AddDays(3),
                        Raum = new Raum() { PreisProTag = 2m }
                    };

                    var b2 = new Buchung()
                    {
                        Id = 2,
                        Von = dt,
                        Bis = dt.AddDays(4),
                        Raum = new Raum() { PreisProTag = 2m }
                    };
                    return new[] { b1, b2 }.AsQueryable();
                });
            var core = new Core(mock.Object);

            Buchung? result = core.GetTeuersteBuchung();

            result.Id.Should().Be(2);
        }

        [Fact]
        public void GetTeuersteBuchung_second_buchung_is_more_expensive()
        {
            var core = new Core(new TestRepo());

            Buchung? result = core.GetTeuersteBuchung();

            result.Id.Should().Be(2);
        }


        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_NULL_throws_ArgumentNullEx()
        {
            var core = new Core(null);

            Assert.Throws<ArgumentNullException>(() => core.CalcBuchungsGesamtPreis(null));
        }

        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_with_no_Room_throws_ArgumentEx()
        {
            var core = new Core(null);
            var b = new Buchung() { Von = DateTime.Now, Bis = DateTime.Now.AddDays(1), Raum = null };

            //Assert.Throws<ArgumentException>(() => core.CalcBuchungsGesamtPreis(b));

            new Action(() => core.CalcBuchungsGesamtPreis(b)).Should().Throw<ArgumentException>();

        }

        [Fact]
        public void CalcBuchungsGesamtPreis_Buchung_Von_Bis_Should_not_be_the_same_throws_ArgumentEx()
        {
            var core = new Core(null);
            var dt = new DateTime(2020, 1, 1, 1, 0, 0);
            var b = new Buchung() { Von = dt, Bis = dt, Raum = new Raum() };

            //gleiche Zeit
            new Action(() => core.CalcBuchungsGesamtPreis(b)).Should()
                .Throw<ArgumentException>(because: "gleiche Zeit")
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
            var core = new Core(null);

            var result = core.CalcBuchungsGesamtPreis(b);

            result.Should().Be(expected);

        }

    }
}