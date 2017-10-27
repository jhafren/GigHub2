using GigHub2.Models;

namespace GigHub2.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
    }
}