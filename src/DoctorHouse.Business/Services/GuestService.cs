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
    public class GuestService : IGuestService
    {
        private readonly IRepository<Guest> guestRepository;

        public GuestService(IRepository<Guest> guestRepository)
        {
            this.guestRepository = guestRepository;
        }

        public IPagedList<Guest> GetAll(
            int? id = null, 
            string name = null, 
            string phone = null, 
            string jobPlace = null, 
            string jobAddress = null, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.guestRepository.TableNoTracking;

            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id);
            }

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name == name);
            }
            
            if (!String.IsNullOrEmpty(phone))
            {
                query = query.Where(c => c.Phone == phone);
            }

            if (!String.IsNullOrEmpty(jobPlace))
            {
                query = query.Where(c => c.JobPlace == jobPlace);
            }

            if (!String.IsNullOrEmpty(jobAddress))
            {
                query = query.Where(c => c.JobAddress == jobAddress);
            }

            return new PagedList<Guest>(query, page, pageSize);
        }

        public async Task UpdateAsync(Guest guest)
        {
            try
            {
                await this.guestRepository.UpdateAsync(guest);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var sqlex = (SqlException)e.InnerException;

                    if (sqlex.Number == 547)
                    {
                        var target = e.ToString();

                        if (sqlex.Message.IndexOf("FK_Guests_Requests_RequestId") != -1)
                        {
                            target = "Requests";
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
    }
}
