using System.Net.Http;

namespace DoctorHouse.Api.Tests.Models
{
    public class TypedResponseModel<T>
    {
        public T Content { get; set; }

        public HttpResponseMessage Response { get; set; }
    }
}