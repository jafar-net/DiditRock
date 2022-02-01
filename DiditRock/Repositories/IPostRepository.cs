using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int id);
        List<Post> GetAll();
        Post GetById(int id);
        void Update(Post post);
        public List<Post> GetUserPostsById(int userProfileId);
        public List<Post> GetAllPostsForUser(int userProfileId);

    }
}