using CookBook.CookBook_Database;
using CookBook.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CookBook
{
    public static class Composer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<CookBookDbContext>(options =>
            {
                options.UseNpgsql(
                    "Host = localhost; Port = 5432; Username = postgres; Password = 123456; Database = CookBook");
            });

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddRecipeRepository();
            return services;
        }
    }
}
