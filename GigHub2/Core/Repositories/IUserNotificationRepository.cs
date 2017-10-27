using GigHub2.Core.Models;
using System.Collections.Generic;

namespace GigHub2.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<Notification> GetUnreadNotificationsOfUser(string userId);
        IEnumerable<UserNotification> GetUnreadUserNotifications(string userId);
    }
}