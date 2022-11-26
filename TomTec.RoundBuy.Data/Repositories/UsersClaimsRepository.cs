using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public class UsersClaimsRepository : IUsersClaimsRepository
    {
        private readonly RoundBuyDbContext _dbContext;
        public UsersClaimsRepository(RoundBuyDbContext context)
        {
            _dbContext = context;
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public void SignUserToClaim(int userId, int claimId)
        {
            var userClaim = new UsersClaims(userId, claimId);
            _dbContext.UsersClaims.Add(userClaim);
            _dbContext.SaveChanges();
        }

        public void UnsignUserToClaim(int userId, int claimId)
        {
            var userClaim = new UsersClaims(userId, claimId);
            _dbContext.UsersClaims.Remove(userClaim);
            _dbContext.SaveChanges();
        }
    }
}
