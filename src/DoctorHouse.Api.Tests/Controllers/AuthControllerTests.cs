using DoctorHouse.Api.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DoctorHouse.Api.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests : BaseControllerTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task Get_NoSession_401()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/auth/current");
            this.Assert401(response.Response);
        }

        [Test]
        public async Task Get_WithSession_SameUserAnd200()
        {
            var response = await this.GetAsync<UserModel>($"/api/v1/auth/current", defaultAuthentication: true);
            this.Assert200(response.Response);
            Assert.AreEqual(1, response.Content.Id);
        }
    }
}