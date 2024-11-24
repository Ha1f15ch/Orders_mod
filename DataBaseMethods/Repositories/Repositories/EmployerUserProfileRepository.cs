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
    public class EmployerUserProfileRepository : IEmployerUserProfileRepository
    {
        private readonly AppDbContext context;

        public EmployerUserProfileRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateEmployerUserProfile(int userId)
        {
            try
            {
                if(userId > 0)
                {
                    var user = await context.Users.FindAsync(userId);

                    if (user != null)
                    {
                        var employerUserProfile = new EmployerProfile
                        {
                            IsActived = true,
                            UserId = user.Id,
                        };

                        await context.EmployerProfiles.AddAsync(employerUserProfile);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                Console.WriteLine($"Предоставлены некорректные даныне для поиска учетной записи пользователя, чтобы выполнить корректно привязку - {userId}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При создании профиля исполнителя возникла ошибка - {ex.Message}");
                return false;
            }
        }

        public async Task<EmployerProfile?> GetEmployerUserprofileByUserId(int? userId)
        {
            try
            {
                if(userId > 0)
                {
                    return await context.EmployerProfiles.SingleOrDefaultAsync(p => p.UserId == userId);
                }

                throw new InvalidOperationException($"Передано некоррекное значение для поиска. userId = {userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При получении профиля исполнителя по значению userId = {userId}, возникла ошибка - {ex.Message}");
                return null;
            }
        }
    }
}
