using GigHub.MVC.Models;
using GigHub.MVC.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.MVC.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}