using System.Collections.Generic;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/requests/{requestId}/guests")]
    public class GuestsController : BaseApiController
    {
        public GuestsController(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int requestId)
        {
            var models = new List<GuestModel>()
            {
                new GuestModel { }
            };

            return this.Ok(models, true, 10);
        }

        [Authorize]
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public IActionResult Put(int id, [FromBody] GuestModel model)
        {
            return this.Ok();
        }
    }
}