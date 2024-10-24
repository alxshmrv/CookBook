using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CookBook.CookBook_Database
{
    public class CookBookDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<User> Users { get; set; }

        public CookBookDbContext(DbContextOptions<CookBookDbContext> options) : base(options)
        {
            
        }
    }
}
