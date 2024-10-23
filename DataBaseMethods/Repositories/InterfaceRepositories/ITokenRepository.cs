using Models;
using SiteEngine.CommandsAndHandlers.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface ITokenRepository
    {
        public Task<string> GenerateRefreshToken(int userId);
        public Task<string> GenerateJwtToken(int userId);
    }
}
