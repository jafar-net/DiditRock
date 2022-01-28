using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IConcertRepository
    {
        List<Concert> GetAll();
        Concert GetById(int id);
        void Add(Concert category);
        void Update(Concert category);
        void Delete(int id);

    }
}