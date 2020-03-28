using System;
using System.ComponentModel.DataAnnotations.Schema;
using Beto.Core.Data;

namespace DoctorHouse.Data
{
    public class Request : IEntity
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int UserRequesterId { get; set; }

        public string Description { get; set; }

        public byte GuestTypeId { get; set; }

        public byte StatusId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }

        public Place Place { get; set; }

        public virtual User UserRequester { get; set; }

        public bool Deleted { get; set; }


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