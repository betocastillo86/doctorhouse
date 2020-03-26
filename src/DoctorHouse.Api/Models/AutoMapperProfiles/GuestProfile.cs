using AutoMapper;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Models.AutoMapperProfiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<Guest, GuestModel>()
                .ReverseMap();
        }
    }
}