using System.Collections.Generic;
using DoctorHouse.Api.Models.Communication;

namespace DoctorHouse.Api.Models.Requests
{
    public class ListRequestResponseModel : BaseResponseModel
    {
        public IList<RequestModel> Requests { get; set; }
    }
}