using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface ICommonProfileData
    {
        public Task<IDictionary<string, object?>> ReturnUserProfilesByUserId(int userId);
    }
}
