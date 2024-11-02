using CookBook.Contracts;
using FluentValidation;

namespace CookBook.Services.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(createDto => createDto.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);
            RuleFor(createDto => createDto.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
