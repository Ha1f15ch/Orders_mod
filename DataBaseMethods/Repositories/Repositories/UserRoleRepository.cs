using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext context;

        public UserRoleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Task<bool> CreateUserRoleById(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task SetDefaultUserRole(int userId, int roleId)
        {
            try
            {
                var newUserRoleItem = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };

                await context.UserRoles.AddAsync(newUserRoleItem);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при привязке стандартной роли к пользователю. {ex.Message}");
            }
        }

        public Task<bool> DeleteUserRoleById(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole?> GetUserRoleByUserIdAndRoleName(int userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRole>> GetUserRolesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole?> GetUserRoleByUserIdAndRoleId(int userId, int roleId)
        {
            try
            {
                if(userId > 0 && roleId > 0)
                {
                    return await context.UserRoles.SingleOrDefaultAsync(el => el.UserId == userId && el.RoleId == roleId);
                }
                else
                {
                    throw new ArgumentException($"Переданы некорретные значения userId = {userId} или roleId = {roleId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске записи связи пользователя и роли - {ex.Message}");
                return null;
            }
        }
    }
}
