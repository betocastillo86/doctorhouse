using System;
using Beto.Core.Data;
using Beto.Core.Data.Notifications;

namespace DoctorHouse.Data
{
    public partial class EmailNotification : IEntity, IEmailNotificationEntity
    {
        public int Id { get; set; }

        public string To { get; set; }

        public string ToName { get; set; }

        public string Cc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public DateTime? SentDate { get; set; }

        public short SentTries { get; set; }

        public bool IsSent { get; set; }
    }
}