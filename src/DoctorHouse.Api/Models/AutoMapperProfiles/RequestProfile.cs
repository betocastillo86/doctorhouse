using AutoMapper;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Models.AutoMapperProfiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<Request, RequestModel>()
                .ReverseMap();
            CreateMap<SaveRequestModel, Request>()
                .ReverseMap();
        }
    }
}