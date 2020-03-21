using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorHouse.Api.Models
{
    public class GuestModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string JobPlace { get; set; }

        public string JobAddress { get; set; }
    }
}
