using System;
using System.Threading.Tasks;
using Beto.Core.Web.Api;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Tests.Models;
using DoctorHouse.Data;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Controllers
{
    public class RequestsControllerTests : BaseControllerTests
    {
        [Test]
        public async Task GetAllByRequester_NoSession_401()
        {
            var response = await this.GetAsync<RequestModel>($"/api/v1/requests/get-all-by-requesterid?userId=1");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetAllByOwner_NoSession_401()
        {
            var response = await this.GetAsync<RequestModel>($"/api/v1/requests/get-all-by-ownerid?userId=1");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetAllByRequester_InvalidFilter_400()
        {
            var response = await this.GetAsync<BaseApiErrorModel>($"/api/v1/requests/get-all-by-requesterid?page=-1", defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Page");
            this.AssertTargetError(response.Content, "UserId");
        }

        [Test]
        public async Task GetAllByOwner_InvalidFilter_400()
        {
            var response = await this.GetAsync<BaseApiErrorModel>($"/api/v1/requests/get-all-by-ownerid?page=-1", defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Page");
            this.AssertTargetError(response.Content, "UserId");
        }

        [Test]
        public async Task GetAllByRequester_OtherUser_403()
        {
            var response = await this.GetAsync<BaseApiErrorModel>($"/api/v1/requests/get-all-by-requesterid?userId=2", defaultAuthentication: true);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task GetAllByOwner_OtherUser_403()
        {
            var response = await this.GetAsync<BaseApiErrorModel>($"/api/v1/requests/get-all-by-ownerid?userId=2", defaultAuthentication: true);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task GetAllByOwner_ValidFilter_200()
        {
            var response = await this.GetAsync<PaginationResponseModel<RequestModel>>($"/api/v1/requests/get-all-by-ownerid?userId=1", defaultAuthentication: true);

            this.Assert200(response.Response);
        }

        [Test]
        public async Task GetAllByRequester_ValidFilter_200()
        {
            var response = await this.GetAsync<PaginationResponseModel<RequestModel>>($"/api/v1/requests/get-all-by-requesterid?userId=1", defaultAuthentication: true);

            this.Assert200(response.Response);
        }

        [Test]
        public async Task GetById_NoSession_401()
        {
            var response = await this.GetAsync<EmptyContentModel>($"/api/v1/requests/1");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetById_NoSession_404()
        {
            var response = await this.GetAsync<EmptyContentModel>($"/api/v1/requests/0", defaultAuthentication: true);

            this.Assert404(response.Response);
        }

        [Test]
        public async Task GetById_OtherUser_403()
        {
            // Creates a user owner
            var owner = await this.InsertUserAsync(authenticate: true);

            // Creates a place
            var place = await this.InsertPlaceAsync(defaultAuthentication: false);

            // Creates a requester
            var requester = await this.InsertUserAsync(authenticate: true);

            // Creates a request to the place created
            var request = await this.InsertRequestAsync(place: place.Id, defaultAuthentication: false);

            // Tries to get the request with the default authentication
            var response = await this.GetAsync<BaseModel>($"/api/v1/requests/{request.Id}", defaultAuthentication: true);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task GetById_SameOwner_200()
        {
            // Creates a user owner
            var owner = await this.InsertUserAsync(authenticate: true);

            // Creates a place
            var place = await this.InsertPlaceAsync(defaultAuthentication: false);

            // Creates a requester
            var requester = await this.InsertUserAsync(authenticate: true);

            // Creates a request to the place created
            var request = await this.InsertRequestAsync(place: place.Id, defaultAuthentication: false);

            await this.AuthenticateClient(email: owner.Email);

            // Tries to get the request with the owner
            var response = await this.GetAsync<BaseModel>($"/api/v1/requests/{request.Id}", defaultAuthentication: false, removeAuthentication: false);

            this.Assert200(response.Response);
        }

        [Test]
        public async Task GetById_SameRequester_200()
        {
            // Creates a user owner
            var owner = await this.InsertUserAsync(authenticate: true);

            // Creates a place
            var place = await this.InsertPlaceAsync(defaultAuthentication: false);

            // Creates a requester
            var requester = await this.InsertUserAsync(authenticate: true);

            // Creates a request to the place created
            var request = await this.InsertRequestAsync(place: place.Id, defaultAuthentication: false);

            // Tries to get the request with the requester
            var response = await this.GetAsync<BaseModel>($"/api/v1/requests/{request.Id}", defaultAuthentication: false, removeAuthentication: false);

            this.Assert200(response.Response);
        }

        [Test]
        public async Task Post_NoSession_401()
        {
            var request = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PostAsync<RequestModel>($"/api/v1/requests", request);

            this.Assert401(response.Response);
        }

        [Test]
        public async Task Post_EmptyModel_400()
        {
            var request = new RequestModel
            {
            };

            var response = await this.PostAsync<BaseApiErrorModel>($"/api/v1/requests", request, defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "GuestTypeId");
            this.AssertTargetError(response.Content, "Description");
            this.AssertTargetError(response.Content, "PlaceId");
            this.AssertTargetError(response.Content, "StartDate");
            this.AssertTargetError(response.Content, "EndDate");
        }

        [Test]
        public async Task Post_ValidModel_201()
        {
            var request = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PostAsync<BaseApiErrorModel>($"/api/v1/requests", request, defaultAuthentication: true);

            this.Assert201(response.Response);
        }

        [Test]
        public async Task Put_NoSession_401()
        {
            var request = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PutAsync<EmptyContentModel>($"/api/v1/requests/1", request);

            this.Assert401(response.Response);
        }

        [Test]
        public async Task Put_NotFound_404()
        {
            var request = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PutAsync<EmptyContentModel>($"/api/v1/requests/0", request, defaultAuthentication: true);

            this.Assert404(response.Response);
        }

        [Test]
        public async Task Put_OtherUser_403()
        {
            var request = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            // Authenticate another user
            await this.InsertUserAsync(authenticate: true);

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/requests/1", request, defaultAuthentication: false, removeAuthentication: false);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task Put_EmptyModel_400()
        {
            var request = new RequestModel { };

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/requests/1", request, defaultAuthentication: true);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "GuestTypeId");
            this.AssertTargetError(response.Content, "Description");
            this.AssertTargetError(response.Content, "PlaceId");
            this.AssertTargetError(response.Content, "StartDate");
            this.AssertTargetError(response.Content, "EndDate");
        }

        [Test]
        public async Task Put_ValidModelAndUpdated_200()
        {
            var request = await this.InsertRequestAsync();

            var requestModel = new SaveRequestModel
            {
                Description = "description1",
                GuestTypeId = GuestType.Traveller,
                PlaceId = 5,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PutAsync<EmptyContentModel>($"/api/v1/requests/{request.Id}", requestModel, defaultAuthentication: true);

            var updatedRequest = await this.GetRequestById(request.Id);

            this.Assert200(response.Response);
            Assert.AreEqual(StatusType.New, updatedRequest.Status); // remains new
            Assert.AreEqual(requestModel.Description, updatedRequest.Description);
            Assert.AreEqual(requestModel.GuestTypeId, updatedRequest.GuestType);
            Assert.AreEqual(requestModel.StartDate.Value.ToString("yyyy/MM/dd HH:mm:ss"), updatedRequest.StartDate.ToString("yyyy/MM/dd HH:mm:ss"));
            Assert.AreEqual(requestModel.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss"), updatedRequest.EndDate.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        private async Task<RequestModel> GetRequestById(int id)
        {
            return (await this.GetAsync<RequestModel>($"/api/v1/requests/{id}", removeAuthentication: false)).Content;
        }
    }
}