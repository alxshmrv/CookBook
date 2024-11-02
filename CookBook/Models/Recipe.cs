namespace CookBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Algorithm { get; set; } = default!;
        public RecipeCategory RecipeCategory { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; } = default!;
        public decimal Score { get; set; } = default;
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
