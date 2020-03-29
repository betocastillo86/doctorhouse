using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class PlaceModelValidator : AbstractValidator<PlaceModel>
    {
        public PlaceModelValidator()
        {
            this.RuleFor(c => c.Latitude)
                .NotNull()
                .GreaterThan(-90)
                .LessThan(90);

            this.RuleFor(c => c.Longitude)
                .NotNull()
                .GreaterThan(-180)
                .LessThan(180);

            this.RuleFor(c => c.Address)
                .NotNull()
                .NotEmpty()
                .MaximumLength(140);

            this.RuleFor(c => c.Phone)
                .NotNull()
                .NotEmpty()
                .MaximumLength(14);

            this.RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(4000);

            this.RuleFor(c => c.GuestsAllowed)
                .NotNull()
                .GreaterThan((byte)0)
                .LessThan((byte)20);

            this.RuleFor(c => c.Location)
                .NotNull();
        }
    }
}