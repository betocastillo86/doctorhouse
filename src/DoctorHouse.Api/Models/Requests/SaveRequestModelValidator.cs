using System;
using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class SaveRequestModelValidator : AbstractValidator<SaveRequestModel>
    {
        public SaveRequestModelValidator()
        {
            this.RuleFor(c => c.Description)
                .NotNull()
                .MaximumLength(2000);

            this.RuleFor(c => c.PlaceId)
                .GreaterThan(0);

            this.RuleFor(c => c.StartDate)
                .NotNull()
                .GreaterThan(DateTime.UtcNow);

            this.RuleFor(c => c.EndDate)
                .NotNull()
                .GreaterThan(c => c.StartDate)
                .WithMessage("End date must after start date");

            this.RuleFor(c => c.GuestTypeId)
                .NotNull()
                .IsInEnum();
        }
    }
}