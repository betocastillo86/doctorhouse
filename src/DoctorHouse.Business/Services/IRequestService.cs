using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beto.Core.Data;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IRequestService
    {
        IPagedList<Request> GetAll(
            int? userId = null,
            int page = 0,
            int pageSize = int.MaxValue);
        Request GetById (int id);

        Task InsertAsync(Request request);

        Task UpdateAsync(Request request);

        Task Delete(Request request);
    }
}
