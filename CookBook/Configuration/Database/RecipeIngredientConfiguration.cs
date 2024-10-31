using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CookBook.Models;

namespace CookBook.Configuration.Database
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> modelBuilder)
        {
            modelBuilder
                .HasKey(x => new { x.RecipeId, x.IngredientId });
            modelBuilder
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);
            modelBuilder
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);
        }
    }
}
