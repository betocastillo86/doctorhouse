using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DoctorHouse.Api.Models
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            this.RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            this.RuleFor(c => c.JobPlace)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            this.RuleFor(c => c.JobAddress)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            this.RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15);
        }
    }
}
