using DiditRock.Models;
using System.Collections.Generic;

namespace DiditRock.Repositories
{
    public interface IConcertArtistRepository
    {
        void Add(ConcertArtist concertArtist);
        void Delete(int id);
        ConcertArtist GetById(int id);
        void clearConcertArtistsForConcert(int concertId);
    }
}