using System;
using System.Collections.Generic;
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
    [Route("api/v1/places")]
    public class PlacesController : BaseApiController
    {
        private readonly IPlaceService placeService;

        private readonly IMapper mapper;

        private readonly IWorkContext workContext;

        public PlacesController(
            IMessageExceptionFinder messageExceptionFinder,
            IPlaceService placeService,
            IMapper mapper,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.placeService = placeService;
            this.mapper = mapper;
            this.workContext = workContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] PlaceFilterModel filter)
        {
            var places = this.placeService.GetAll(
                guestsAllowed: filter.CountGuests,
                locationId: filter.LocationId,
                onlyActive: true,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<PlaceModel>>(places);

            return this.Ok(models, places.HasNextPage, places.TotalCount);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "GetPlaceById")]
        public IActionResult Get(int id)
        {
            var place = this.placeService.GetById(id);

            if (place == null)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<PlaceModel>(place);

            return this.Ok(model);
        }

        [Authorize]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] PlaceModel model)
        {
            var place = new Place()
            {
                Latitude = model.Latitude.Value,
                Longitude = model.Longitude.Value,
                Address = model.Address,
                Phone = model.Phone,
                Description = model.Description,
                GuestsAllowed = model.GuestAllowed.Value,
                Bathroom = model.Bathroom,
                Food = model.Food,
                Kitchen = model.Kitchen,
                Parking = model.Parking,
                Active = true,
                Internet = model.Internet,
                EntireHouse = model.EntireHouse,
                UserId = this.workContext.CurrentUserId,
                LocationId = model.Location.Id
            };

            try
            {
                await this.placeService.InsertAsync(place);
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }

            return this.Created("GetPlaceById", place.Id);
        }

        [Authorize]
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public IActionResult Put(int id, [FromBody] PlaceModel model)
        {
            return this.Ok();
        }
    }
}