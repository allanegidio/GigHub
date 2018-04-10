using GigHub.MVC.Core.Models;
using GigHub.MVC.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.MVC.Persistance.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> GetArtistsFollowedByUser(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }
    }
}