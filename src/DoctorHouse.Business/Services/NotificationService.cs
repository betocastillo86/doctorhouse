using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beto.Core.Caching;
using Beto.Core.Data;
using Beto.Core.Data.Notifications;
using Beto.Core.Data.Users;
using Beto.Core.EventPublisher;
using DoctorHouse.Data;
using Microsoft.Extensions.Configuration;

namespace DoctorHouse.Business.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ICoreNotificationService coreNotificationService;

        private readonly IRepository<EmailNotification> emailNotificationRepository;

        private readonly IRepository<Notification> notificationRepository;

        private readonly ICacheManager cacheManager;

        private readonly IPublisher publisher;

        private readonly IConfiguration configuration;

        private readonly IUserService userService;

        public NotificationService(
            IRepository<Notification> notificationRepository,
            ICoreNotificationService coreNotificationService,
            ICacheManager cacheManager,
            IRepository<EmailNotification> emailNotificationRepository,
            IUserService userService,
            IPublisher publisher,
            IConfiguration configuration)
        {
            this.notificationRepository = notificationRepository;
            this.coreNotificationService = coreNotificationService;
            this.cacheManager = cacheManager;
            this.emailNotificationRepository = emailNotificationRepository;
            this.userService = userService;
            this.publisher = publisher;
            this.configuration = configuration;
        }

        public bool IsNotificationActive(NotificationType type)
        {
            var notification = this.GetCachedNotifications().FirstOrDefault(c => c.Id == Convert.ToInt32(type));
            return notification != null ? notification.Active && (notification.IsMobile || notification.IsEmail || notification.IsSystem) : false;
        }

        public async Task NewAdminNotification(string to, string subject, string message)
        {
            var notification = new EmailNotification
            {
                Body = message,
                CreatedDate = DateTime.UtcNow,
                Subject = subject,
                To = to,
                ToName = "Administrador"
            };

            await this.emailNotificationRepository.InsertAsync(notification);
        }

        public async Task NewNotification(
            User user,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters)
        {
            await this.NewNotification(user, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        public async Task NewNotification(
                    User user,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters,
                    string defaultFromName,
                    string defaultSubject,
                    string defaultMessage)
        {
            var list = new List<User>() { user };
            await this.NewNotification(list, userTriggerEvent, type, targetUrl, parameters, defaultFromName, defaultSubject, defaultMessage);
        }

        public async Task NewNotification(
                    IList<User> users,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters)
        {
            await this.NewNotification(users, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        public async Task NewNotification(
                    IList<User> users,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters,
                    string defaultFromName,
                    string defaultSubject,
                    string defaultMessage)
        {
            var notificationId = Convert.ToInt32(type);
            var notification = this.GetCachedNotifications()
                .FirstOrDefault(n => n.Id == notificationId);

            var settings = new Beto.Core.Data.Notifications.NotificationSettings()
            {
                BaseHtml = this.configuration["BaseHtmlBody"],
                DefaultFromName = defaultFromName,
                DefaultMessage = defaultMessage,
                DefaultSubject = defaultSubject,
                IsManual = false,
                SiteUrl = this.configuration["SiteUrl"]
            };

            try
            {
                await this.coreNotificationService.NewNotification<MockSystemNotification, EmailNotification, DefaultMobileNotification, DefaultUnsubscriber>(
                users.Select(c => (IUserEntity)c).ToList(),
                userTriggerEvent,
                notification,
                targetUrl,
                parameters,
                settings);
            }
            catch
            {
                Console.WriteLine($"-----------------------------------------------> Error saving notification {type}");
            }
        }

        private IList<Notification> GetCachedNotifications()
        {
            return this.cacheManager.Get(
                "cache.notifications.all",
                10,
                () =>
                {
                    return this.notificationRepository.Table
                        .Where(c => !c.Deleted)
                        .ToList();
                });
        }
    }
}