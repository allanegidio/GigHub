using GigHub.MVC.Core.Models;
using GigHub.MVC.Core.Repositories;
using System.Linq;

namespace GigHub.MVC.Persistance.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
            => _context = context;

        public Following GetFollowing(string artistId, string userId) =>
            _context.Followings
                .SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);

        public void Add(Following following) => _context.Followings.Add(following);

        public void Remove(Following following) => _context.Followings.Remove(following);
    }
}