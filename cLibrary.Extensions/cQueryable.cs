﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using cLibrary.Models;
using cLibrary.Enums;
using cLibrary.Extensions;

namespace cLibrary.Helper
{    
    public static class cQueryableExtensions
    { 
        public static async Task<DataTable<T>> ApplyBaseFilterAsync<T>(this IQueryable<T> query, Filter filter)
        {
            var model = new DataTable<T>();
            model.TotalItems = filter.CountTotal ? query.Count() : 0;

            if (filter.SortField.IsNotNullOrEmpty())
            {
                var order = string.Format("{0} {1}", filter.SortField, filter.SortOrder.GetDescription());
                query = query.OrderBy(order);

                if (filter.PageSize.HasValue)
                {
                    query = query.Skip(filter.Skip);
                    query = query.Take(filter.PageSize.Value);
                }
            }

            model.Items = query is IAsyncDisposable ? await query.ToListAsync() : query.ToList();
            return model;
        }
        public static DataTable<T> ApplyBaseFilter<T>(this IQueryable<T> query, Filter filter)
        {
            return query.ApplyBaseFilterAsync(filter).Result;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string orderBy)
        {
            foreach (OrderByInfo orderByInfo in ParseOrderBy(orderBy))
                collection = ApplyOrderBy<T>(collection, orderByInfo);

            return collection;
        }

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> collection, OrderByInfo orderByInfo)
        {
            string[] props = orderByInfo.PropertyName.Split('.');
            Type type = typeof(T);

            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            string methodName = String.Empty;

            if (!orderByInfo.Initial && collection is IOrderedQueryable<T>)
            {
                if (orderByInfo.Direction == cSortDirection.Ascending)
                    methodName = "ThenBy";
                else
                    methodName = "ThenByDescending";
            }
            else
            {
                if (orderByInfo.Direction == cSortDirection.Ascending)
                    methodName = "OrderBy";
                else
                    methodName = "OrderByDescending";
            }

            //TODO: apply caching to the generic methodsinfos?
            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { collection, lambda });

        }

        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (String.IsNullOrEmpty(orderBy))
                yield break;

            string[] items = orderBy.Split(',');
            bool initial = true;
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                    throw new ArgumentException(String.Format("Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", item));

                string prop = pair[0].Trim();

                if (String.IsNullOrEmpty(prop))
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");

                cSortDirection dir = cSortDirection.Ascending;

                if (pair.Length == 2)
                    dir = ("desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase) ? cSortDirection.Descending : cSortDirection.Ascending);

                yield return new OrderByInfo() { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }

        }

        private class OrderByInfo
        {
            public string PropertyName { get; set; }
            public cSortDirection Direction { get; set; }
            public bool Initial { get; set; }
        }

    }
}
