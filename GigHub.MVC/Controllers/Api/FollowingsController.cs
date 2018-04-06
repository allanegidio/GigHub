using GigHub.MVC.DTOs;
using GigHub.MVC.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.MVC.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        public ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO dto)
        {
            var userId = User.Identity.GetUserId();
            var duplicated = _context.Followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId);

            if (duplicated)
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                .FirstOrDefault(f => f.FolloweeId == id && f.FollowerId == userId);

            if (following == null)
                return NotFound();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
