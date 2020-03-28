using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class SaveRequestModelValidator : AbstractValidator<SaveRequestModel>
    {
        public SaveRequestModelValidator()
        {
            this.RuleFor(c => c.Description)
                .MaximumLength(2000);

            this.RuleFor(c => c.PlaceId)
                .GreaterThan(0);

            this.RuleFor(c => c.StatusId)
                .NotNull()
                .IsInEnum();

            this.RuleFor(c => c.GuestTypeId)
                .NotNull()
                .IsInEnum();
        }
    }
}