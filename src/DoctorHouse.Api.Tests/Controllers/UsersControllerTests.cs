using System.Threading.Tasks;
using Beto.Core.Web.Api;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Tests.Models;
using NUnit.Framework;

namespace DoctorHouse.Api.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests : BaseControllerTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task Post_EmptyModel_BadRequest()
        {
            var response = await this.PostAsync<BaseApiErrorModel>("/api/v1/users", new NewUserModel());

            this.AssertBadRequest(response.Response);
            this.AssertTargetError(response.Content, "Email");
            this.AssertTargetError(response.Content, "Name");
            this.AssertTargetError(response.Content, "Password");
        }

        [Test]
        public async Task Post_UserEmailAlreadyExists_BadRequest()
        {
            var model = new NewUserModel { Name = "name", Email = "test@test.com", Password = "123456", LocationId = 1 };

            var response = await this.PostAsync<BaseApiErrorModel>("/api/v1/users", model);

            this.AssertBadRequest(response.Response);
            this.AssertErrorCode(response.Content, "UserEmailAlreadyExists");
        }

        [Test]
        public async Task Post_ValidModel_Created()
        {
            var model = new NewUserModel { Name = "name", Email = "test1@test.com", Password = "123456", LocationId = 1 };

            var response = await this.PostAsync<BaseModel>("/api/v1/users", model);

            this.AssertCreated(response.Response);
            Assert.Greater(response.Content.Id, 1);
        }

        [Test]
        public async Task GetById_WithoutToken_Unauthorized()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/users/{1}");

            this.AssertUnathorized(response.Response);
        }

        [Test]
        public async Task GetById_ValidId_Ok()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/users/{1}", defaultAuthentication: true);

            this.AssertOk(response.Response);
            Assert.AreEqual(1, response.Content.Id);
        }
    }
}