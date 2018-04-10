using GigHub.MVC.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.MVC.Persistance.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Gig);
        }
    }
}