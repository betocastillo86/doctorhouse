using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorHouse.Data
{
    public class Place
    {
        public int Id { get; set; }

        public long Latitude { get; set; }

        public long Logitude { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public byte GuestAllowed { get; set; }

        public bool Bathroom { get; set; }

        public bool Food { get; set; }

        public bool Kitchen { get; set; }

        public bool Parking { get; set; }

        public int UserId { get; set; }

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableTo { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Internet { get; set; }

        public bool EntireHouse { get; set; }

        public virtual User User { get; set; }
    }
}
