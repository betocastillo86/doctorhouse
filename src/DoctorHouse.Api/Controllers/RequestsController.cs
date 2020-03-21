using System.Collections.Generic;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/requests")]
    public class RequestsController : BaseApiController
    {
        public RequestsController(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] RequestFilterModel model)
        {
            var models = new List<RequestModel>()
            {
                new RequestModel
                {
                }
            };

            return this.Ok(models, true, 10);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "GetRequestById")]
        public IActionResult Get(int id)
        {
            var models = new List<RequestModel>()
            {
                new RequestModel
                {
                }
            };

            return this.Ok(models);
        }

        [Authorize]
        [HttpPost]
        [RequiredModel]
        [Route("{id:int}")]
        public IActionResult Post([FromBody] RequestModel model)
        {
            return this.Created("GetRequestById", 1);
        }

        [Authorize]
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public IActionResult Put(int id, [FromBody] RequestModel model)
        {
            return this.Ok();
        }
    }
}