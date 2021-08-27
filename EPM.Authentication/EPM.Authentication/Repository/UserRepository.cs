using EPM.Authentication.Data.Context;
using EPM.Authentication.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.Authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetEntityAsync(Expression<Func<User, bool>> predicate)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(predicate);
        }

        public void Update(User entity, Expression<Func<User, object>>[] updatedProperties)
        {
            _appDbContext.Set<User>().Attach(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    _appDbContext.Entry(entity).Property(property).IsModified = true;
                }
            }
        }
    }
}
