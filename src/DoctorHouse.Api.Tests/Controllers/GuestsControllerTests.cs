namespace DoctorHouse.Api.Tests.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Web.Api;
    using DoctorHouse.Api.Models;
    using DoctorHouse.Api.Tests.Models;
    using NUnit.Framework;

    public class GuestsControllerTests : BaseControllerTests
    {
        [Test]
        public async Task GetAll_NoSession_401()
        {
            var response = await this.GetAsync<GuestModel>($"/api/v1/requests/2/guests");

            this.Assert401(response.Response);
        }
    }
}