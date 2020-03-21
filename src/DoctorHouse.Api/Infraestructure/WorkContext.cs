using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using DoctorHouse.Business.Security;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Http;

namespace DoctorHouse.Api.Infraestructure
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private User currentUser;

        private IWebCacheService webCacheService;

        public WorkContext(
            IHttpContextAccessor httpContextAccessor,
            IWebCacheService webCacheService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.webCacheService = webCacheService;
        }

        public User CurrentUser
        {
            get
            {
                if (this.httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated == true)
                {
                    if (this.currentUser == null)
                    {
                        this.currentUser = this.webCacheService.GetUserById(this.CurrentUserId);
                    }
                }

                return this.currentUser;
            }
        }

        public int CurrentUserId => this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? Convert.ToInt32(this.httpContextAccessor.HttpContext.User.FindFirst(OpenIdConnectConstants.Claims.Subject).Value) : 0;

        public bool IsAuthenticated => this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
