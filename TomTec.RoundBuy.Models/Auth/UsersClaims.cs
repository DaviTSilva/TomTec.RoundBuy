using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class UsersClaims
    {
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int ClaimId { get; set; }

        public Claim Claim { get; set; }

        public UsersClaims()
        {
        }

        public UsersClaims(int userId, int claimId)
        {
            UserId = userId;
            ClaimId = claimId;
        }

        //public System.Security.Claims.Claim ToSecurityClaim()
        //{
        //    return new System.Security.Claims.Claim(ClaimTypes.Name, UserId.ToString());
        //}
    }
}
