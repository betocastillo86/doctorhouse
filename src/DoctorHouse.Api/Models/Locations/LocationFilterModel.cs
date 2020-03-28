using System.Text.Json.Serialization;
using Beto.Core.Web.Api;

namespace DoctorHouse.Api.Models
{
    public class LocationFilterModel : BaseFilterModel
    {
        [JsonIgnore]
        public int? ParentLocationId { get; set; }
    }
}