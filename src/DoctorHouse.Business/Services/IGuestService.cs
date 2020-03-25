using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IGuestService
    {
        IPagedList<Place> GetAll(
            int? id = null,
            string Name = null,
            string Phone = null,
            string JobPlace = null,
            string JobAddress = null,
            int page = 0,
            int pageSize = int.MaxValue);

        Task UpdateAsync(Guest guest);
    }
}