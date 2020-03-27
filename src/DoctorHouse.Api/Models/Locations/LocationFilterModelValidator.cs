using Beto.Core.Web.Api.Models;
using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class LocationFilterModelValidator : AbstractValidator<LocationFilterModel>
    {
        public LocationFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}