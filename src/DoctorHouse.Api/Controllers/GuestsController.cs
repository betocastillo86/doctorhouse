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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHouse.Api.Controllers
{
    [Route("api/v1/requests/{requestId}/guests")]
    public class GuestsController : BaseApiController
    {
        private readonly IGuestService guestService;

        private readonly IMapper mapper;

        private readonly IWorkContext workContext;
        public GuestsController(
            IMessageExceptionFinder messageExceptionFinder,
            IGuestService guestService,
            IMapper mapper,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.guestService = guestService;
            this.mapper = mapper;
            this.workContext = workContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int requestId)
        {
            var guests = guestService.GetAll(requestId);

            var models = this.mapper.Map<IList<GuestModel>>(guests);

            return this.Ok(models, false, models.Count);
        }

        [Authorize]
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GuestModel model)
        {
            
            var guest = guestService.GetById(id);

            if (guest == null)
            {
                return this.NotFound();
            }

            guest.JobAddress = model.JobAddress;
            guest.JobPlace = model.JobPlace;
            guest.Name = model.Name;
            guest.Phone = model.Phone;

            try
            {
                await this.guestService.UpdateAsync(guest);
            }
            catch (DoctorHouseException e)
            {
                return this.BadRequest(e);
            }

            return this.Ok();
        }
    }
}