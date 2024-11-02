namespace CookBook.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Password { get; set; } = default!;
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
