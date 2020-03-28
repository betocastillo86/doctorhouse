using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Business.Exceptions;
using DoctorHouse.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                query = query.Where(c => c.GuestsAllowed <= guestsAllowed);
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
            return this.placeRepository.TableNoTracking
                .Include(c => c.User)
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        public async Task InsertAsync(Place place)
        {
            try
            {
                place.CreationDate = DateTime.UtcNow;
                await this.placeRepository.InsertAsync(place);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var sqlex = (SqlException)e.InnerException;

                    if (sqlex.Number == 547)
                    {
                        var target = e.ToString();

                        if (sqlex.Message.IndexOf("FK_Places_Locations") != -1)
                        {
                            target = "Locations";
                        }

                        throw new DoctorHouseException(target, DoctorHouseExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task UpdateAsync(Place place)
        {
            try
            {
                await this.placeRepository.UpdateAsync(place);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var sqlex = (SqlException)e.InnerException;

                    if (sqlex.Number == 547)
                    {
                        var target = e.ToString();

                        if (sqlex.Message.IndexOf("FK_Places_Locations") != -1)
                        {
                            target = "Locations";
                        }

                        throw new DoctorHouseException(target, DoctorHouseExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dbRequest = GetPlace(id);
            if(dbRequest == null){
                throw new DoctorHouseException(DoctorHouseExceptionCode.BadArgument);
            }
            
            dbRequest.Deleted = true;
            try
            {
                await this.placeRepository.UpdateAsync(dbRequest);
            }
            catch (DbUpdateException e)
            {
                throw new DoctorHouseException(e.ToString());
            }
        }

        private Place GetPlace(int id)
        {
            return this.placeRepository.TableNoTracking.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}