using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raythos.Models;

namespace Raythos.Seeders
{
    public class CategoriesTableSeeder : IEntityTypeConfiguration<Category>
    {
        private Dictionary<string, string> categories = new Dictionary<string, string>()
        {
            { "Commercial Jets", "commercial-jets" },
            { "Executive Aircraft", "executive-aircraft" },
            { "Vintage Classics", "vintage-classics" },
            { "Luxury Business Jets", "luxury-business-jets" },
            { "Regional Propeller Planes", "regional-propeller-planes" },
            { "Military and Defense", "military-and-defense" },
            { "Amphibious Aircraft", "amphibious-aircraft" },
            { "Light Sport Aircraft", "light-sport-aircraft" },
            { "Helicopters and Rotorcraft", "helicopters-and-rotorcraft" },
            { "Training and Educational Aircraft", "training-and-educational-aircraft" },
            { "Experimental and Homebuilt", "experimental-and-homebuilt" },
            { "Aerobatic Flyers", "aerobatic-flyers" },
            { "Cargo and Freight Haulers", "cargo-and-freight-haulers" },
            { "Electric and Hybrid Models", "electric-and-hybrid-models" },
            { "Ultralight Planes", "ultralight-planes" }
        };

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasData(
                categories.Select(
                    (category, index) =>
                        new Category
                        {
                            Id = index + 1,
                            Name = category.Key,
                            Slug = category.Value,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        }
                )
            );
        }
    }
}
