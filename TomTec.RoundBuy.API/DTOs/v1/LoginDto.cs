using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
