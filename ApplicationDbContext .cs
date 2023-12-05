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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.ApplyConfiguration(new UsersTableSeeder());
        }
    }
}
