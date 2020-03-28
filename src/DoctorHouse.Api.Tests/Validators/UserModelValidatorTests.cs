using DoctorHouse.Api.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Validators
{
    [TestFixture]
    public class UserModelValidatorTests
    {
        private UserModelValidator validator;

        private UserModel model;

        [SetUp]
        public void Setup()
        {
            this.validator = new UserModelValidator();
            this.model = new UserModel();
        }

        [Test]
        public void Validate_EmptyModel_ShouldFail()
        {
            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Email");
            result.ShouldHaveValidationErrorFor("Name");
            result.ShouldHaveValidationErrorFor("Location");
            result.ShouldHaveValidationErrorFor("JobPlace");
            result.ShouldHaveValidationErrorFor("JobAddress");
            result.ShouldHaveValidationErrorFor("PhoneNumber");
        }

        [Test]
        public void Validate_InvalidEmail_ShouldFail()
        {
            this.model.Name = "thename";
            this.model.Location = new LocationModel { Id = 1 };
            this.model.JobAddress = "jobaddress";
            this.model.PhoneNumber = "PhoneNumber";
            this.model.JobPlace = "JobPlace";
            this.model.Email = "anyemail";

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Email");
        }

        [Test]
        public void Validate_ValidModel_ShouldPass()
        {
            this.model.Name = "thename";
            this.model.Location = new LocationModel { Id = 1 };
            this.model.JobAddress = "jobaddress";
            this.model.PhoneNumber = "PhoneNumber";
            this.model.JobPlace = "JobPlace";
            this.model.Email = "anyemailq@email.com";

            var result = this.validator.TestValidate(this.model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_InvalidLengths_ShouldFail()
        {
            this.model.Name = new string('*', 101);
            this.model.JobAddress = new string('*', 101);
            this.model.PhoneNumber = new string('*', 16);
            this.model.JobPlace = new string('*', 101);
            this.model.Email = "anyemail@any.com";

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Name");
            result.ShouldHaveValidationErrorFor("JobPlace");
            result.ShouldHaveValidationErrorFor("JobAddress");
            result.ShouldHaveValidationErrorFor("PhoneNumber");
        }
    }
}