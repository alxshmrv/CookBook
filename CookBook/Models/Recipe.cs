namespace CookBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Algorithm { get; set; } = default!;
        public RecipeCategory RecipeCategory { get; set; }
        public List<Ingredient>? Ingredients { get; set; } // убрать nullable
        // public User User { get; set; }
        public int Score { get; set; }
    }
}
