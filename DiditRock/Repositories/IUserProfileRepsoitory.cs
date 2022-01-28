using System.Collections.Generic;
using DiditRock.Models;

namespace DiditRock.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetAllUsers();
        UserProfile GetUserProfileId(int id);
        void UpdateUserTypeId(int userTypeId, int userId);
        UserProfile GetUserProfileById(int id);
        List<UserType> AllUserTypes();
    }
}