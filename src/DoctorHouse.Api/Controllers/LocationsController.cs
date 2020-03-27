using System.Collections.Generic;
using AutoMapper;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using DoctorHouse.Api.Models;
using DoctorHouse.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/locations")]
    public class LocationsController : BaseApiController
    {
        private readonly ILocationService locationService;

        private readonly IMapper mapper;

        public LocationsController(
            IMessageExceptionFinder messageExceptionFinder,
            ILocationService locationService,
            IMapper mapper) : base(messageExceptionFinder)
        {
            this.locationService = locationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]LocationFilterModel filter)
        {
            var locations = this.locationService.GetAll(
                parentLocationId: filter.ParentLocationId,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = this.mapper.Map<IList<LocationModel>>(locations);

            return this.Ok(models, locations.HasNextPage, locations.TotalCount);
        }
    }
}