using System.Collections.Generic;
using Beto.Core.Data;

namespace DoctorHouse.Data
{
    public partial class Location : IEntity
    {
        public Location()
        {
            this.InverseParentLocation = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentLocationId { get; set; }

        public bool Deleted { get; set; }

        public virtual Location ParentLocation { get; set; }

        public virtual ICollection<Location> InverseParentLocation { get; set; }
    }
}