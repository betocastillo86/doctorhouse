using System.Text.Json.Serialization;
using Beto.Core.Web.Api;
using DoctorHouse.Data;
using Newtonsoft.Json.Converters;

namespace DoctorHouse.Api.Models
{
    public class RequestFilterModel : BaseFilterModel
    {
        public RequestFilterModel()
        {
            this.MaxPageSize = 20;
        }

        public int? UserOwnerId { get; set; }

        public int? UserRequesterId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType? Status { get; set; }
    }
}