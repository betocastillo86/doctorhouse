namespace DoctorHouse.Api.Models.Communication
{
    public abstract class BaseResponseModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}