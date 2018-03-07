using GigHub.MVC.Models;
using GigHub.MVC.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.MVC.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var gigViewModel = new GigFormViewModel();
            gigViewModel.Genres = _context.Genres.ToList();

            return View(gigViewModel);
        }
    }
}