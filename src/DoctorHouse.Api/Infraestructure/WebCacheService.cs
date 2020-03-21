using Beto.Core.Caching;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Infraestructure
{
    public class WebCacheService : IWebCacheService
    {
        private readonly ICacheManager cacheManager;
        private readonly IUserService userService;

        public WebCacheService(
            ICacheManager cacheManager,
            IUserService userService)
        {
            this.cacheManager = cacheManager;
            this.userService = userService;
        }

        public User GetUserById(int id)
        {
            return this.cacheManager.Get(
                $"cache.web.user.byid.{id}",
                10,
                () =>
                {
                    return this.userService.GetById(id, includeAttributes: true);
                });
        }
    }
}