using System;
using System.Text.Json.Serialization;
using DoctorHouse.Data;
using Newtonsoft.Json.Converters;

namespace DoctorHouse.Api.Models
{
    public class SaveRequestModel
    {
        public int PlaceId { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GuestType? GuestTypeId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType? StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}