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
            CreateMap<SaveRequestModel, Request>();
            CreateMap<OwnerRequestModel, Request>();
            CreateMap<RequesterRequestModel, Request>();
        }
    }
}