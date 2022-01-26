using System.Collections.Generic;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetAllUsers();
        void ReactivateAndDeactivate(UserProfile userProfile);
        UserProfile GetUserProfileId(int id);

        UserProfile GetUserProfileById(int id);
    }
}