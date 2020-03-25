using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IGuestService
    {
        IPagedList<Guest> GetAll(
            int? id = null,
            string name = null,
            string phone = null,
            string jobPlace = null,
            string jobAddress = null,
            int page = 0,
            int pageSize = int.MaxValue);

        Task UpdateAsync(Guest guest);
    }
}