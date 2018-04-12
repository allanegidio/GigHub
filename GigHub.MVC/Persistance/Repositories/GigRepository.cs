﻿using GigHub.MVC.Core.Models;
using GigHub.MVC.Core.Repositories;
using GigHub.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.MVC.Persistance.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public Gig GetGigWithAttendees(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                        .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                        .Select(a => a.Gig)
                        .Include(g => g.Artist)
                        .Include(g => g.Genre)
                        .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                        .Where(g => g.ArtistId == userId &&
                            g.DateTime > DateTime.Now &&
                            !g.IsCanceled)
                        .Include(g => g.Genre)
                        .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public void Remove(Gig gig)
        {
            _context.Gigs.Remove(gig);
        }

        public IEnumerable<Gig> GetUpcomingGigs(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .Where(x => x.DateTime > DateTime.Now && !x.IsCanceled);

            if (query.IsNotNullOrEmpty())
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }

            return upcomingGigs.ToList();
        }
    }
}