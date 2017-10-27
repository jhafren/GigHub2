using GigHub2.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub2.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new { un.UserId, un.NotificationId });

            Property(un => un.UserId)
                .HasColumnOrder(1);

            Property(un => un.NotificationId)
                .HasColumnOrder(2);
        }
    }
}