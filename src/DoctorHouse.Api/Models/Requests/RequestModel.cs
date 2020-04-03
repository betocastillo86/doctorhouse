using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DoctorHouse.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

namespace DoctorHouse.Api.Models
{
    public class RequestModel
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int UserOwnerId { get; set; }

        public int UserRequesterId { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GuestType GuestType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PlaceModel Place { get; set; }

        public UserModel UserOwner { get; set; }

        public UserModel UserRequester { get; set; }

        public IList<GuestModel> Guests { get; set; }
    }
}