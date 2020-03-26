using System;
using System.Linq;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public IPagedList<Location> GetAll(int? parentLocationId = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.locationRepository.TableNoTracking
                .Where(c => !c.Deleted);

            if (parentLocationId.HasValue)
            {
                query = query.Where(c => c.ParentLocationId == parentLocationId);
            }

            return new PagedList<Location>(query, page, pageSize);
        }
    }
}