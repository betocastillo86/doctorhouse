using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Models.Requests;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/requests")]
    public class RequestsController : BaseApiController
    {
        private readonly IRequestService requestService;

        private readonly IMapper mapper;

        public RequestsController(IMessageExceptionFinder messageExceptionFinder,
            IRequestService requestService,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.requestService = requestService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Get([FromQuery] RequestFilterModel filter)
        {
            var requests = this.requestService.GetAll(
                page: filter.Page,
                pageSize: filter.PageSize);

            var results = this.mapper.Map<IList<RequestModel>>(requests);

            return this.Ok(results);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "GetRequestById")]
        public IActionResult Get(int id)
        {
            var request = this.requestService.GetById(id);

            if (request == null)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<RequestModel>(request);

            return this.Ok(model);
        }

        [Authorize]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] NewRequestModel model)
        {
            var entity = this.mapper.Map<NewRequestModel, Request>(model);

            await this.requestService.InsertAsync(entity);

            return this.Ok(model);
        }

        [Authorize]
        [HttpPut]
        [RequiredModel]
        public IActionResult Put(int id, [FromBody] RequestModel model)
        {
            return this.Ok();
        }
    }
}