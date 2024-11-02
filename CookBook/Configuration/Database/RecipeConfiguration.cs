using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CookBook.Models;

namespace CookBook.Configuration.Database
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> modelBuilder)
        {
            modelBuilder
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder
                .Property(r => r.Description)
                .HasMaxLength(1000);

            modelBuilder
                .Property(r => r.Algorithm)
                .HasMaxLength(4000);
            modelBuilder
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
