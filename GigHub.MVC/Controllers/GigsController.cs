using GigHub.MVC.Models;
using GigHub.MVC.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.Following = _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
        
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                        .Where(g => g.ArtistId == userId &&
                            g.DateTime > DateTime.Now && 
                            !g.IsCanceled)
                        .Include(g => g.Genre)
                        .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId)
                                .Select(a => a.Gig)
                                .Include(g => g.Artist)
                                .Include(g => g.Genre)
                                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var gigsViewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendances
            };

            return View("Gigs", gigsViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var gigViewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm", gigViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                viewModel.Heading = "Add a Gig";
                return View("GigForm", viewModel);
            }
                
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var gigFormViewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Id = gig.Id,
                Heading = "Edit a Gig"
            };

            return View("GigForm", gigFormViewModel);
        }

        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(),viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            _context.Gigs.Remove(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}