using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IEmployerUserProfileRepository
    {
        public Task<bool> CreateEmployerUserProfile(int userId);
        public Task<EmployerProfile?> GetEmployerUserprofileByUserId(int? userId);
    }
}
