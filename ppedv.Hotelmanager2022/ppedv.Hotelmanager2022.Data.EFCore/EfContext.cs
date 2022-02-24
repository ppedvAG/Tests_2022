using Microsoft.EntityFrameworkCore;
using ppedv.Hotelmanager2022.Model;

namespace ppedv.Hotelmanager2022.Data.EFCore
{
    public class EfContext : DbContext
    {
        private readonly string _conString;

        public DbSet<Buchung>? Buchungen { get; set; }
        public DbSet<Gast>? Gaeste { get; set; }
        public DbSet<Raum> Raeume { get; set; }

        public EfContext(string conString = "Server=(localdb)\\mssqllocaldb;Database=Hotelmanager2022_dev;Trusted_Connection=true;")
        {
            this._conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Raum>().HasMany(x => x.Buchungen).WithOne(x => x.Raum).IsRequired();
            modelBuilder.Entity<Gast>().HasMany(x => x.Buchungen).WithOne(x => x.Gast).IsRequired();
        }
    }
}