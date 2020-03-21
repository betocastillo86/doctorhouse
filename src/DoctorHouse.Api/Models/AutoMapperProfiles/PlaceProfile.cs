using AutoMapper;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Models.AutoMapperProfiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceModel>()
                .ReverseMap();
        }
    }
}