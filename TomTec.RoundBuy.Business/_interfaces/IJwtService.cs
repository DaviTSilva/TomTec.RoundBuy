using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TomTec.RoundBuy.Business
{
    public interface IJwtService
    {
        public string Generate(int id, IEnumerable<Claim> claims);
        public JwtSecurityToken Verify(string jwtToken);
    }
}
