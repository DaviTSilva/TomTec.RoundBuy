using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        public AuthService(ILogger<AuthService> logger, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUserByLogin(string userNameOrEmail, string password) 
        {
            try
            {
                var user = _userRepository.Get(
                                    u => u.Email.Equals(userNameOrEmail) || u.UserName.Equals(userNameOrEmail),
                                    $"{nameof(User.UsersClaims)}.{nameof(UsersClaims.Claim)}"
                                ).FirstOrDefault();

                if (user == null)
                    throw new UnauthorizedAccessException("Invalid Credentials!");
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    throw new UnauthorizedAccessException("Invalid Credentials!");

                return user;
            }
            catch (KeyNotFoundException)
            {
                throw new UnauthorizedAccessException("Invalid Credentials!");
            }
            catch (Exception)
            {
                throw;
            }          
        }
    }
}
