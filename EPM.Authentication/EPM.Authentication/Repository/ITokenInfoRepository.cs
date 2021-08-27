using EPM.Authentication.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Repository
{
    public interface ITokenInfoRepository
    {
        void Add(TokenInfo entity);
    }
}
