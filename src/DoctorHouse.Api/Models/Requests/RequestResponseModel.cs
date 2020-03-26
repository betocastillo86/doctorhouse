using System.Collections.Generic;
using DoctorHouse.Api.Models.Communication;

namespace DoctorHouse.Api.Models.Requests
{
    public class RequestResponseModel : BaseResponseModel
    {
        public RequestModel Request { get; set; }
    }
}