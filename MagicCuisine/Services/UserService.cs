using System;
using Data.Models;
using Services.Contracts;
using Data.Repository.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUser(string userId)
        {
            return this.userRepository.Get(userId);
        }
    }
}
