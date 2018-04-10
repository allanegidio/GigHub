using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.MVC.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification() { }
        public static Notification GigCreated(Gig gig) => new Notification(NotificationType.GigCreated, gig);
        public static Notification GigCancel(Gig gig) => new Notification(NotificationType.GigCanceled, gig);
        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        private Notification(NotificationType type, Gig gig)
        {
            Type = type;
            Gig = gig ?? throw new ArgumentNullException("gig");
            DateTime = DateTime.Now;
        }
    }
}