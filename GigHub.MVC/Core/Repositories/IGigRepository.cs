using GigHub.MVC.Core.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int id);
        Gig GetGigWithAttendees(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        IEnumerable<Gig> GetUpcomingGigs(string query = null);
        void Add(Gig gig);
        void Remove(Gig gig);
    }
}
