using System.Collections.Generic;
using System.Threading.Tasks;
using Beto.Core.Data;
using Beto.Core.Data.Notifications;
using DoctorHouse.Data;

namespace DoctorHouse.Business.Services
{
    public interface INotificationService
    {
        bool IsNotificationActive(NotificationType type);

        Task NewAdminNotification(string to, string subject, string message);

        Task NewNotification(
            User user,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters);

        Task NewNotification(
            User user,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters,
            string defaultFromName,
            string defaultSubject,
            string defaultMessage);

        Task NewNotification(
            IList<User> users,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters);

        Task NewNotification(
            IList<User> users,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters,
            string defaultFromName,
            string defaultSubject,
            string defaultMessage);
    }
}