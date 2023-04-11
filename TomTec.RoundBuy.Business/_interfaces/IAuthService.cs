using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public interface IAuthService
    {
        public User GetUserByLogin(string userNameOrEmail, string password);
        public IEnumerable<System.Security.Claims.Claim> GenerateSecurityClaims(User user);
        public User GetCurrentUser(ClaimsPrincipal CurrentUser);
    }
}
