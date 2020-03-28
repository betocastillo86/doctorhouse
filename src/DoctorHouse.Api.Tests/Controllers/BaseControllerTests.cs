using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Beto.Core.Web.Api;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Controllers
{
    public class BaseControllerTests
    {
        protected ApiWebApplicationFactory factory;

        protected HttpClient client;

        [OneTimeSetUp]
        public void InitiateServer()
        {
            this.factory = new ApiWebApplicationFactory();
            this.client = this.factory.CreateClient();
        }

        public async Task AuthenticateClient(string email = null, string password = null)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("username", email ?? "test@test.com"));
            nvc.Add(new KeyValuePair<string, string>("password", password ?? "123456"));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            var req = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth") { Content = new FormUrlEncodedContent(nvc) };
            var authResponse = await this.client.SendAsync(req);

            var result = await this.GetResponseContent<AuthResponseModel>(authResponse);

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

        protected void AssertBadRequest(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        protected void AssertOk(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        protected void AssertCreated(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        protected void AssertUnathorized(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        protected void AssertForbidden(HttpResponseMessage result)
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
    }
}