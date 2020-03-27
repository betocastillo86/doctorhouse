using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class NewUserModelValidator : AbstractValidator<NewUserModel>
    {
        public NewUserModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            this.RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(100);

            this.RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}
