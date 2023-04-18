using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class PaymentMethodDto
    {
        public string Name { get; set; }

        public PaymentMethod ToModel()
        {
            return new PaymentMethod()
            {
                Name = this.Name,
            };
        }
    }
}
