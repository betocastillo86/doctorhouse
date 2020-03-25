namespace DoctorHouse.Business.Services.Communication
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}