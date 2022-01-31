using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Models
{
    public class ConcertArtist
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}