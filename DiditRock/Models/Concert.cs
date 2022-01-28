using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Models
{
    public class Concert { 
    public int Id { get; set; }
    public string Name { get; set; }
    public string EncoreSongs { get; set; }
    public DateTime Date { get; set; }
    public string Genre { get; set; }
    public int VenueId { get; set; }
    public Venue Venue { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    public bool IsByCurrentUser { get; set; }

}
}