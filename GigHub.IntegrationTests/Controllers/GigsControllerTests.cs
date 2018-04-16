using FluentAssertions;
using GigHub.IntegrationTests.Extensions;
using GigHub.MVC.Controllers;
using GigHub.MVC.Core.Models;
using GigHub.MVC.Core.ViewModels;
using GigHub.MVC.Persistance;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        
        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //ARRANGE
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { ArtistId = user.Id, DateTime = DateTime.Now.AddDays(5), Genre = genre, Venue = "-" };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //ACT
            var result = _controller.Mine();

            //ASSERT
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        {
            //ARRANGE
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Venue = "Venue", DateTime = DateTime.Now.AddDays(1), Artist = user, Genre = genre };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //ACT
            var result = _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Venue = "New Venue",
                Date = DateTime.Now.AddMonths(1).ToString("d MMM yyyy"),
                Time = "23:00",
                Genre = 2
            });


            //ASSERT
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(23));
            gig.Venue.Should().Be("New Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}
