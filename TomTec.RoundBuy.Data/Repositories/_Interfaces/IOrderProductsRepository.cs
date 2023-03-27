using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Data.Repositories
{
    interface IOrderProductsRepository
    {
        public void AddProductToOrder(int orderId, int productId);
        public void RemoveProductFromOrder(int orderId, int productId);
    }
}
