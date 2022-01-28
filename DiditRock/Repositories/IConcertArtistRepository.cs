using System.Collections.Generic;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IConcertArtistRepository
    {
        void Add(ConcertArtist concertArtist);
        void Delete(ConcertArtist concertArtist);
        List<ConcertArtist> Get(int concertId);
    }
}