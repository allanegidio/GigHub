using GigHub.MVC.Core.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
