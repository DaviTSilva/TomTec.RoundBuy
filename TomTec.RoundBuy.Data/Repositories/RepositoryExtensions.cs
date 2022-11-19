using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TomTec.RoundBuy.Data
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// can be only used with simple 1:1 or 1:N relationships
        /// </summary>
        /// <typeparam name="T">BaseEntity implementation</typeparam>
        /// <param name="query">original query</param>
        /// <param name="includes">includes by property reference</param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        /// <summary>
        /// recomended to use with N:N complex relationships
        /// </summary>
        /// <typeparam name="T">BaseEntity implementation</typeparam>
        /// <param name="query">original query</param>
        /// <param name="includes">includes by class' name in string</param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
