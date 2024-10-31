using CookBook.Contracts;
using FluentValidation;

namespace CookBook.Services.Validators
{
    public class CreateRecipeValidator : AbstractValidator<CreateRecipeDto>
    {
        public CreateRecipeValidator()
        {
            RuleFor(createDto => createDto.Name)
                .NotEmpty().WithMessage("Название рецепта обязательно.")
                .NotNull()
                .MaximumLength(200).WithMessage("Слишком много символов");
            RuleFor(createDto => createDto.Algorithm)
                .NotEmpty().WithMessage("Алгоритм рецепта обязателен.")
                .NotNull()
                .MaximumLength(4000);
            RuleFor(createDto => createDto.Description)
                .NotEmpty().WithMessage("Описание рецепта обязательно.")
                .NotNull()
                .MaximumLength(4000);
            RuleFor(createDto => createDto.Ingredients)
                .NotEmpty().WithMessage("Нужно указать ингредиенты")
                .NotNull();
        }
    }
}
