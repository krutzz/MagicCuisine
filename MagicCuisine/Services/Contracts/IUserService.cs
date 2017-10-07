using Data.Models;
using System;

namespace Services.Contracts
{
    public interface IUserService
    {
        User GetUser(string userId);
    }
}
