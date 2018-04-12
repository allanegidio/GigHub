using FluentAssertions;
using GigHub.MVC.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class NotificationTest
    {
        [TestMethod]
        public void GigCanceled_WhenCalled_ShouldReturnANotificationForACanceledGig()
        {
            var gig = new Gig();

            var notification = Notification.GigCanceled(gig);

            notification.Type.Should().Be(NotificationType.GigCanceled);
            notification.Gig.Should().Be(gig);
        }
    }
}
