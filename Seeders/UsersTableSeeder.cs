using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raythos.Models;

namespace Raythos.Seeders
{
    public class UsersTableSeeder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasData(
                new User
                {
                    Id = 1,
                    FName = "Admin",
                    LName = "System",
                    Email = "admin@system.com",
                    Password = "admin",
                    ContactNo = "1234567890",
                    IsAdmin = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}
