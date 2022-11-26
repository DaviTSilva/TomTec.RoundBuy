using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class OfficialIdentificationTypeDto
    {
        public string Name { get; set; }

        public OfficialIdentificationType ToModel()
        {
            return new OfficialIdentificationType() 
            {
                Name = this.Name,
            };
        }
    }
}
