using GigHub2.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub2.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Followees()
        {
            var userId = User.Identity.GetUserId();
            var followees = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();

            return View(followees);
        }
    }
}