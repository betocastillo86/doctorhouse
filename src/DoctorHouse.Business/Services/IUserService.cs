using System.Threading.Tasks;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface IUserService
    {
        User GetByEmail(string email);

        User GetById(int id, bool includeAttributes = false);

        Task<User> GetByIdAsync(int id);

        Task InsertAsync(User user);

        Task UpdateAsync(User user);

        Task<User> ValidateAuthentication(string email, string password);
    }
}