using DiceRoller.Identity.API.Models.Requests;
using FluentValidation;

namespace DiceRoller.Identity.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(200);
        }
    }
}
