using DoctorHouse.Api.Models.Communication;

namespace DoctorHouse.Api.Models.Requests
{
    public class SaveRequestResponseModel : BaseResponseModel
    {
        public SaveRequestModel Request { get; set; }
    }
}