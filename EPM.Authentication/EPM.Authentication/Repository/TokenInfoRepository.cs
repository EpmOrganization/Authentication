using EPM.Authentication.Data.Context;
using EPM.Authentication.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Repository
{
    public class TokenInfoRepository : ITokenInfoRepository
    {
        private readonly AppDbContext _appDbContext;

        public TokenInfoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(TokenInfo entity)
        {
            _appDbContext.TokenInfos.Add(entity);
        }
    }
}
