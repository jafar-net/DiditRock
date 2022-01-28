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
        List<Post> GetAll(int currentUserId);
        Post GetById(int id, int userId);
        void Update(Post post);
        public List<Post> GetUserPostsById(int userProfileId);
        public List<Post> GetAllPostsForUser(int userProfileId);

    }
}