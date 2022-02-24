using ppedv.Hotelmanager2022.Model;
using ppedv.Hotelmanager2022.Model.Contracts;

namespace ppedv.Hotelmanager2022.Data.EFCore
{
    public class EfRepository : IRepository
    {
        EfContext _context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Set<T>().Update(entity);
        }
    }
}
