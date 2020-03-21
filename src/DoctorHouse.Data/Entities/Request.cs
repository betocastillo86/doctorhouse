using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorHouse.Data
{
    public class Request
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int UserOwnerId { get; set; }

        public int UserRequesterId { get; set; }

        public string Description { get; set; }

        public byte GuestTypeId { get; set; }

        public byte StatusId { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual Place Place { get; set; }

        public virtual User UserOwner { get; set; }

        public virtual User UserRequester { get; set; }

        [NotMapped]
        public StatusType Status
        {
            get
            {
                return (StatusType)this.StatusId;
            }

            set
            {
                this.StatusId = Convert.ToByte(value);
            }
        }

        [NotMapped]
        public GuestType GuestType
        {
            get
            {
                return (GuestType)this.GuestTypeId;
            }

            set
            {
                this.GuestTypeId = Convert.ToByte(value);
            }
        }
    }
}
