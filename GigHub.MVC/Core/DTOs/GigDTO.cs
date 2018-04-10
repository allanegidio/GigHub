using System;

namespace GigHub.MVC.Core.DTOs
{
    public class GigDTO
    {
        public int Id { get; set; }

        public string Venue { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUserDTO Artist { get; set; }

        public DateTime DateTime { get; set; }

        public GenreDTO Genre { get; set; }

    }
}