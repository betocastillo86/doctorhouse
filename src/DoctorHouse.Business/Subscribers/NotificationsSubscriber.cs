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
    public class NotificationsSubscriber : ISubscriber<EntityInsertedMessage<User>>
    {
        private readonly INotificationService notificationService;

        private readonly IConfiguration configuration;

        public NotificationsSubscriber(
            INotificationService notificationService,
            IConfiguration configuration)
        {
            this.notificationService = notificationService;
            this.configuration = configuration;
        }

        public async Task HandleEvent(EntityInsertedMessage<User> message)
        {
            var user = message.Entity; // TODO: Change only when request insertion

            var url = $"{this.configuration["SiteUrl"]}/requests";

            var parameters = new List<NotificationParameter>();
            parameters.Add("UserRequester.Name", user.Name);
            parameters.Add("UserRequester.PhoneNumber", user.PhoneNumber);

            await this.notificationService.NewNotification(user, null, NotificationType.NewRequest, url, parameters);
        }
    }
}