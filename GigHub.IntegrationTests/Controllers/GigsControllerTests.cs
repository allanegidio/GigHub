using FluentAssertions;
using GigHub.IntegrationTests.Extensions;
using GigHub.MVC.Controllers;
using GigHub.MVC.Core.Models;
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
        public void Dispose()
        {
            _context.Dispose();
        }
        
        [Test, Isolated]
        public void Mine_WhenCalled_ShouldREturnUpcomingGigs()
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
    }
}
