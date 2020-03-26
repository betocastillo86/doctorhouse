using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.EventPublisher;
using DoctorHouse.Business.Services.Communication;
using DoctorHouse.Data;
using Microsoft.EntityFrameworkCore;

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

        public ListRequestResponse GetAllByRequesterId
        (
            int requesterId, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var allRequests = GetAllRequests();
            var query = allRequests.Where(w => w.UserRequesterId == requesterId);
            
            var pagedList = new PagedList<Request>(query, page, pageSize);

            return new ListRequestResponse()
            {
                Success = true,
                Requests = pagedList
            };
        }

        public ListRequestResponse GetAllByOwnerId
        (
            int ownerId, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var allRequests = GetAllRequests();
            var query = allRequests.Where(w => w.UserOwnerId == ownerId);

            var pagedList = new PagedList<Request>(query, page, pageSize);

            return new ListRequestResponse()
            {
                Success = true,
                Requests = pagedList
            };
        }

        public RequestResponse GetById(int id)
        {
            var request = GetRequest(id);

            return new RequestResponse()
            {
                Success = true,
                Request = request
            };
        }

        public async Task<RequestResponse> InsertAsync(Request request)
        {
            try
            {
                await this.requestRepository.InsertAsync(request);
                return new RequestResponse()
                {
                    Success = true,
                    Request = request
                };
            }
            catch (DbUpdateException e)
            {
                return new RequestResponse() { Success = false, ErrorMessage = "There was an error creating the request." };
            }
        }

        public async Task<RequestResponse> UpdateAsync(int id, Request request)
        {
            var dbRequest = GetRequest(id);
            if(dbRequest == null){
                return new RequestResponse() { Success = false, ErrorMessage = "Not Found" };
            }
            
            dbRequest.Description = request.Description;
            dbRequest.StatusId = request.StatusId;
            try
            {
                var success = (await this.requestRepository.UpdateAsync(dbRequest)) == 1;
                if(success) {
                    return new RequestResponse() { Success = true, Request = dbRequest };
                } else {
                    return new RequestResponse() { Success = false, ErrorMessage = "No rows affected" };
                }
            }
            catch (DbUpdateException e)
            {
                return new RequestResponse() { Success = false, ErrorMessage = "There was an error saving the request." };
            }
        }

        public async Task<RequestResponse> DeleteAsync(int id)
        {
            var dbRequest = GetRequest(id);
            if(dbRequest == null){
                return new RequestResponse() { Success = false, ErrorMessage = "Not Found" };
            }

            dbRequest.Deleted = true;
            try
            {
                var success = (await this.requestRepository.UpdateAsync(dbRequest)) == 1;
                if(success) {
                    return new RequestResponse() { Success = true, Request = dbRequest };
                } else {
                    return new RequestResponse() { Success = false, ErrorMessage = "No rows affected" };
                }
            }
            catch (DbUpdateException e)
            {
                return new RequestResponse() { Success = false, ErrorMessage = "There was an error deleting the request." };
            }
        }

        private IQueryable<Request> GetAllRequests()
        {
            return this.requestRepository.TableNoTracking.Where(w => w.Deleted == false).Include(p => p.Place);
        }

        private Request GetRequest(int id)
        {
            return this.requestRepository.TableNoTracking.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}