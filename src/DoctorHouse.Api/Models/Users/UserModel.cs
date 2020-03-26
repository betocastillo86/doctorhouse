using System;

namespace DoctorHouse.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public short UserType { get; set; }

        public string JobPlace { get; set; }

        public string JobAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public LocationModel Location { get; set; }
    }
}