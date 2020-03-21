using System;
using System.Collections.Generic;

namespace DoctorHouse.Api.Models
{
    public class RequestModel
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int UserOwnerId { get; set; }

        public int UserRequesterId { get; set; }

        public string Description { get; set; }

        public byte GuestTypeId { get; set; }

        public byte StatusId { get; set; }

        public DateTime CreationDate { get; set; }

        public PlaceModel Place { get; set; }

        public UserModel UserOwner { get; set; }

        public UserModel UserRequester { get; set; }

        public IList<GuestModel> Guests { get; set; }
    }
}