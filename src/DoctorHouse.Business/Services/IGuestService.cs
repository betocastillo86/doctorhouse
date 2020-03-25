using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IGuestService
    {
        IPagedList<Guest> GetAll(
            int requestId,
            int page = 0,
            int pageSize = int.MaxValue);

        Guest GetById(int id);

        Task UpdateAsync(Guest guest);
    }
}