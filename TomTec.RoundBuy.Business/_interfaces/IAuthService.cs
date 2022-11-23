using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public interface IAuthService
    {
        public User GetUserByLogin(string userNameOrEmail, string password);
    }
}
