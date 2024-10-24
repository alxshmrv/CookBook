using CookBook.Abstractions;

namespace CookBook.Services.Extentions
{
    public static class RecipeRepositoryExtention
    {
        public static IServiceCollection AddRecipeRepository(this IServiceCollection services) =>
            services.AddScoped<IRecipeRepository, RecipeRepository>();
    }
}
