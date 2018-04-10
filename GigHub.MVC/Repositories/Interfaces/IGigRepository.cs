using GigHub.MVC.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Repositories.Interfaces
{
    public interface IGigRepository
    {
        Gig GetGig(int id);
        Gig GetGigWithAttendees(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        void Add(Gig gig);
        void Remove(Gig gig);
    }
}
