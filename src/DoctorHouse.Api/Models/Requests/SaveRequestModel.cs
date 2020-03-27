using System;

namespace DoctorHouse.Api.Models.Requests
{
    public class SaveRequestModel
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }

        public int UserOwnerId { get; set; }
        public string Description { get; set; }

        public int UserRequesterId { get; set; }

        public byte GuestTypeId { get; set; }

        public byte StatusId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}