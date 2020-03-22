using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Place> placeRepository;

        public PlaceService(IRepository<Place> placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        public IPagedList<Place> GetAll(
            int? userId = null, 
            byte? guestsAllowed = null, 
            int? locationId = null,
            bool? food = null, 
            bool? kitchen = null, 
            bool? parking = null, 
            bool? onlyActive = null, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.placeRepository.TableNoTracking
                .Where(c => !c.Deleted);

            if (userId.HasValue)
            {
                query = query.Where(c => c.UserId == userId);
            }

            if (guestsAllowed.HasValue)
            {
                query = query.Where(c => c.GuestAllowed <= guestsAllowed);
            }

            if (locationId.HasValue)
            {
                query = query.Where(c => c.LocationId == locationId);
            }

            if (food.HasValue)
            {
                query = query.Where(c => c.Food == food);
            }

            if (kitchen.HasValue)
            {
                query = query.Where(c => c.Kitchen == kitchen);
            }

            if (parking.HasValue)
            {
                query = query.Where(c => c.Parking == parking);
            }

            if (onlyActive.HasValue)
            {
                query = query.Where(c => c.Active == onlyActive);
            }

            return new PagedList<Place>(query, page, pageSize);
        }

        public Place GetById(int id)
        {
            return this.placeRepository.TableNoTracking.FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        public Task InsertAsync(Place place)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Place place)
        {
            throw new NotImplementedException();
        }
    }
}