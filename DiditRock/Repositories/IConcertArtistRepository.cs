using System.Collections.Generic;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IConcertArtistRepository
    {
        void Add(ConcertArtist concertArtist);
        void Delete(int concertArtistId);
        List<ConcertArtist> GetAllConcertArtistsForConcert(int id);
        ConcertArtist GetConcertArtistById(int id);
    }
}