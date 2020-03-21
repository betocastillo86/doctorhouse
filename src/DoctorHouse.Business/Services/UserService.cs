using System;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.EventPublisher;
using Beto.Core.Helpers;
using DoctorHouse.Business.Exceptions;
using DoctorHouse.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoctorHouse.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        private readonly IPublisher publisher;

        public UserService(
            IRepository<User> userRepository,
            IPublisher publisher)
        {
            this.userRepository = userRepository;
            this.publisher = publisher;
        }

        public User GetByEmail(string email)
        {
            return this.userRepository.TableNoTracking
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Email.Equals(email) && !c.Deleted);
        }

        public User GetById(int id, bool includeAttributes = false)
        {
            var user = this.userRepository.TableNoTracking
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id && !c.Deleted);

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await this.userRepository.TableNoTracking
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        public async Task InsertAsync(User user)
        {
            if (this.UserAlreadyExists(user.Email))
            {
                throw new DoctorHouseException(DoctorHouseExceptionCode.UserEmailAlreadyExists);
            }

            user.CreationDate = DateTime.UtcNow;

            try
            {
                await this.userRepository.InsertAsync(user);
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

            await this.publisher.EntityInserted(user);
        }

        public bool UserAlreadyExists(string email)
        {
            return this.userRepository.TableNoTracking.Any(c => c.Email.Equals(email));
        }

        public async Task UpdateAsync(User user)
        {
            var emailAlreadyUsed = await this.userRepository.TableNoTracking.AnyAsync(c => c.Email.Equals(user.Email) && c.Id != user.Id);

            if (emailAlreadyUsed)
            {
                throw new DoctorHouseException(DoctorHouseExceptionCode.UserEmailAlreadyExists);
            }

            try
            {
                await this.userRepository.UpdateAsync(user);
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

            await this.publisher.EntityUpdated(user);
        }

        public async Task<User> ValidateAuthentication(string email, string password)
        {
            var user = await this.userRepository.TableNoTracking.FirstOrDefaultAsync(c => c.Email.Equals(email) && !c.Deleted);

            if (user != null)
            {
                var passwordSha = StringHelpers.ToSha1(password, user.Salt);
                return passwordSha.Equals(user.Password) ? user : null;
            }
            else
            {
                return null;
            }
        }
    }
}