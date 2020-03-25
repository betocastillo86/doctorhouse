using System.Collections.Generic;
using DoctorHouse.Api.Models.Communication;

namespace DoctorHouse.Api.Models.Requests
{
    public class OwnerListRequestResponseModel : BaseResponseModel
    {
        public IList<OwnerRequestModel> Requests { get; set; }
    }
}