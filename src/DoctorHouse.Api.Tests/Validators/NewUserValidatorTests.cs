using DoctorHouse.Api.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Validators
{
    [TestFixture]
    public class NewUserValidatorTests
    {
        private NewUserModelValidator validator;

        private NewUserModel model;

        [SetUp]
        public void Setup()
        {
            this.validator = new NewUserModelValidator();
            this.model = new NewUserModel();
        }

        [Test]
        public void Validate_EmptyModel_ShouldFail()
        {
            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Email");
            result.ShouldHaveValidationErrorFor("Name");
            result.ShouldHaveValidationErrorFor("Password");
        }

        [Test]
        public void Validate_InvalidEmail_ShouldFail()
        {
            this.model.Name = "thename";
            this.model.Password = "thepassword";
            this.model.Email = "anyemail";

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Email");
        }

        [Test]
        public void Validate_ValidModel_ShouldPass()
        {
            this.model.Name = "thename";
            this.model.Password = "thepassword";
            this.model.Email = "anyemail@any.com";

            var result = this.validator.TestValidate(this.model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_InvalidLengths_ShouldFail()
        {
            this.model.Name = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
            this.model.Password = "123";
            this.model.Email = "anyemail@any.com";

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Name");
            result.ShouldHaveValidationErrorFor("Password");
        }
    }
}