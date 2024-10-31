using CookBook.Configuration.Database;
using CookBook.CookBook_Database;
using CookBook.Services.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CookBook
{
    public static class Composer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<CookBookDbConnectionSettings>(
                configuration.GetRequiredSection(
                    nameof(CookBookDbConnectionSettings)));
            services.AddDbContext<CookBookDbContext>(options =>
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var settings = scope
                .ServiceProvider
                .GetRequiredService<IOptions<CookBookDbConnectionSettings>>()
                .Value;

                options.UseNpgsql(settings.ConnectionString);
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddRecipeRepository();
            services.AddIngredientRepository();
            return services;
        }
    }
}
