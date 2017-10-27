using GigHub2.Core.Dtos;
using GigHub2.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub2.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(n => new NotificationDto
            {
                DateTime = n.DateTime,
                Gig = new GigDto()
                {
                    Artist = new UserDto()
                    {
                        Id = n.Gig.Artist.Id,
                        Name = n.Gig.Artist.Name
                    },
                    Id = n.Gig.Id,
                    IsCanceled = n.Gig.IsCanceled,
                    Venue = n.Gig.Venue,
                    DateTime = n.Gig.DateTime
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type = n.Type
            });
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
