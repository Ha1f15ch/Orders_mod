using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IUserAccauntRepository
    {
        public Task<bool> CreateUserAccaunt(string user_name, string user_email, string user_password, string user_phoneNumber);
        public Task<bool> UpdateUserAccaunt(int user_id, string new_user_name, string new_user_email, string new_user_password, string new_user_phoneNumber);
        public Task<bool> DeleteUserAccaunt(int user_id);
        public Task<bool> VerifyPassword(User userModel, string innerPassword);
        public Task<bool> ReturnHasUser(int userId);
        public Task<User?> GetUserAccauntByUserId(int user_id);
        public Task<User?> GetUserAccauntByUserName(string user_name);
        public Task<User?> GetUserAccauntByUserEmail(string user_email);
        public Task<User?> GetUserAccauntByUserPhoneNumber(string user_phoneNumber);
        public Task<List<User>> GetAllUsers();
        public Task<List<User>> GetAllUsersByUserName(string user_name);
        public Task<List<User>> GetAllUsersByUserEmail(string user_email);
    }
}
