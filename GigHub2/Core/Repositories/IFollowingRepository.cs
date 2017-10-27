using GigHub2.Core.Models;
using System.Collections.Generic;

namespace GigHub2.Core.Repositories
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        void Remove(Following following);
        Following GetFollowing(string followeeId, string followerId);
        IEnumerable<ApplicationUser> GetFollowees(string followerId);       
    }
}