using FluentAssertions;
using GigHub.MVC.Core.Models;
using GigHub.MVC.Persistance;
using GigHub.MVC.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendance;
        private GigRepository _gigRepository;
        private AttendanceRepository _attendanceRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendance = new Mock<DbSet<Attendance>>();


            _gigRepository = new GigRepository(_mockContext.Object);
            _attendanceRepository = new AttendanceRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-5), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
            _mockContext.SetupGet(r => r.Gigs).Returns(_mockGigs.Object);

            var gigs = _gigRepository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(2), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });
            _mockContext.Setup(r => r.Gigs).Returns(_mockGigs.Object);

            var gigs = _gigRepository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(2), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
            _mockContext.Setup(r => r.Gigs).Returns(_mockGigs.Object);

            var gigs = _gigRepository.GetUpcomingGigsByArtist(gig.ArtistId + "-df212");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(2), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
            _mockContext.Setup(r => r.Gigs).Returns(_mockGigs.Object);

            var gigs = _gigRepository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
        

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance() { Gig = gig, AttendeeId = "1" };

            _mockAttendance.SetSource(new[] { attendance });
            _mockContext.Setup(r => r.Attendances).Returns(_mockAttendance.Object);

            var gigs = _gigRepository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(2) };
            var attendance = new Attendance() { Gig = gig, AttendeeId = "1" };

            _mockAttendance.SetSource(new[] { attendance });
            _mockContext.Setup(r => r.Attendances).Returns(_mockAttendance.Object);

            var gigs = _gigRepository.GetGigsUserAttending(attendance.AttendeeId + "-1234325");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_UpcomingGigUserAttending_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(2) };
            var attendance = new Attendance() { Gig = gig, AttendeeId = "1" };

            _mockAttendance.SetSource(new[] { attendance });
            _mockContext.Setup(r => r.Attendances).Returns(_mockAttendance.Object);

            var gigs = _gigRepository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }
    }
}
