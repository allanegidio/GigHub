using GigHub.MVC.Core.Models;
using System;

namespace GigHub.MVC.Core.DTOs
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }
        
        public GigDTO Gig { get; set; }
    }
}