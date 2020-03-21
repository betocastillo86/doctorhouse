using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IWebCacheService
    {
        User GetUserById(int id);
    }
}