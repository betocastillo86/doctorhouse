using System.Collections.Generic;
using DoctorHouse.Api.Models.Communication;

namespace DoctorHouse.Api.Models.Requests
{
    public class RequesterListRequestResponseModel : BaseResponseModel
    {
        public IList<RequesterRequestModel> Requests { get; set; }
    }
}