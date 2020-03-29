using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorHouse.Api.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Validators
{
    public class SaveRequestValidatorTests
    {
        private SaveRequestModelValidator validator;

        private SaveRequestModel model;

        [SetUp]
        public void Setup()
        {
            this.validator = new SaveRequestModelValidator();
            this.model = new SaveRequestModel();
        }

        [Test]
        public void Validate_EmptyModel_ShouldFail()
        {
            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("PlaceId");
            result.ShouldHaveValidationErrorFor("Description");
            result.ShouldHaveValidationErrorFor("GuestTypeId");
            result.ShouldHaveValidationErrorFor("StartDate");
            result.ShouldHaveValidationErrorFor("EndDate");
        }

        [Test]
        public void Validate_StartDateLessThanNow_ShouldFail()
        {
            this.model.StartDate = DateTime.UtcNow.AddMinutes(-1);

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("StartDate");
        }

        [Test]
        public void Validate_EndDateLessThanStartDate_ShouldFail()
        {
            this.model.StartDate = DateTime.UtcNow.AddMinutes(10);
            this.model.EndDate = DateTime.UtcNow.AddMinutes(9);

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("EndDate");
        }

        [Test]
        public void Validate_ValidModel_ShouldPass()
        {
            this.model.PlaceId = 1;
            this.model.Description = "thepassword";
            this.model.GuestTypeId = Data.GuestType.MedicalStaff;
            this.model.StartDate = DateTime.UtcNow.AddMinutes(1);
            this.model.EndDate = DateTime.UtcNow.AddMinutes(2);

            var result = this.validator.TestValidate(this.model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_InvalidLengths_ShouldFail()
        {
            this.model.Description = new string('*', 2001);

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Description");
        }
    }
}
