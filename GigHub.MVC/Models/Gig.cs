using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.MVC.Models
{
    public class Gig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }
    }
}