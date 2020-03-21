using System;
using System.Collections.Generic;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using DoctorHouse.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/places")]
    public class PlacesController : BaseApiController
    {
        public PlacesController(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] PlaceFilterModel model)
        {
            var models = new List<PlaceModel>()
            {
                new PlaceModel
                {
                    Active = true,
                    Address = "address",
                    AvailableFrom = DateTime.UtcNow,
                    AvailableTo = DateTime.UtcNow,
                    Bathroom = true,
                    CreationDate = DateTime.UtcNow,
                    Description = "description",
                    EntireHouse = false,
                    Food = false,
                    GuestAllowed = 2,
                    Internet = true,
                    Kitchen = true,
                    Latitude = 1,
                    Logitude = 1,
                    Parking = false,
                    User = new UserModel{ Name = "the user" },
                    Phone = "423423"
                }
            };

            return this.Ok(models, true, 10);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "GetPlaceById")]
        public IActionResult Get(int id)
        {
            var models = new List<PlaceModel>()
            {
                new PlaceModel
                {
                    Active = true,
                    Address = "address",
                    AvailableFrom = DateTime.UtcNow,
                    AvailableTo = DateTime.UtcNow,
                    Bathroom = true,
                    CreationDate = DateTime.UtcNow,
                    Description = "description",
                    EntireHouse = false,
                    Food = false,
                    GuestAllowed = 2,
                    Internet = true,
                    Kitchen = true,
                    Latitude = 1,
                    Logitude = 1,
                    Parking = false,
                    User = new UserModel{ Name = "the user" },
                    Phone = "423423"
                }
            };

            return this.Ok(models, false, 10);
        }

        [Authorize]
        [HttpPost]
        [RequiredModel]
        [Route("{id:int}")]
        public IActionResult Post([FromBody] PlaceModel model)
        {
            return this.Created("GetPlaceById", 1);
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