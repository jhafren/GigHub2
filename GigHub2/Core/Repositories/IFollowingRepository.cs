using GigHub2.Core.Models;

namespace GigHub2.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
    }
}