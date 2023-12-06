using Microsoft.EntityFrameworkCore;
using Raythos.Models;
using Raythos.Seeders;

namespace Raythos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.UserId).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new UsersTableSeeder());
            modelBuilder.Entity<Country>().Property(c => c.CountryId).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new CountriesTableSeeder());
            modelBuilder.Entity<Address>().Property(a => a.AddressID).ValueGeneratedOnAdd();
        }
    }
}
