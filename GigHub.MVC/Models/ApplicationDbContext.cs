using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }


        public ApplicationDbContext()
            : base("GigHubConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany(g => g.Attendances)
                .WillCascadeOnDelete(false);

            //Um Artista tem varios seguidores(Followers) - Precisa de ser seguido (Followee)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            //Um Artista está seguindo pessoas (Followees) - Precisa de um seguidor (Follower)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}