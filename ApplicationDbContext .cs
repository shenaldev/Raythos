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
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftCustomization> AircraftCustomizations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new UsersTableSeeder());

            modelBuilder.Entity<Country>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new CountriesTableSeeder());

            modelBuilder.Entity<Address>().Property(a => a.AddressID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Aircraft>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AircraftCustomization>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Team>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TeamMember>().HasKey(tm => new { tm.TeamId, tm.UserId });

            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Payment>().Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
