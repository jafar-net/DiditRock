using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Review { get; set; }
        public string ImageUrl { get; set; }
        public string SeatNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
        public int UserId { get; set; }
        public UserProfile UserProfile { get; set; }
        public bool IsByCurrentUser { get; set; }

    }
}