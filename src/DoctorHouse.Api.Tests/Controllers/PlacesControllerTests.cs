namespace DoctorHouse.Api.Tests.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Web.Api;
    using DoctorHouse.Api.Models;
    using DoctorHouse.Api.Tests.Models;
    using NUnit.Framework;

    public class PlacesControllerTests : BaseControllerTests
    {
        [Test]
        public async Task GetAll_NoSession_401()
        {
            var response = await this.GetAsync<PlaceModel>($"/api/v1/places");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetAll_InvalidFilter_400()
        {
            var response = await this.GetAsync<BaseApiErrorModel>($"/api/v1/places?page=-1", defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Page");
            this.AssertTargetError(response.Content, "LocationId");
        }

        [Test]
        public async Task GetAll_ValidFilter_200()
        {
            var response = await this.GetAsync<PaginationResponseModel<PlaceModel>>($"/api/v1/places?locationId=1", defaultAuthentication: true);

            this.Assert200(response.Response);
        }

        [Test]
        public async Task GetById_NoSession_401()
        {
            var response = await this.GetAsync<PlaceModel>($"/api/v1/places/1");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetById_NoSession_404()
        {
            var response = await this.GetAsync<PlaceModel>($"/api/v1/places/0", defaultAuthentication: true);

            this.Assert404(response.Response);
        }

        [Test]
        public async Task GetById_200()
        {
            var response = await this.GetAsync<PlaceModel>($"/api/v1/places/1", defaultAuthentication: true);

            this.Assert200(response.Response);
            Assert.AreEqual(1, response.Content.Id);
        }

        [Test]
        public async Task Post_NoSession_401()
        {
            var place = new PlaceModel
            {
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                GuestsAllowed = 1
            };

            var response = await this.PostAsync<PlaceModel>($"/api/v1/places", place);

            this.Assert401(response.Response);
        }

        [Test]
        public async Task Post_EmptyModel_400()
        {
            var place = new PlaceModel
            {
            };

            var response = await this.PostAsync<BaseApiErrorModel>($"/api/v1/places", place, defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Address");
            this.AssertTargetError(response.Content, "Description");
            this.AssertTargetError(response.Content, "Latitude");
            this.AssertTargetError(response.Content, "Longitude");
            this.AssertTargetError(response.Content, "Phone");
            this.AssertTargetError(response.Content, "GuestsAllowed");
        }

        [Test]
        public async Task Post_ValidModel_201()
        {
            var place = new PlaceModel
            {
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                GuestsAllowed = 1,
                Location = new LocationModel { Id = 1 }
            };

            var response = await this.PostAsync<BaseApiErrorModel>($"/api/v1/places", place, defaultAuthentication: true);

            this.Assert201(response.Response);
        }

        [Test]
        public async Task Put_NoSession_401()
        {
            var place = new PlaceModel
            {
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                GuestsAllowed = 1,
                Location = new LocationModel { Id = 1 }
            };

            var response = await this.PutAsync<PlaceModel>($"/api/v1/places/1", place);

            this.Assert401(response.Response);
        }

        [Test]
        public async Task Put_NotFound_404()
        {
            var place = new PlaceModel
            {
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                GuestsAllowed = 1,
                Location = new LocationModel { Id = 1 }
            };

            var response = await this.PutAsync<PlaceModel>($"/api/v1/places/0", place, defaultAuthentication: true);

            this.Assert404(response.Response);
        }

        [Test]
        public async Task Put_OtherOwnerUser_403()
        {
            var place = new PlaceModel
            {
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                GuestsAllowed = 1,
                Location = new LocationModel { Id = 1 }
            };

            // Authenticate another user
            await this.InsertUserAsync(authenticate: true);

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/places/1", place, defaultAuthentication: false, removeAuthentication: false);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task Put_EmptyModel_400()
        {
            var place = new PlaceModel { };

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/places/1", place, defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Address");
            this.AssertTargetError(response.Content, "Description");
            this.AssertTargetError(response.Content, "Latitude");
            this.AssertTargetError(response.Content, "Longitude");
            this.AssertTargetError(response.Content, "Phone");
            this.AssertTargetError(response.Content, "GuestsAllowed");
        }

        [Test]
        public async Task Put_ValidModel_200()
        {
            var place = await this.InsertPlaceAsync();

            place.Address = "address1";
            place.Description = "description1";
            place.Latitude = 1;
            place.Longitude = 2;
            place.Phone = "1234567";
            place.GuestsAllowed = 3;
            place.Location = new LocationModel { Id = 2 };

            var response = await this.PutAsync<EmptyContentModel>($"/api/v1/places/{place.Id}", place, defaultAuthentication: true);

            var updatedPlace = await this.GetPlaceById(place.Id);

            this.Assert200(response.Response);
            Assert.AreEqual(place.Address, updatedPlace.Address);
            Assert.AreEqual(place.Description, updatedPlace.Description);
            Assert.AreEqual(place.Latitude, updatedPlace.Latitude);
            Assert.AreEqual(place.Longitude, updatedPlace.Longitude);
            Assert.AreEqual(place.Phone, updatedPlace.Phone);
            Assert.AreEqual(place.GuestsAllowed, updatedPlace.GuestsAllowed);
            Assert.AreEqual(place.Location.Id, updatedPlace.Location.Id);
        }

        private async Task<PlaceModel> GetPlaceById(int id)
        {
            return (await this.GetAsync<PlaceModel>($"/api/v1/places/{id}", removeAuthentication: false)).Content;
        }
    }
}