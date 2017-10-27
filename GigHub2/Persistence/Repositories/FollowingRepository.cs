using GigHub2.Core.Models;
using GigHub2.Core.Repositories;
using System.Linq;

namespace GigHub2.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followeeId, string followerId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }
    }
}