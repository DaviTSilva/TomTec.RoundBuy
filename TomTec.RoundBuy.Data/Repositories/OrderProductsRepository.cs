using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data.Repositories
{
    public class OrderProductsRepository
    {
        private readonly RoundBuyDbContext _dbContext;
        public OrderProductsRepository(RoundBuyDbContext context)
        {
            _dbContext = context;
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public void AddProductToOrder(int orderId, int productId)
        {
            var orderProducts = new OrderProducts(orderId, productId);
            _dbContext.OrderProducts.Add(orderProducts);
            _dbContext.SaveChanges();
        }

        public void RemoveProductFromOrder(int orderId, int productId)
        {
            var orderProducts = new OrderProducts(orderId, productId);
            _dbContext.OrderProducts.Remove(orderProducts);
            _dbContext.SaveChanges();
        }
    }
}
