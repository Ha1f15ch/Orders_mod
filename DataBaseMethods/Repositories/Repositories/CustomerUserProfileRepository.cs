using DatabaseContext;
using Models;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CustomerUserProfileRepository : ICustomerUserProfileRepository
    {
        private readonly AppDbContext context;

        public CustomerUserProfileRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateCustomerUserProfile(int userId)
        {
            try
            {
                if(userId > 0)
                {
                    var user = await context.Users.FindAsync(userId);

                    if(user != null)
                    {
                        var customerUserProfile = new CustomerProfile
                        {
                            IsActived = true,
                            UserId = user.Id,
                        };

                        await context.CustomerProfiles.AddAsync(customerUserProfile);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании профиля пользователя - {ex.Message}");
                return false;
            }
        }
    }
}
