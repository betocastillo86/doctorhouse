using DoctorHouse.Data;

namespace DoctorHouse.Business.Services.Communication
{
    public class RequestResponse : BaseResponse
    {
        public Request Request { get; set; }
    }
}