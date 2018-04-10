using GigHub.MVC.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.MVC.Persistance.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(g => g.Id);

            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}