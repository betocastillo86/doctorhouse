using Beto.Core.Web.Api;

namespace DoctorHouse.Api.Models
{
    public class PlaceFilterModel : BaseFilterModel
    {
        public PlaceFilterModel()
        {
            this.MaxPageSize = 100;
        }

        public int LocationId { get; set; }

        public byte? CountGuests { get; set; }


    }
}