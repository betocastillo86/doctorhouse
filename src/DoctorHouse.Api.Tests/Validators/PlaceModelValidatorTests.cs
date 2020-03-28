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
    [TestFixture]
    public class PlaceModelValidatorTests
    {
        private PlaceModelValidator validator;

        private PlaceModel model;

        [SetUp]
        public void Setup()
        {
            this.validator = new PlaceModelValidator();
            this.model = new PlaceModel();
        }

        [Test]
        public void Validate_EmptyModel_ShouldFail()
        {
            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Latitude");
            result.ShouldHaveValidationErrorFor("Longitude");
            result.ShouldHaveValidationErrorFor("Address");
            result.ShouldHaveValidationErrorFor("Phone");
            result.ShouldHaveValidationErrorFor("Description");
            result.ShouldHaveValidationErrorFor("GuestAllowed");
            result.ShouldHaveValidationErrorFor("Location");
        }

        [Test]
        [TestCase(91)]
        [TestCase(-91)]
        public void Validate_InvalidLatitude_ShouldFail(decimal latitude)
        {
            this.model.Latitude = latitude;

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Latitude");
        }

        [Test]
        [TestCase(181)]
        [TestCase(-181)]
        public void Validate_InvalidLongitude_ShouldFail(decimal longitude)
        {
            this.model.Longitude = longitude;

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Longitude");
        }

        [Test]
        [TestCase(0)]
        [TestCase(21)]
        public void Validate_InvalidGuests_ShouldFail(byte guests)
        {
            this.model.GuestAllowed = guests;

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("GuestAllowed");
        }

        [Test]
        public void Validate_InvalidLengths_ShouldFail()
        {
            this.model.Description = new string('*', 5000);
            this.model.Address = new string('*', 150);
            this.model.Phone = new string('*', 15);

            var result = this.validator.TestValidate(this.model);

            result.ShouldHaveValidationErrorFor("Description");
            result.ShouldHaveValidationErrorFor("Address");
            result.ShouldHaveValidationErrorFor("Phone");
        }

        [Test]
        public void Validate_ValidModel_ShouldPass()
        {
            this.model.Latitude = 1;
            this.model.Longitude = 1;
            this.model.Description = "desc";
            this.model.Phone = "123";
            this.model.Address = "address";
            this.model.GuestAllowed = 1;
            this.model.Location = new LocationModel { Id = 1 };

            var result = this.validator.TestValidate(this.model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
