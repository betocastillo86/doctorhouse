using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Extensions;
using DoctorHouse.Api.Models;
using DoctorHouse.Api.Models.Requests;
using DoctorHouse.Business.Security;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Authorize]
    [Route("api/v1/requests")]
    public class RequestsController : BaseApiController
    {
        private readonly IRequestService requestService;
        private readonly IWorkContext context;
        private readonly IMapper mapper;

        public RequestsController(
            IMessageExceptionFinder messageExceptionFinder,
            IRequestService requestService,
            IWorkContext context,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.requestService = requestService;
            this.mapper = mapper;
            this.context = context;
        }

        [ProducesResponseType(typeof(RequesterListRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpGet("get-all-by-requesterid")]
        public ActionResult GetAllRequestsByRequesterId([FromQuery] RequestFilterModel filter)
        {
            var response = this.requestService.GetAllByRequesterId(
                requesterId: this.context.CurrentUserId,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<Request>, IList<RequesterRequestModel>>(response.Requests);
            var result = new RequesterListRequestResponseModel() {
                Success = true,
                Requests = models
            };
            return this.Ok(result);
        }

        [ProducesResponseType(typeof(OwnerListRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpGet("get-all-by-ownerid")]
        public ActionResult GetAllRequestsByOwnerId([FromQuery] RequestFilterModel filter)
        {
            var response = this.requestService.GetAllByOwnerId(
                ownerId: this.context.CurrentUserId,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<Request>, IList<OwnerRequestModel>>(response.Requests);
            var result = new OwnerListRequestResponseModel() {
                Success = true,
                Requests = models
            };
            return this.Ok(result);
        }
        
        // [ProducesResponseType(typeof(SaveRequestResponseModel), (int)HttpStatusCode.OK)]
        // [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // [Produces("application/json")]
        // [HttpGet]
        // [Route("{id:int}", Name = "GetRequestById")]
        // public IActionResult Get(int id)
        // {
        //     var response = this.requestService.GetById(id);

        //     if (response.Request == null) {
        //         return this.NotFound();
        //     }

        //     var model = this.mapper.Map<Request, ShowRequestModel>(response.Request);
        //     var result = new SaveRequestResponseModel() {
        //         Success = true,
        //         Request = model
        //     };
        //     return this.Ok(result);
        // }
        
        [ProducesResponseType(typeof(SaveRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> PostAsync([FromBody] SaveRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var entity = this.mapper.Map<SaveRequestModel, Request>(model);
            var response = await this.requestService.InsertAsync(entity);

            if (!response.Success)
                return BadRequest(response.ErrorMessage);

            var requestModel = this.mapper.Map<Request, SaveRequestModel>(response.Request);
            var result = new SaveRequestResponseModel() {
                Success = true,
                Request = requestModel
            };

            return Ok(result);
        }

        [ProducesResponseType(typeof(SaveRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpPut]
        [RequiredModel]
        public async Task<IActionResult> Put(int id, [FromBody] SaveRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var entity = this.mapper.Map<SaveRequestModel, Request>(model);
            var response = await this.requestService.UpdateAsync(entity);

            if (!response.Success)
                return BadRequest(response.ErrorMessage);

            var requestModel = this.mapper.Map<Request, SaveRequestModel>(response.Request);
            var result = new SaveRequestResponseModel() {
                Success = true,
                Request = requestModel
            };

            return Ok(result);
        }

        [RequiredModel]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await this.requestService.DeleteAsync(id);
            
            if (!response.Success)
                return BadRequest(response.ErrorMessage);

            var requestModel = this.mapper.Map<Request, SaveRequestModel>(response.Request);
            var result = new SaveRequestResponseModel() {
                Success = true,
                Request = requestModel
            };
            return Ok(result);
        }
    }
}