using Beto.Core.Web.Api.Models;
using FluentValidation;

namespace DoctorHouse.Api.Models.Requests
{
    public class RequestFilterModelValidator : AbstractValidator<RequestFilterModel>
    {
        public RequestFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.UserId)
                .NotNull();
        }
    }
}