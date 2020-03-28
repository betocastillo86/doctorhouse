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
            int requestId,
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.guestRepository.TableNoTracking;

            query = query.Where(c => c.RequestId == requestId);

            return new PagedList<Guest>(query, page, pageSize);
        }

        public Guest GetById(int id)
        {
            return this.guestRepository.TableNoTracking
                .Where(p => p.Id == id).FirstOrDefault();
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
