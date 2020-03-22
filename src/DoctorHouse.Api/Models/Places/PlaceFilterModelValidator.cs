using Beto.Core.Web.Api.Models;
using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class PlaceFilterModelValidator : AbstractValidator<PlaceFilterModel>
    {
        public PlaceFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.LocationId)
                .NotNull()
                .GreaterThan(0);

            this.When(c => c.CountGuests.HasValue, () =>
            {
                this.RuleFor(c => c.CountGuests)
                    .NotNull()
                    .GreaterThan((byte)0)
                    .LessThan((byte)20);
            });            
        }
    }
}