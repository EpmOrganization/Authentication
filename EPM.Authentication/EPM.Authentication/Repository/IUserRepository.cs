using EPM.Authentication.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.Authentication.Repository
{
    public interface IUserRepository
    {
        Task<User> GetEntityAsync(Expression<Func<User, bool>> predicate);

        void Update(User entity, Expression<Func<User, object>>[] updatedProperties);
    }
}
