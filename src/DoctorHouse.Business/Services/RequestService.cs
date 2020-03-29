using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.EventPublisher;
using DoctorHouse.Business.Exceptions;
using DoctorHouse.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoctorHouse.Business.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> requestRepository;

        private readonly IPublisher publisher;

        public RequestService(
            IRepository<Request> _requestRepository,
           IPublisher publisher)
        {
            this.publisher = publisher;
            this.requestRepository = _requestRepository;
        }

        public IPagedList<Request> GetAllByRequesterId
        (
            int requesterId, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var allRequests = GetAllRequests();
            var query = allRequests.Where(w => w.UserRequesterId == requesterId);
            
            var pagedList = new PagedList<Request>(query, page, pageSize);

            return pagedList;
        }

        public IPagedList<Request> GetAllByOwnerId
        (
            int ownerId, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var allRequests = GetAllRequests();
            var query = allRequests.Where(w => w.Place.UserId == ownerId);

            var pagedList = new PagedList<Request>(query, page, pageSize);

            return pagedList;
        }

        public Request GetById(int id)
        {
            var request = GetRequest(id);

            return request;
        }

        public async Task InsertAsync(Request request)
        {
            try
            {
                request.Status = StatusType.New;
                await this.requestRepository.InsertAsync(request);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var sqlex = (SqlException)e.InnerException;

                    if (sqlex.Number == 547)
                    {
                        var target = e.ToString();

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

            await this.publisher.EntityInserted(request);
        }

        public async Task UpdateAsync(int id, Request request)
        {
            try
            {
                await this.requestRepository.UpdateAsync(request);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var sqlex = (SqlException)e.InnerException;

                    if (sqlex.Number == 547)
                    {
                        var target = e.ToString();

                        if (sqlex.Message.IndexOf("FK_Users_Locations") != -1)
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

            await this.publisher.EntityUpdated(request);
        }

        public async Task DeleteAsync(int id)
        {
            var dbRequest = GetRequest(id);
            if(dbRequest == null){
                throw new DoctorHouseException(DoctorHouseExceptionCode.BadArgument);
            }
            
            dbRequest.Deleted = true;
            try
            {
                await this.requestRepository.UpdateAsync(dbRequest);
            }
            catch (DbUpdateException e)
            {
                throw new DoctorHouseException(e.ToString());
            }

            await this.publisher.EntityDeleted(dbRequest);

        }

        private IQueryable<Request> GetAllRequests()
        {
            return this.requestRepository.TableNoTracking.Where(w => w.Deleted == false).Include(p => p.Place);
        }

        private Request GetRequest(int id)
        {
            return this.requestRepository.TableNoTracking
                .Include(c => c.Place)
                .Where(c => c.Id == id)
                .FirstOrDefault();
        }

        public IPagedList<Request> GetAll(
            int? requesterId = null, 
            int? ownerId = null, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.requestRepository.TableNoTracking
                .Where(w => !w.Deleted)
                .Include(p => p.Place)
                .Include(c => c.Place.User)
                .AsQueryable();

            if (requesterId.HasValue)
            {
                query = query.Where(c => c.UserRequesterId == requesterId);
            }

            if (ownerId.HasValue)
            {
                query = query.Where(c => c.Place.UserId == ownerId);
            }

            return new PagedList<Request>(query, page, pageSize); ;
        }
    }
}