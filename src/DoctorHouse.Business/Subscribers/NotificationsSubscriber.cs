using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beto.Core.Data.Notifications;
using Beto.Core.EventPublisher;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.Extensions.Configuration;

namespace DoctorHouse.Business.Subscribers
{
    public class NotificationsSubscriber : ISubscriber<EntityInsertedMessage<Request>>
    {
        private readonly INotificationService notificationService;

        private readonly IConfiguration configuration;

        private readonly IWebCacheService webCacheService;

        public NotificationsSubscriber(
            INotificationService notificationService,
            IConfiguration configuration,
            IWebCacheService webCacheService)
        {
            this.notificationService = notificationService;
            this.configuration = configuration;
            this.webCacheService = webCacheService;
        }

        public async Task HandleEvent(EntityInsertedMessage<Request> message)
        {
            var request = message.Entity;

            var url = $"{this.configuration["SiteUrl"]}/requests";

            var requester = this.webCacheService.GetUserById(request.UserRequesterId);

            var parameters = new List<NotificationParameter>();
            parameters.Add("UserRequester.Name", requester.Name);
            parameters.Add("UserRequester.PhoneNumber", requester.PhoneNumber);

            await this.notificationService.NewNotification(requester, null, NotificationType.NewRequest, url, parameters);
        }
    }
}