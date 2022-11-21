using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity 
    {
        private readonly RoundBuyDbContext _dbContext;
        public SQLRepository(RoundBuyDbContext context)
        {
            _dbContext = context;
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public T Create(T entity)
        {
            entity.CreationDate = DateTime.UtcNow;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            _dbContext.Remove(Get(id));
            _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"the given id '{id}' of entity type '{typeof(T)}' was not found");
            _dbContext.SaveChanges();
            return entity;
        }

        public T Get(int id, params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"the given id '{id}' of entity type '{typeof(T)}' was not found");
            _dbContext.DetachLocal(entity, id);
            return entity;
        }

        public T Get(int id, params string[] includes)
        {
            var entity = _dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"the given id '{id}' of entity type '{typeof(T)}' was not found");
            
            return entity;
        }

        public IEnumerable<T> Get()
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>();
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}'");
            return entities;
        }

        public IEnumerable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>().IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}'");
            return entities;
        }

        public IEnumerable<T> Get(params string[] includes)
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>().IncludeMultiple(includes);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}'");
            return entities;
        }

        public IEnumerable<T> Get(Func<T, bool> query)
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>().Where(query);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}' with parameters");
            return entities;
        }

        public IEnumerable<T> Get(Func<T, bool> query, params Expression<Func<T, object>>[] includes)
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>().IncludeMultiple(includes).Where(query);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}' with parameters");
            return entities;
        }

        public IEnumerable<T> Get(Func<T, bool> query, params string[] includes)
        {
            var entities = (IEnumerable<T>)_dbContext.Set<T>().IncludeMultiple(includes).Where(query);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(T)}' with parameters");
            return entities;
        }

        public void Update(T entity)
        {
            _dbContext.DetachLocal(entity, entity.Id);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).Property(x => x.CreationDate).IsModified = false;
            _dbContext.SaveChanges();
        }

        
    }
}
