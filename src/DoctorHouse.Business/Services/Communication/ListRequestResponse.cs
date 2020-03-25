using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services.Communication
{
    public class ListRequestResponse : BaseResponse
    {
        public IPagedList<Request> Requests { get; set; }
        
    }
}