using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CommonProfileData : ICommonProfileData
    {
        private readonly AppDbContext context;

        public CommonProfileData(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IDictionary<string, object?>> ReturnUserProfilesByUserId(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    var userProfile = await context.UserProfiles.SingleOrDefaultAsync(el => el.UserId == userId);
                    var customerProfile = await context.CustomerProfiles.SingleOrDefaultAsync(el => el.UserId == userId);
                    var employerProfile = await context.EmployerProfiles.SingleOrDefaultAsync(el => el.UserId == userId);

                    var dictionaryUserProfiles = new Dictionary<string, object?>
                    {
                        {"UserProfile", userProfile },
                        {"CustomerProfile", customerProfile },
                        {"EmployerProfile", employerProfile }
                    };

                    return dictionaryUserProfiles;
                }

                throw new ArgumentNullException($"Возникла ошибка при проверке обрабатываемых данных. Передано некорректное значение userId - {nameof(userId)}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При формировании справочника с данными профилей пользователя возникла ошибка - {ex.Message}");
                return new Dictionary<string, object?>()
                {
                    {"UserProfile", null },
                    {"CustomerProfile", null },
                    {"EmployerProfile", null }
                };
            }
        }
    }
}
