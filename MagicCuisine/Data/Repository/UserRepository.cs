using System;
using Data.Models;
using Data.Repository.Contracts;

namespace Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CuisineDbContext context)
            : base(context)
        {

        }

        public User Get(string id)
        {
            return this.Context.Set<User>().Find(id);
        }
    }
}
