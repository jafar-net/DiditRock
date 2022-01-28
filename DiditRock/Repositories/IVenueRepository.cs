using System.Collections.Generic;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IVenueRepository
    {
        List<Venue> GetAll();
        void Add(Venue venue);
        void Delete(int id);
        void Update(Venue venue);
        Venue GetById(int id);
    }
}