using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IUserRoleRepository
    {
        public Task<bool> CreateUserRoleById(int userId, int roleId);
        public Task SetDefaultUserRole(int userId, int roleId = 1); //userRole = USER
        public Task<bool> DeleteUserRoleById(int userId, int roleId);
        public Task<List<UserRole>> GetUserRolesByUserId(int userId);
        public Task<UserRole?> GetUserRoleByUserIdAndRoleName(int userId, string roleName);
        public Task<UserRole?> GetUserRoleByUserIdAndRoleId(int userId, int roleId);
    }
}
