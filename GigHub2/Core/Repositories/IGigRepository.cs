using System.Collections.Generic;
using GigHub2.Core.Models;

namespace GigHub2.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigs();
        IEnumerable<Gig> GetUpcomingGigsByUser(string userId);
    }
}