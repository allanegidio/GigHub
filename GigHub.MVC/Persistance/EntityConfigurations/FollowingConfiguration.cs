using GigHub.MVC.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.MVC.Persistance.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}