using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public interface IOrderProductsRepository
    {
        public void AddProductToOrder(int orderId, int productId);
        public void RemoveProductFromOrder(int orderId, int productId);
        public IEnumerable<OrderProducts> Get();
        public IEnumerable<OrderProducts> Get(params Expression<Func<OrderProducts, object>>[] includes);
        public IEnumerable<OrderProducts> Get(params string[] includes);
        public IEnumerable<OrderProducts> GetByProductId(int productId);
        public IEnumerable<OrderProducts> GetByProductId(int productId, params Expression<Func<OrderProducts, object>>[] includes);
        public IEnumerable<OrderProducts> GetByProductId(int productId, params string[] includes);
    }
}
