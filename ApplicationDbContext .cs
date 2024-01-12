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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftOption> AircraftOptions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new UsersTableSeeder());

            modelBuilder.Entity<Country>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new CountriesTableSeeder());

            modelBuilder.Entity<Address>().Property(a => a.AddressID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new CategoriesTableSeeder());

            modelBuilder.Entity<Aircraft>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AircraftOption>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Team>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TeamMember>().HasKey(tm => new { tm.TeamId, tm.UserId });

            modelBuilder.Entity<Cart>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>().Property(oi => oi.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Payment>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Inventory>().Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
