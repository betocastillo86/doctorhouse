using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IRequestService
    {
        IPagedList<Request> GetAll(
            int? requesterId = null,
            int? ownerId = null,
            int page = 0,
            int pageSize = int.MaxValue);

        IPagedList<Request> GetAllByRequesterId(
            int requesterId,
            int page = 0,
            int pageSize = int.MaxValue);

        IPagedList<Request> GetAllByOwnerId(
            int ownerId,
            int page = 0,
            int pageSize = int.MaxValue);

        Request GetById (int id);

        Task InsertAsync(Request request);

        Task UpdateAsync(int id, Request request);

        Task DeleteAsync(int id);
    }
}
