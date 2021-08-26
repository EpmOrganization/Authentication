using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(string id);
    }
}
