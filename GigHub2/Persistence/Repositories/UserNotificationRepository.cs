﻿using GigHub2.Core.Models;
using GigHub2.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub2.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetUnreadNotificationsOfUser(string userId)
        {
            return _context.UserNotifications
                    .Where(un => un.UserId == userId && !un.IsRead)
                    .Select(un => un.Notification)
                    .Include(n => n.Gig.Artist)
                    .ToList();
        }

        public IEnumerable<UserNotification> GetUnreadUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }
    }
}