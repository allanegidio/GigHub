using GigHub.MVC.Core.Models;

namespace GigHub.MVC.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
        void Add(Following following);
        void Remove(Following following);
    }
}
