using System;

namespace DoctorHouse.Api.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public byte? GuestsAllowed { get; set; }

        public bool Bathroom { get; set; }

        public bool Food { get; set; }

        public bool Kitchen { get; set; }

        public bool Parking { get; set; }

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableTo { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Internet { get; set; }

        public bool EntireHouse { get; set; }

        public UserModel User { get; set; }

        public LocationModel Location { get; set; }
    }
}