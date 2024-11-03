using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceForServices
{
    public interface IDecodeAndVerifieCookieService
    {
        public Task<int> GetUserIdFromCookie(string tokenValue);
    }
}
