using GigHub2.Core.Models;
using GigHub2.Core.Repositories;
using System.Collections.Generic;
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

        public IEnumerable<ApplicationUser> GetFollowees(string followerId)
        {
            return _context.Followings
                    .Where(a => a.FollowerId == followerId)
                    .Select(a => a.Followee)
                    .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}