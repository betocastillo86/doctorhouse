using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Business.Services.Communication;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IRequestService
    {
        ListRequestResponse GetAllByRequesterId(
            int requesterId,
            int page = 0,
            int pageSize = int.MaxValue);
        ListRequestResponse GetAllByOwnerId(
            int ownerId,
            int page = 0,
            int pageSize = int.MaxValue);
        RequestResponse GetById (int id);

        Task<RequestResponse> InsertAsync(Request request);

        Task<RequestResponse> UpdateAsync(Request request);

        Task<RequestResponse> DeleteAsync(int id);
    }
}
