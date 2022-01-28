using DiditRock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Repositories
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();
        void Add(Artist tag);
        void Delete(int id);
        void Update(Artist tag);
        Artist GetById(int id);
    }
}