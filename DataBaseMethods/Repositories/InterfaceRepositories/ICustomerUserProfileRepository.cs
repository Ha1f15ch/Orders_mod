using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface ICustomerUserProfileRepository
    {
        public Task<bool> CreateCustomerUserProfile(int userId);
        public Task<CustomerProfile?> GetCustomerUserProfileByUserId(int userId);
    }
}
