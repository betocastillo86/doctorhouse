using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using DoctorHouse.Api.Models;
using DoctorHouse.Business.Security;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/auth/current")]
    public class AuthController : BaseApiController
    {
        private readonly IWorkContext workContext;

        private readonly IMapper mapper;

        public AuthController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var model = this.mapper.Map<User, UserModel>(this.workContext.CurrentUser);
            return this.Ok(model);
        }
    }
}