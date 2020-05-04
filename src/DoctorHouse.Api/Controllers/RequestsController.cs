﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Beto.Core.Exceptions;
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
    [Authorize]
    [Route("api/v1/requests")]
    public class RequestsController : BaseApiController
    {
        private readonly IRequestService requestService;

        private readonly IWorkContext workContext;

        private readonly IMapper mapper;

        public RequestsController(
            IMessageExceptionFinder messageExceptionFinder,
            IRequestService requestService,
            IWorkContext context,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.requestService = requestService;
            this.mapper = mapper;
            this.workContext = context;
        }

        [Produces("application/json")]
        [HttpGet("get-all-by-requesterid")]
        public IActionResult GetAllRequestsByRequesterId([FromQuery] RequestFilterModel filter)
        {
            if (filter.UserId != this.workContext.CurrentUserId)
            {
                return this.Forbid();
            }

            var requests = this.requestService.GetAllByRequesterId(
                requesterId: filter.UserId.Value,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<RequestModel>>(requests);

            return this.Ok(models, requests.HasNextPage, requests.TotalCount);
        }

        [Produces("application/json")]
        [HttpGet("get-all-by-ownerid")]
        public IActionResult GetAllRequestsByOwnerId([FromQuery] RequestFilterModel filter)
        {
            if (filter.UserId != this.workContext.CurrentUserId)
            {
                return this.Forbid();
            }

            var requests = this.requestService.GetAllByOwnerId(
                ownerId: filter.UserId.Value,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<RequestModel>>(requests);

            return this.Ok(models, requests.HasNextPage, requests.TotalCount);
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public IActionResult GetRequestById(int id)
        {
            var request = this.requestService.GetById(id);

            if (request == null)
            {
                return this.NotFound();
            }

            if (request.UserRequesterId != this.workContext.CurrentUserId && request.Place.UserId != this.workContext.CurrentUserId)
            {
                return this.Forbid();
            }

            var model = this.mapper.Map<RequestModel>(request);

            return this.Ok(model);
        }

        [Produces("application/json")]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> PostAsync([FromBody] SaveRequestModel model)
        {
            var request = new Request
            {
                GuestType = model.GuestTypeId.Value,
                PlaceId = model.PlaceId,
                UserRequesterId = this.workContext.CurrentUserId,
                StartDate = model.StartDate.Value,
                EndDate = model.EndDate.Value,
                Description = model.Description
            };

            try
            {
                await this.requestService.InsertAsync(request);
                var requestModel = this.mapper.Map<Request, RequestModel>(this.requestService.GetById(request.Id));

                return this.CreatedAtAction(nameof(GetRequestById), requestModel, requestModel);
            }
            catch (DoctorHouseException ex)
            {
                return this.BadRequest(ex);
            }
        }

        [Produces("application/json")]
        [HttpPut("{id:int}")]
        [RequiredModel]
        public async Task<IActionResult> Put(int id, [FromBody] SaveRequestModel model)
        {
            var request = this.requestService.GetById(id);

            if (request == null)
            {
                return this.NotFound();
            }

            if (request.UserRequesterId != this.workContext.CurrentUserId)
            {
                return this.Forbid();
            }

            request.Description = model.Description;
            request.GuestType = model.GuestTypeId.Value;
            request.StartDate = model.StartDate.Value;
            request.EndDate = model.EndDate.Value;

            try
            {
                await this.requestService.UpdateAsync(id, request);

                return this.Ok();
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await this.requestService.DeleteAsync(id);

                return this.Ok();
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }
        }
    }
}