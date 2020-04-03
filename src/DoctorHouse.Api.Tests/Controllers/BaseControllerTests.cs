using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Beto.Core.Helpers;
using Beto.Core.Web.Api;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Tests.Models;
using DoctorHouse.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Controllers
{
    public class BaseControllerTests
    {
        protected ApiWebApplicationFactory factory;

        protected HttpClient client;

        protected string currentEmailAuthenticated = string.Empty;

        [OneTimeSetUp]
        public void InitiateServer()
        {
            this.factory = new ApiWebApplicationFactory();
            this.client = this.factory.CreateClient();
        }

        public async Task AuthenticateClient(string email = null, string password = null)
        {
            email = email ?? "test@test.com";

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("username", email));
            nvc.Add(new KeyValuePair<string, string>("password", password ?? "123456"));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            var req = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth") { Content = new FormUrlEncodedContent(nvc) };
            var authResponse = await this.client.SendAsync(req);

            var result = await this.GetResponseContent<AuthResponseModel>(authResponse);

            this.currentEmailAuthenticated = email;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }

        public void RemoveAuthentication()
        {
            this.client.DefaultRequestHeaders.Authorization = null;
        }

        protected async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        protected async Task<BaseApiErrorModel> GetErrorResponseModel(HttpResponseMessage response)
        {
            return await this.GetResponseContent<BaseApiErrorModel>(response);
        }

        protected StringContent GetRequestContent(object obj) =>
            new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        public async Task<TypedResponseModel<T>> PostAsync<T>(string requestUri, object model, bool defaultAuthentication = false, bool removeAuthentication = true) where T : class
        {
            if (defaultAuthentication)
            {
                await this.AuthenticateClient();
            }
            else if (removeAuthentication)
            {
                this.RemoveAuthentication();
            }

            var content = this.GetRequestContent(model);

            var response = await this.client.PostAsync(requestUri, content);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public async Task<TypedResponseModel<T>> PutAsync<T>(string requestUri, object model, bool defaultAuthentication = false, bool removeAuthentication = true) where T : class
        {
            if (defaultAuthentication)
            {
                await this.AuthenticateClient();
            }
            else if (removeAuthentication)
            {
                this.RemoveAuthentication();
            }

            var content = this.GetRequestContent(model);

            var response = await this.client.PutAsync(requestUri, content);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public async Task<TypedResponseModel<T>> GetAsync<T>(string requestUri, bool defaultAuthentication = false, bool removeAuthentication = true) where T : class
        {
            if (defaultAuthentication)
            {
                await this.AuthenticateClient();
            }
            else if (removeAuthentication)
            {
                this.RemoveAuthentication();
            }

            var response = await this.client.GetAsync(requestUri);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, bool defaultAuthentication = false, bool removeAuthentication = true)
        {
            if (defaultAuthentication)
            {
                await this.AuthenticateClient();
            }
            else if (removeAuthentication)
            {
                this.RemoveAuthentication();
            }

            return await this.client.DeleteAsync(requestUri);
        }

        protected void Assert400(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        protected void Assert200(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        protected void Assert201(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        protected void Assert401(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        protected void Assert404(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        protected void Assert403(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        protected void AssertTargetError(BaseApiErrorModel error, string target)
        {
            Assert.IsTrue(error.Error.Details.Any(c => c.Target.Equals(target)));
        }

        protected void AssertErrorCode(BaseApiErrorModel error, string errorCode)
        {
            Assert.AreEqual(error.Error.Code, errorCode);
        }

        protected async Task<UserModel> InsertUserAsync(string email = null, bool authenticate = false)
        {
            var model = new NewUserModel { Name = "name", Email = email ?? this.GetRandomEmail(), Password = "123456", LocationId = 1 };

            var response = await this.PostAsync<BaseModel>("/api/v1/users", model);

            var user = new UserModel
            {
                Id = response.Content.Id,
                Name = model.Name,
                Email = model.Email,
                Location = new LocationModel { Id = model.LocationId }
            };

            if (authenticate)
            {
                await this.AuthenticateClient(model.Email);
            }

            return user;
        }

        protected async Task<PlaceModel> InsertPlaceAsync(bool defaultAuthentication = true)
        {
            var place = new PlaceModel
            { 
                Address = "address",
                Description = "description",
                Latitude = 1,
                Longitude = 1,
                Phone = "123456",
                Location = new LocationModel { Id = 1 },
                GuestsAllowed = 1
            };

            var response = await this.PostAsync<BaseModel>(
                "/api/v1/places", 
                place, 
                defaultAuthentication: defaultAuthentication, 
                removeAuthentication: false);

            place.Id = response.Content.Id;

            return place;
        }

        protected async Task<RequestModel> InsertRequestAsync(int? place = null, bool defaultAuthentication = true)
        {
            var saveRequest = new SaveRequestModel
            {
                Description = "description",
                GuestTypeId = GuestType.MedicalStaff,
                PlaceId = place ?? 1,
                StartDate = DateTime.UtcNow.AddMinutes(1),
                EndDate = DateTime.UtcNow.AddMinutes(2)
            };

            var response = await this.PostAsync<BaseModel>(
                "/api/v1/requests",
                saveRequest,
                defaultAuthentication: defaultAuthentication,
                removeAuthentication: false);

            var request = new RequestModel
            { 
                Id = response.Content.Id,
                Description = saveRequest.Description,
                GuestType = saveRequest.GuestTypeId.Value,
                PlaceId = saveRequest.PlaceId,
                StartDate = saveRequest.StartDate.Value,
                EndDate = saveRequest.EndDate.Value
            };

            request.Id = response.Content.Id;

            return request;
        }

        protected string GetRandomEmail()
        {
            return $"{StringHelpers.GetRandomStringNoSpecialCharacters()}@{StringHelpers.GetRandomStringNoSpecialCharacters()}.com";
        }
    }
}