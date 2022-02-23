using Microsoft.EntityFrameworkCore;
using ppedv.Hotelmanager2022.Model;

namespace ppedv.Hotelmanager2022.Data.EFCore
{
    public class EfContext : DbContext
    {
        public DbSet<Buchung>? Buchungen { get; set; }
        public DbSet<Gast>? Gaeste { get; set; }
        public DbSet<Raum>? Raeume { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Hotelmanager2022_dev;Trusted_Connection=true;");
        }
    }
}