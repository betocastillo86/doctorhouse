using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IPlaceService
    {
        IPagedList<Place> GetAll(
            int? userId = null,
            byte? guestsAllowed = null,
            int? locationId = null,
            bool? food = null,
            bool? kitchen = null,
            bool? parking = null,
            bool? onlyActive = null,
            int page = 0,
            int pageSize = int.MaxValue);

        Place GetById(int id);

        Task UpdateAsync(Place place);

        Task InsertAsync(Place place);

        Task DeleteAsync(Place place);
    }
}