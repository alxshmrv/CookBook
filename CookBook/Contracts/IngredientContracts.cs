using CookBook.Models;

namespace CookBook.Contracts
{
    public record IngredientVm(int Id, string Name);
    public record IngredientDto(string Name, decimal Quantity, UnitOfMeasurement Unit);
}
