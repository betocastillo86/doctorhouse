using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.EventPublisher;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> requestRepository;

        public RequestService(
            IRepository<Request> _requestRepository)
        {
            this.requestRepository = _requestRepository;
        }

        public Task Delete(Request request)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<Request> GetAll
        (
            int? userId = null, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.requestRepository.TableNoTracking;

            return new PagedList<Request>(query, page, pageSize);
        }

        public Request GetById(int id)
        {
            return this.requestRepository.TableNoTracking.FirstOrDefault(c => c.Id == id);
        }

        public Task InsertAsync(Request request)
        {
            return this.requestRepository.InsertAsync(request);
        }

        public Task UpdateAsync(Request request)
        {
            var dbRequest = GetById(request.Id);
            dbRequest.Description = request.Description;
            if(dbRequest!= null){
                return this.requestRepository.UpdateAsync(request);
            }

            throw new Exception("Not Found");
            
        }

        // public Place GetById(int id)
        // {
        //     return this.placeRepository.TableNoTracking.FirstOrDefault(c => c.Id == id && !c.Deleted);
        // }

        // public Task InsertAsync(Place place)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task UpdateAsync(Place place)
        // {
        //     throw new NotImplementedException();
        // }
    }
}