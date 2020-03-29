﻿using System;
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
    [Authorize]
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

        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PlaceModel model)
        {
            var place = this.placeService.GetById(id);

            if (place == null)
            {
                return this.NotFound();
            }

            if (place.UserId != this.workContext.CurrentUserId)
            {
                return this.Forbid();
            }

            place.Latitude = model.Latitude.Value;
            place.Longitude = model.Longitude.Value;
            place.Address = model.Address;
            place.Phone = model.Phone;
            place.Description = model.Description;
            place.GuestsAllowed = model.GuestAllowed.Value;
            place.Bathroom = model.Bathroom;
            place.Food = model.Food;
            place.Kitchen = model.Kitchen;
            place.Parking = model.Parking;
            place.Internet = model.Internet;
            place.EntireHouse = model.EntireHouse;
            place.LocationId = model.Location.Id;

            try
            {
                await this.placeService.UpdateAsync(place);
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }

            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var place = this.placeService.GetById(id);

            if (place == null)
            {
                return this.NotFound();
            }

            await this.placeService.DeleteAsync(place);

            return this.Ok();
        }
    }
}