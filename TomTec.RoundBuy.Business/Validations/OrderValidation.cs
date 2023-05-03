using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business.Validations
{
    public static class OrderValidation
    {
        public static void Validate(this Order order)
        {
            if (order.DiscountPorcentage < 0 || order.DiscountPorcentage > 1)
                throw new InvalidOperationException($"[INVALID \"{nameof(Order)}\"! WRONG VALUE FOR FIELD \"{nameof(Order.DiscountPorcentage)}\"]: Porcentage can not be lower than 0% or higher than 100%!");

        }
    }
}
