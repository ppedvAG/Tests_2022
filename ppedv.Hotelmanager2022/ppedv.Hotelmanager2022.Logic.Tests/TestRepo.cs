using ppedv.Hotelmanager2022.Model;
using ppedv.Hotelmanager2022.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.Hotelmanager2022.Logic.Tests
{
    internal class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {

            if (typeof(T) == typeof(Buchung))
            {
                var dt = new DateTime(2020, 1, 1, 1, 0, 0);
                var b1 = new Buchung()
                {
                    Id=1,
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
                return new[] { b1, b2 }.AsQueryable().Cast<T>();
            }

            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }
    }
}