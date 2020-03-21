using Beto.Core.Web.Api.Models;
using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class PlaceFilterModelValidator : AbstractValidator<PlaceFilterModel>
    {
        public PlaceFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}