using FluentAssertions;
using GigHub.IntegrationTests.Extensions;
using GigHub.MVC.Controllers.Api;
using GigHub.MVC.Core.Models;
using GigHub.MVC.Persistance;
using NUnit.Framework;
using System;
using System.Linq;
using System.Web.Http.Results;

namespace GigHub.IntegrationTests.Controllers.Api
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
        public void Cancel_WhenCalled_ShouldReturnOk()
        {
            //ARRANGE
            var user = _context.Users.First();
            _controller.MockCurrentUserApi(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Venue = "Venue", DateTime = DateTime.Now.AddDays(3), Artist = user, Genre = genre };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //ACT
            var result = _controller.Cancel(gig.Id);

            //ASSERT
            result.Should().BeOfType<OkResult>();
        }
    }
}
