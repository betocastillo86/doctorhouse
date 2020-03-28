using System;
using Beto.Core.Data;
using Beto.Core.Data.Notifications;

namespace DoctorHouse.Data
{
    public partial class Notification : IEntity, INotificationEntity
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public string EmailHtml { get; set; }

        public string EmailSubject { get; set; }

        public bool IsEmail { get; set; }

        public bool IsSystem { get; set; }

        public string Name { get; set; }

        public string SystemText { get; set; }

        public string Tags { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsMobile { get; set; }

        public string MobileText { get; set; }
    }
}