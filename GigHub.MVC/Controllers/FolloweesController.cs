using GigHub.MVC.Core;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.MVC.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _unitOfWork.Users
                .GetArtistsFollowedByUser(userId)
                .Select(u => u.Name);

            return View(artists);
        }
    }
}