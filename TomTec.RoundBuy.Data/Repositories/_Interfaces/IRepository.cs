using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        public T Create(T entity);

        public void Delete(int id);

        public T Get(int id);

        public T Get(int id, params Expression<Func<T, object>>[] includes);

        public T Get(int id, params string[] includes);

        public IEnumerable<T> Get();

        public IEnumerable<T> Get(params Expression<Func<T, object>>[] includes);

        public IEnumerable<T> Get(params string[] includes);

        public IEnumerable<T> Get(Func<T, bool> query);

        public IEnumerable<T> Get(Func<T, bool> query, params Expression<Func<T, object>>[] includes);

        public IEnumerable<T> Get(Func<T, bool> query, params string[] includes);

        public void Update(T entity);
    }
}
