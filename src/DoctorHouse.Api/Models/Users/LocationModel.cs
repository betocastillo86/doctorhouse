namespace DoctorHouse.Api.Models
{
    public class LocationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LocationModel ParentLocation { get; set; }
    }
}