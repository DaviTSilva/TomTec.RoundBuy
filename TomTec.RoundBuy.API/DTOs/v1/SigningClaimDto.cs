using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class SigningClaimDto
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}
