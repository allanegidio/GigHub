using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.MVC.Models
{
    public class Gig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}