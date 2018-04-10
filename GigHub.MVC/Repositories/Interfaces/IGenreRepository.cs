using GigHub.MVC.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
