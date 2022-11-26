using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class ClaimDto
    {
        public string Name { get; set; }

        public Claim ToModel()
        {
            return new Claim
            {
                Name = this.Name
            };
        }
    }
}
