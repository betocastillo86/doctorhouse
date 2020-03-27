using System;
using System.Collections.Generic;
using Beto.Core.Data;
using DoctorHouse.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DoctorHouse.Data
{
    public class DoctorHouseContext : DbContext, IDbContext
    {
        public DoctorHouseContext(DbContextOptions<DoctorHouseContext> options) : base(options)
        {
        }

        public virtual DbSet<Guest> Guests { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Place> Places { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestMapping());
            modelBuilder.ApplyConfiguration(new LocationMapping());
            modelBuilder.ApplyConfiguration(new PlaceMapping());
            modelBuilder.ApplyConfiguration(new RequestMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new NotificationMapping());
            modelBuilder.ApplyConfiguration(new EmailNotificationMapping());
        }

        public void BulkInsert<T>(IList<T> entities, BulkConfigCore bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            return;
        }
    }
}