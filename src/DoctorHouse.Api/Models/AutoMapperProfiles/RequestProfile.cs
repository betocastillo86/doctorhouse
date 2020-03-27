using AutoMapper;
using DoctorHouse.Api.Models.Requests;
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
            CreateMap<RequestModel, Request>()
                .ReverseMap();
        }
    }
}