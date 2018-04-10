using GigHub.MVC.Core.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);
    }
}
