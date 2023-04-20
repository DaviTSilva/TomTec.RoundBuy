using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public interface IOrderService
    {
        public void CalculateValues(Order order);
    }
}
