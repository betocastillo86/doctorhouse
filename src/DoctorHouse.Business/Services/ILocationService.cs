using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface ILocationService
    {
        IPagedList<Location> GetAll(
            int? parentLocationId = null,
            int page = 0,
            int pageSize = int.MaxValue);
    }
}