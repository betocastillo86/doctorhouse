using DoctorHouse.Data.Enums;

namespace DoctorHouse.Api.Models
{
    public class NewUserModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public UserType UserType { get; set; }

        public string Password { get; set; }

        public int LocationId { get; set; }
    }
}