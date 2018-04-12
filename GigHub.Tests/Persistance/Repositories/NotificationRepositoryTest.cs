using FluentAssertions;
using GigHub.MVC.Core.Models;
using GigHub.MVC.Persistance;
using GigHub.MVC.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTest
    {
        private Mock<IApplicationDbContext> _mockContext;
        private Mock<DbSet<UserNotification>> _mockUserNotification;
        private NotificationRepository _notificationRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockUserNotification = new Mock<DbSet<UserNotification>>();

            _notificationRepository = new NotificationRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser() { Id = "1231" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUserNotification.SetSource(new[] { userNotification });
            _mockContext.Setup(r => r.UserNotifications).Returns(_mockUserNotification.Object);

            var notifications = _notificationRepository.GetNewNotificationsFor(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationFor_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser() { Id = "1231" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotification.SetSource(new[] { userNotification });
            _mockContext.Setup(r => r.UserNotifications).Returns(_mockUserNotification.Object);

            var notifications = _notificationRepository.GetNewNotificationsFor(user.Id + "12412321312");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationFor_NewNotificationForTheGivenUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser() { Id = "1231" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotification.SetSource(new[] { userNotification });
            _mockContext.Setup(r => r.UserNotifications).Returns(_mockUserNotification.Object);

            var notifications = _notificationRepository.GetNewNotificationsFor(user.Id);

            notifications.Should().Contain(notification);
        }
    }
}
