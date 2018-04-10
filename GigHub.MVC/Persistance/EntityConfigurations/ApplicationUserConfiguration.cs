using GigHub.MVC.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.MVC.Persistance.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasKey(a => a.Id);

            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            //Um Artista tem varios seguidores(Followers) - Precisa de ser seguido (Followee)
            HasMany(a => a.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            //Um Artista está seguindo pessoas (Followees) - Precisa de um seguidor (Follower)
            HasMany(a => a.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);
        }
    }
}