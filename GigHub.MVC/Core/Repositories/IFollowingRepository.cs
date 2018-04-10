using GigHub.MVC.Core.Models;

namespace GigHub.MVC.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
    }
}
