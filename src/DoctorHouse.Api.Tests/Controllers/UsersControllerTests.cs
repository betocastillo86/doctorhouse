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
        public async Task Post_EmptyModel_400()
        {
            var response = await this.PostAsync<BaseApiErrorModel>("/api/v1/users", new NewUserModel());

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Email");
            this.AssertTargetError(response.Content, "Name");
            this.AssertTargetError(response.Content, "Password");
        }

        [Test]
        public async Task Post_UserEmailAlreadyExists_400()
        {
            var model = new NewUserModel { Name = "name", Email = "test@test.com", Password = "123456", LocationId = 1 };

            var response = await this.PostAsync<BaseApiErrorModel>("/api/v1/users", model);

            this.Assert400(response.Response);
            this.AssertErrorCode(response.Content, "UserEmailAlreadyExists");
        }

        [Test]
        public async Task Post_ValidModel_201()
        {
            var model = new NewUserModel { Name = "name", Email = "test1@test.com", Password = "123456", LocationId = 1 };

            var response = await this.PostAsync<BaseModel>("/api/v1/users", model);

            this.Assert201(response.Response);
            Assert.Greater(response.Content.Id, 1);
        }

        [Test]
        public async Task GetById_WithoutToken_401()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/users/{1}");

            this.Assert401(response.Response);
        }

        [Test]
        public async Task GetById_ValidId_200()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/users/{1}", defaultAuthentication: true);

            this.Assert200(response.Response);
            Assert.AreEqual(1, response.Content.Id);
        }

        [Test]
        public async Task Put_EmptyModel_400()
        {
            var user = await this.InsertUserAsync();

            await this.AuthenticateClient(user.Email);

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/users/{user.Id}", new UserModel { }, removeAuthentication: false);

            this.Assert400(response.Response);
            this.AssertTargetError(response.Content, "Email");
            this.AssertTargetError(response.Content, "Name");
            this.AssertTargetError(response.Content, "Location");
            this.AssertTargetError(response.Content, "JobPlace");
            this.AssertTargetError(response.Content, "JobAddress");
            this.AssertTargetError(response.Content, "PhoneNumber");
        }

        [Test]
        public async Task Put_UserEmailAlreadyExists_400()
        {
            var user = await this.InsertUserAsync();

            await this.AuthenticateClient(user.Email);

            user.JobPlace = "jobplace";
            user.JobAddress = "jobaddress";
            user.PhoneNumber = "123456";
            user.Email = "test@test.com";

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/users/{user.Id}", user, removeAuthentication: false);

            this.Assert400(response.Response);
            this.AssertErrorCode(response.Content, "UserEmailAlreadyExists");
        }

        [Test]
        public async Task Put_ModifyOtherUserAccount_403()
        {
            var user = await this.InsertUserAsync();

            user.JobPlace = "jobplace";
            user.JobAddress = "jobaddress";
            user.PhoneNumber = "123456";

            var response = await this.PutAsync<BaseApiErrorModel>($"/api/v1/users/{user.Id}", user, defaultAuthentication: true);

            this.Assert403(response.Response);
        }

        [Test]
        public async Task Put_ValidModel_200()
        {
            var user = await this.InsertUserAsync();

            await this.AuthenticateClient(user.Email);

            user.Name = "newname";
            user.Email = this.GetRandomEmail();
            user.Location = new LocationModel { Id = 2 };
            user.JobPlace = "jobplace";
            user.JobAddress = "jobaddress";
            user.PhoneNumber = "123456";

            var response = await this.PutAsync<UserModel>($"/api/v1/users/{user.Id}", user, removeAuthentication: false);

            var userUpdated = await this.GetUserById(user.Id);

            this.Assert200(response.Response);
            Assert.AreEqual(user.Name, userUpdated.Name);
            Assert.AreEqual(user.Email, userUpdated.Email);
            Assert.AreEqual(user.Location.Id, userUpdated.Location.Id);
            Assert.AreEqual(user.JobPlace, userUpdated.JobPlace);
            Assert.AreEqual(user.JobAddress, userUpdated.JobAddress);
            Assert.AreEqual(user.PhoneNumber, userUpdated.PhoneNumber);
        }

        private async Task<UserModel> GetUserById(int id)
        {
            return (await this.GetAsync<UserModel>($"/api/v1/users/{id}", removeAuthentication: false)).Content;
        }
    }
}