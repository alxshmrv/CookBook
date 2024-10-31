using CookBook.Contracts;
using FluentValidation;

namespace CookBook.Services.Validators
{
    public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeDto>
    {
        public UpdateRecipeValidator()
        {
            RuleFor(updateDto => updateDto.Name)
                .NotEmpty().WithMessage("Название рецепта обязательно.")
                .NotNull()
                .MaximumLength(200);
            RuleFor(updateDto => updateDto.Algorithm)
                .NotEmpty()
                .NotNull()
                .MaximumLength(4000);
            RuleFor(updateDto => updateDto.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(4000);
            RuleFor(updateDto => updateDto.Ingredients)
                .NotEmpty()
                .NotNull();
        }
    }
}
