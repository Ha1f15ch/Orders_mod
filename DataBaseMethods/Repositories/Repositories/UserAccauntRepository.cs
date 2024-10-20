using System;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.InterfaceRepositories;

namespace Repositories.Repositories
{
    public class UserAccauntRepository : IUserAccauntRepository
    {
        private readonly AppDbContext context;

        public UserAccauntRepository(AppDbContext context)
        {
            this.context = context;
        }

        private async Task<bool> IsUserExists(string user_name, string user_email, string user_phoneNumber)
        {
            var userByName = await GetUserAccauntByUserName(user_name);
            var userByEmail = await GetUserAccauntByUserEmail(user_email);
            var userByPhoneNumber = await GetUserAccauntByUserPhoneNumber(user_phoneNumber);

            return userByName != null || userByEmail != null || userByPhoneNumber != null;
        }

        public async Task<bool> CreateUserAccaunt(string user_name, string user_email, string user_password, string user_phoneNumber)
        {
            try
            {
                if (await IsUserExists(user_name, user_email, user_phoneNumber))
                {
                    throw new ArgumentException("Пользователь с таким именем, email или номером телефона уже существует.");
                }

                var newUser = new User
                {
                    Name = user_name,
                    Email = user_email,
                    Password = user_password,
                    PhoneNumber = user_phoneNumber
                };

                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при сохранении новой записи user с данными: \n user_name = {user_name} \n user_email = {user_email} \n user_password = {user_password} \n user_phoneNumber = {user_phoneNumber}, \n Либо уже существует запись с одним из парамтеров - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAccaunt(int user_id)
        {
            var user = await GetUserAccauntByUserId(user_id);

            if(user != null)
            {
                try
                {
                    context.Remove(user);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Возникла ошибка во время удаления записи user. Error - {ex.Message}");
                    return false;
                }
                
            }
            else
            {
                Console.WriteLine($"Удалить запись user по значению 'user_id = {user_id}' не удалось. Аккаунт user не найден");
                return false;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsersByUserEmail(string user_email)
        {
            if (!string.IsNullOrWhiteSpace(user_email))
            {
                return await context.Users.Where(el => el.Email.ToLower().Contains(user_email.ToLower())).ToListAsync();
            }
            else
            {
                Console.WriteLine("Передана необрабатываемая строка для поиска Email");
                return await context.Users.ToListAsync();
            }
        }

        public async Task<List<User>> GetAllUsersByUserName(string user_name)
        {
            if (!string.IsNullOrWhiteSpace(user_name))
            {
                return await context.Users.Where(el => el.Name.ToLower().Contains(user_name.ToLower())).ToListAsync();
            }
            else
            {
                Console.WriteLine("Передана необрабатываемая строка для поиска userName");
                return await context.Users.ToListAsync();
            }
        }

        public async Task<User?> GetUserAccauntByUserEmail(string user_email)
        {
            if (!string.IsNullOrWhiteSpace(user_email))
            {
                return await context.Users.SingleOrDefaultAsync(el => el.Email == user_email);
            }
            else
            {
                Console.WriteLine($"Передан некорректный параметр для поиска: user_email = {user_email}");
                return null;
            }
        }

        public async Task<User?> GetUserAccauntByUserId(int user_id)
        {
            if(user_id > 0)
            {
                try
                {
                    return await context.Users.FindAsync(user_id);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Не удалось выполнить поиск User по id, {ex.Message}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Передано некорректное значение 'user_id' = {user_id}");
                return null;
            }
        }

        public async Task<User?> GetUserAccauntByUserName(string user_name)
        {
            if (!string.IsNullOrWhiteSpace(user_name))
            {
                return await context.Users.SingleOrDefaultAsync(el => el.Name == user_name);
            }
            else
            {
                Console.WriteLine($"Передан некорректный параметр для поиска: user_name = {user_name}");
                return null;
            }
        }

        public async Task<User?> GetUserAccauntByUserPhoneNumber(string user_phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(user_phoneNumber))
            {
                return await context.Users.SingleOrDefaultAsync(el => el.PhoneNumber == user_phoneNumber);
            }
            else
            {
                Console.WriteLine($"Передан некорректный параметр для поиска: user_phoneNumber = {user_phoneNumber}");
                return null;
            }
        }

        public async Task<bool> UpdateUserAccaunt(int user_id, string new_user_name, string new_user_email, string new_user_password, string new_user_phoneNumber)
        {
            if(user_id > 0)
            {
                var user = await GetUserAccauntByUserId(user_id);
                if (user != null)
                {
                    var user_userName = await GetUserAccauntByUserName(new_user_name);
                    var user_email = await GetUserAccauntByUserEmail(new_user_email);
                    var user_phoneNumber = await GetUserAccauntByUserPhoneNumber(new_user_phoneNumber);

                    try
                    {
                        if (user_userName == null || user_userName.Id == user.Id)
                        {
                            user.Name = new_user_name;
                        }
                        else
                        {
                            throw new ArgumentException($"Заданное значение для userName уже занято - {new_user_name}");
                        }

                        if (user_email == null || user_email.Id == user.Id)
                        {
                            user.Email = new_user_email;
                        }
                        else
                        {
                            throw new ArgumentException($"Заданное значение для Email уже занято - {new_user_email}");
                        }

                        if (user_phoneNumber == null || user_phoneNumber.Id == user.Id)
                        {
                            user.PhoneNumber = new_user_phoneNumber;
                        }
                        else
                        {
                            throw new ArgumentException($"Заданное значение для PhoneNumber уже занято - {new_user_phoneNumber}");
                        }

                        await context.SaveChangesAsync();
                        return true;
                    } 
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Уже есть запись с одним из переданных значений: {ex.Message}");
                        return false;
                    }
                } 
                else
                {
                    Console.WriteLine($"Найти значение не удалось, user = null");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Найти значение не удалось, передано некорректное значение id = {user_id}");
                return false;
            }
        }
    }
}
