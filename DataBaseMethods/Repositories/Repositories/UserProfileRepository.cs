using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext context;

        public UserProfileRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateUserProfile(int userId, string firstName, string middleName, string lastName, DateTime birthDay)
        {
            try
            {
                if (firstName == null || middleName == null || lastName == null)
                {
                    throw new ArgumentNullException($"Ошибка при создании профиля пользователя, переданы некорректные данные: firstName = {nameof(firstName)} \n lastName = {nameof(lastName)} \n middleName = {nameof(middleName)}");
                }
                else
                {
                    DateTime utcBirthday = TimeZoneInfo.ConvertTimeToUtc(birthDay);

                    var newUserProfile = new UserProfile()
                    {
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Birthday = utcBirthday,
                        IsActived = true,
                        DateCreatedAt = DateTime.UtcNow,
                        DateUpdatedAt = DateTime.UtcNow,
                        DateDelete = null,
                        UserId = userId,
                    };

                    context.UserProfiles.Add(newUserProfile);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполнении операции создания профиля пользователя возникла ошибка - {ex.Message}");
                return false;
            }
        }

        public async Task<List<UserProfile>> GetAllUserProfile()
        {
            return await context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile?> GetUserProfileByUserId(int userId)
        {
            try
            {
                if(userId > 0)
                {
                    return await context.UserProfiles.SingleOrDefaultAsync(el => el.UserId == userId);
                }
                else
                {
                    throw new ArgumentException($"Передано некорректное значение для поиска - {userId}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске профиля пользователя по userId, {ex.Message}");
                return null;
            }
        }

        public async Task<UserProfile?> GetUserProfileByUserProfileId(int userProfileId)
        {
            try
            {
                if (userProfileId > 0)
                {
                    return await context.UserProfiles.FindAsync(userProfileId);
                }
                else
                {
                    throw new ArgumentException($"Передано некорректоное значение для поиска - {userProfileId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске профиля пользователя по userProfileId, error - {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveUserProfileByUserProfileId(int userProfileId)
        {
            try
            {
                if(userProfileId > 0)
                {
                    var userProfile = await GetUserProfileByUserProfileId(userProfileId);
                    if (userProfile != null)
                    {
                        userProfile.IsActived = false;
                        userProfile.DateUpdatedAt = DateTime.UtcNow;
                        userProfile.DateDelete = DateTime.UtcNow;
                        await context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        throw new NullReferenceException($"По заданному userProfileId = {userProfileId} не удалось найти запись профиля пользователя");
                    }
                }
                else
                {
                    throw new ArgumentException($"Передано некорреткное значение userProfileId - {userProfileId}, деактивация невозможна.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при деактивации записи профиля пользователя - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserProfileByUserProfileId(int userProfileId, string firstName, string middleName, string lastName, DateTime birthDay)
        {
            try
            {
                if(userProfileId <= 0 || firstName.IsNullOrEmpty() || middleName.IsNullOrEmpty() || lastName.IsNullOrEmpty())
                {
                    throw new NullReferenceException($"Переданы некорректные данные для обновления: userProfileId = {nameof(userProfileId)} \n firstName = {nameof(firstName)} \n middleName = {nameof(middleName)} \n lastName = {nameof(lastName)}");
                }
                else
                {
                    var oldUserProfile = await GetUserProfileByUserProfileId(userProfileId);
                    
                    if (oldUserProfile != null)
                    {
                        DateTime utcBirthday = TimeZoneInfo.ConvertTimeToUtc(birthDay);

                        oldUserProfile.FirstName = firstName;
                        oldUserProfile.MiddleName = middleName;
                        oldUserProfile.LastName = lastName;
                        oldUserProfile.Birthday = utcBirthday;
                        oldUserProfile.DateUpdatedAt= DateTime.UtcNow;

                        await context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        throw new NullReferenceException($"При поиске профиля пользователя для обновления данных, ничего не найдено");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполненеии операции обновлдения данных профиля пользователя, возникла ошибка - {ex.Message}");
                return false;
            }
        }
    }
}
