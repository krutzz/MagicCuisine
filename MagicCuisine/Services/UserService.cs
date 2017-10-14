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
            if (userRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.userRepository = userRepository;
        }

        public User GetUser(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException();
            }

            return this.userRepository.Get(userId);
        }
    }
}
