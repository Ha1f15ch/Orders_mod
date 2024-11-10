using Models;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IUserProfileRepository
    {
        public Task<UserProfile?> GetUserProfileByUserId(int userId);
        public Task<UserProfile?> GetUserProfileByUserProfileId(int userProfileId);
        public Task<List<UserProfile>> GetAllUserProfile();
        public Task<bool> CreateUserProfile(int userId, string firstName, string middleName, string lastName, DateTime birthDay);
        public Task<bool> UpdateUserProfileByUserProfileId(int userProfileId, string firstName, string middleName, string lastName, DateTime birthDay);
        public Task<bool> RemoveUserProfileByUserProfileId(int userProfileId);
        public Task<bool> HasUserProfileByUserId(int userId);
    }
}
