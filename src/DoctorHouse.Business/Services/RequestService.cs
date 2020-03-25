using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.EventPublisher;
using DoctorHouse.Business.Services.Communication;
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

        public ListRequestResponse GetAllByRequesterId
        (
            int requesterId, 
            int page = 0, 
            int pageSize = int.MaxValue)
        {
            var query = this.requestRepository.TableNoTracking.Where(w => w.UserRequesterId == requesterId);

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
            var query = this.requestRepository.TableNoTracking.Where(w => w.UserOwnerId == ownerId);

            var pagedList = new PagedList<Request>(query, page, pageSize);

            return new ListRequestResponse()
            {
                Success = true,
                Requests = pagedList
            };
        }

        public RequestResponse GetById(int id)
        {
            var request = this.requestRepository.TableNoTracking.Where(c => c.Id == id).FirstOrDefault();

            return new RequestResponse()
            {
                Success = true,
                Request = request
            };
        }

        public async Task<RequestResponse> InsertAsync(Request request)
        {
            await this.requestRepository.InsertAsync(request);

            return new RequestResponse()
            {
                Success = true
            };
        }

        public async Task<RequestResponse> UpdateAsync(Request request)
        {
            var dbRequest = this.requestRepository.TableNoTracking.Where(c => c.Id == request.Id).FirstOrDefault();
            var result = false;
            if(dbRequest != null){
                dbRequest.Description = request.Description;
                result = (await this.requestRepository.UpdateAsync(request)) == 1;
            }

            return new RequestResponse()
            {
                Success = result,
                Request = dbRequest
            };
        }

        public async Task<RequestResponse> DeleteAsync(int id)
        {
            var dbRequest = this.requestRepository.TableNoTracking.Where(c => c.Id == id).FirstOrDefault();
            var result = false;
            if(dbRequest != null){
                result = (await this.requestRepository.DeleteAsync(dbRequest)) == 1;
            }

            return new RequestResponse()
            {
                Success = result,
                Request = dbRequest
            };
        }
    }
}