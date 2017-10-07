using Data.Models;

namespace Data.Repository.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string id);
    }
}
