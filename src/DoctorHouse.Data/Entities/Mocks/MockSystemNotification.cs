using System;
using Beto.Core.Data.Notifications;

namespace DoctorHouse.Data
{
    public class MockSystemNotification : ISystemNotificationEntity
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TargetUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Seen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}