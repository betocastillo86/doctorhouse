using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.ActionFilters;
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

        [ProducesResponseType(typeof(ListRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpGet("get-all-by-requesterid")]
        public ActionResult GetAllRequestsByRequesterId([FromQuery] RequestFilterModel filter)
        {
            if(!filter.UserRequesterId.HasValue) {
                return BadRequest();
            }
            var response = this.requestService.GetAllByRequesterId(
                requesterId: filter.UserRequesterId.Value, //this.context.CurrentUserId,
                page: filter.Page,
                pageSize: filter.PageSize);

            if(!response.Success)
                return BadRequest(response.ErrorMessage);

            var models = this.mapper.Map<IList<Request>, IList<RequestModel>>(response.Requests);
            var result = new ListRequestResponseModel() {
                Success = true,
                Requests = models
            };
            return this.Ok(result);
        }

        [ProducesResponseType(typeof(ListRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpGet("get-all-by-ownerid")]
        public ActionResult GetAllRequestsByOwnerId([FromQuery] RequestFilterModel filter)
        {
            if(!filter.UserOwnerId.HasValue) {
                return BadRequest();
            }

            var response = this.requestService.GetAllByOwnerId(
                ownerId: filter.UserOwnerId.Value,//this.context.CurrentUserId,
                page: filter.Page,
                pageSize: filter.PageSize);

            if(!response.Success)
                return BadRequest(response.ErrorMessage);

            var models = this.mapper.Map<IList<Request>, IList<RequestModel>>(response.Requests);
            var result = new ListRequestResponseModel() {
                Success = true,
                Requests = models
            };
            return this.Ok(result);
        }
        
        [ProducesResponseType(typeof(SaveRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpGet("id")]
        public IActionResult GetRequestById(int id)
        {
            var response = this.requestService.GetById(id);

            if (response.Request == null) {
                return this.NotFound();
            }

            var model = this.mapper.Map<Request, RequestModel>(response.Request);
            var result = new RequestResponseModel() {
                Success = true,
                Request = model
            };
            return this.Ok(result);
        }
        
        [ProducesResponseType(typeof(SaveRequestResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        [RequiredModel]
        [ServiceFilter(typeof(SaveRequestFilter))]
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
            var response = await this.requestService.UpdateAsync(id, entity);

            if (!response.Success)
                return BadRequest(response.ErrorMessage);

            var requestModel = this.mapper.Map<Request, SaveRequestModel>(response.Request);
            var result = new SaveRequestResponseModel() {
                Success = true,
                Request = requestModel
            };

            return Ok(result);
        }

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