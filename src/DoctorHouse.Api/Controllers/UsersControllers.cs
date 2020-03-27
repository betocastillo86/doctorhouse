using System.Threading.Tasks;
using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Helpers;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Models;
using DoctorHouse.Business.Exceptions;
using DoctorHouse.Business.Security;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/users")]
    public class UsersController : BaseApiController
    {
        private readonly IUserService userService;

        private readonly IWebCacheService webCacheService;

        private readonly IMapper mapper;

        private readonly IWorkContext workContext;

        public UsersController(
            IMessageExceptionFinder messageExceptionFinder,
            IUserService userService,
            IWorkContext workContext,
            IWebCacheService webCacheService,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.userService = userService;
            this.workContext = workContext;
            this.webCacheService = webCacheService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("{id:int}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await Task.FromResult(this.webCacheService.GetUserById(id));

            if (user == null)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<UserModel>(user);

            return this.Ok(model);
        }

        [RequiredModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserModel model)
        {
            var salt = StringHelpers.GetRandomString();

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Salt = !string.IsNullOrEmpty(model.Password) ? salt : null,
                LocationId = model.LocationId,
                Password = !string.IsNullOrEmpty(model.Password) ? StringHelpers.ToSha1(model.Password, salt) : null,
                UserType = (short)model.UserType
            };

            try
            {
                await this.userService.InsertAsync(user);

                return this.Created("GetUserById", user.Id);
            }
            catch (DoctorHouseException ex)
            {
                return this.BadRequest(ex);
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        [RequiredModel]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel model)
        {
            if (this.workContext.CurrentUserId != id)
            {
                return this.Forbid();
            }

            var user = this.userService.GetById(id);

            if (user == null)
            {
                return this.NotFound();
            }

            user.Name = model.Name;
            user.JobPlace = model.JobPlace;
            user.JobAddress = model.JobAddress;
            user.LocationId = model.Location?.Id;
            user.Email = model.Email;
            user.UserType = model.UserType;
            user.PhoneNumber = model.PhoneNumber;

            try
            {
                await this.userService.UpdateAsync(user);

                return this.Ok();
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }
        }
    }
}