using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public class OrderProductsRepository : IOrderProductsRepository
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

        public IEnumerable<OrderProducts> Get()
        {
            var orderProducts = (IEnumerable<OrderProducts>)_dbContext.OrderProducts;
            if (orderProducts == null || orderProducts.Count() <= 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");

            return orderProducts;
        }

        public IEnumerable<OrderProducts> Get(params Expression<Func<OrderProducts, object>>[] includes)
        {
            var entities = (IEnumerable<OrderProducts>)_dbContext.Set<OrderProducts>().IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");
            return entities;
        }

        public IEnumerable<OrderProducts> Get(params string[] includes)
        {
            var entities = (IEnumerable<OrderProducts>)_dbContext.Set<OrderProducts>().IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");
            return entities;
        }

        public IEnumerable<OrderProducts> GetByProductId(int productId)
        {
            var orderProducts = (IEnumerable<OrderProducts>)_dbContext.OrderProducts.Where(op => op.ProductId == productId);
            if (orderProducts == null || orderProducts.Count() <= 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");

            return orderProducts;
        }

        public IEnumerable<OrderProducts> GetByProductId(int productId, params Expression<Func<OrderProducts, object>>[] includes)
        {
            var entities = (IEnumerable<OrderProducts>)_dbContext.Set<OrderProducts>().Where(op => op.ProductId == productId).IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");
            return entities;
        }

        public IEnumerable<OrderProducts> GetByProductId(int productId, params string[] includes)
        {
            var entities = (IEnumerable<OrderProducts>)_dbContext.Set<OrderProducts>().Where(op => op.ProductId == productId).IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(OrderProducts)}'");
            return entities;
        }
    }
}
