using System;

namespace DoctorHouse.Api.Models.Requests
{
    public class OwnerRequestModel
    {
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string PLaceOwnerName { get; set; }

        public string Status { get; set; }
    }
}