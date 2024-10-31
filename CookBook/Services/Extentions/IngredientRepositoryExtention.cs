using CookBook.Abstractions;

namespace CookBook.Services.Extentions
{
    public static  class IngredientRepositoryExtention
    {
        public static IServiceCollection AddIngredientRepository(this IServiceCollection services) =>
           services.AddScoped<IIngredientRepository, IngredientRepository>();
    }
}
