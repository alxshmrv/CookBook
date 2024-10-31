using CookBook.Contracts;
using FluentValidation;

namespace CookBook.Services.Validators
{
    public class CreateIngredientValidator : AbstractValidator<IngredientDto>
    {
        public CreateIngredientValidator()
        {
            RuleFor(createDto => createDto.Name)
                .NotEmpty().WithMessage("Название ингредиента обязательно.")
                .NotNull()
                .MaximumLength(200).WithMessage("Слишком много символов");
            RuleFor(createDto => createDto.Quantity)
                .NotEmpty().WithMessage("Добавьте количество")
                .NotNull()
                .GreaterThan(0).WithMessage("Минимальное количество - 1")
                .LessThan(1000000).WithMessage("Максимальное количество - 1000000");
        }
    }
}
