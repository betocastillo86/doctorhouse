using Beto.Core.Data;

namespace DoctorHouse.Data
{
    public class Guest : IEntity
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string JobPlace { get; set; }

        public string JobAddress { get; set; }

        public virtual Request Request { get; set; }
    }
}