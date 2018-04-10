using GigHub.MVC.Models;

namespace GigHub.MVC.Repositories.Interfaces
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
    }
}
