using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.Hotelmanager2022.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ppedv.Hotelmanager2022.Data.EFCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void Can_create_DB()
        {
            using var con = new EfContext("Server=(localdb)\\mssqllocaldb;Database=Hotelmanager2022_CreateDelete;Trusted_Connection=true;");
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        public void Can_insert_Raum()
        {
            using var con = new EfContext();
            con.Database.EnsureCreated();
            var raum = new Raum() { Nummer = "TestRaum01", Raucher = true, AnzBetten = 2, PreisProTag = 33.33m };

            con.Add(raum);
            int rows = con.SaveChanges();

            Assert.Equal(1, rows);
        }

        [Fact]
        public void Can_CRUD_Raum()
        {
            var raum = new Raum() { Nummer = $"TestRaum02_{Guid.NewGuid()}", Raucher = true, AnzBetten = 2, PreisProTag = 33.33m };
            var newNummer = $"TestRaumNEU_{Guid.NewGuid()}";

            using (var con = new EfContext()) //CREATE
            {
                con.Database.EnsureCreated();

                con.Add(raum);
                con.SaveChanges();
            }

            using (var con = new EfContext()) //READ + UPDATE
            {
                var loaded = con.Raeume.Find(raum.Id);
                Assert.Equal(raum.Nummer, loaded.Nummer);

                //UPDATE
                loaded.Nummer = newNummer;
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new EfContext()) //check UPDATE + DELETE
            {
                var loaded = con.Raeume.Find(raum.Id);
                Assert.Equal(newNummer, loaded.Nummer);

                //DELETE
                con.Remove(loaded);
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new EfContext()) //check DELETE
            {
                var loaded = con.Raeume.Find(raum.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_save_all_Raum_properties()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            //var raum = fix.Build<Raum>().Without(x => x.Buchungen).Without(x => x.Id).Create();//nur Raum

            var raum = fix.Create<Raum>();

            using (var con = new EfContext())
            {
                con.Database.EnsureCreated();

                con.Add(raum);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Raeume.Find(raum.Id);
                loaded.Should().BeEquivalentTo(raum, c => c.IgnoringCyclicReferences());


                //cleanup 
                //foreach (var b in loaded.Buchungen)
                //{
                //    con.Remove(b);
                //    con.Remove(b.Gast);
                //}
                //con.Remove(loaded);
                //con.SaveChanges();
            }
        }


        [Fact]
        public void When_deleting_a_Raum_with_Buchungen_throw_exception()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void When_deleting_a_Buchung_the_Raum_and_Gast_should_not_be_deleted()
        {
            throw new NotImplementedException();
        }

    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}