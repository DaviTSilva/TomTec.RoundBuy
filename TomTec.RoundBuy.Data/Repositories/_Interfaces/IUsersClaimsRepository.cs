using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Data
{
    public interface IUsersClaimsRepository
    {
        public void SignUserToClaim(int userId, int claimId);
        public void UnsignUserToClaim(int userId, int claimId);
    }
}
