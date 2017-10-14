using System;
using System.Linq;
using System.Linq.Expressions;
using WEE_WEB_API.Common.DynamicLINQ;
using System.Data.Entity.SqlServer;
using System.Reflection;

namespace WEE_WEB_API.Common.Datatables

{
    public static class DataTablesSearchAndSort
    {
        public static IQueryable<T> SearchForDataTables<T>(this IQueryable<T> query, DatatablesRequest dataTablesRequest)
        {
            string sSearch = dataTablesRequest.Search.Value;

            var predicate = PredicateBuilder.False<T>();
            if (!string.IsNullOrEmpty(sSearch))
            {
                Expression<Func<T, bool>> orCondiction = null;

                PropertyInfo[] props = typeof(T).GetProperties();

                foreach (var property in props)
                {
                    string columnName = property.Name;
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        Type columnType = query.DynamicType(z => z[columnName]);
                        if (columnType == typeof(string))
                        {
                            orCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName].ToLower().Contains(sSearch.ToLower()));
                        }
                        else if (columnType == typeof(int) || columnType == typeof(long) || columnType == typeof(decimal))
                        {
                            orCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName, sSearch, typeof(T)]);
                        }
                        else if (columnType == typeof(DateTime))
                        {
                            // orCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName, sSearch, typeof(T)]);
                        }
                        else if (columnType == typeof(bool))
                        {
                            try
                            {
                                var isTrue = bool.Parse(sSearch);
                                if (isTrue)
                                {
                                    orCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName]);
                                }
                                else
                                {
                                    orCondiction = query.DynamicWhereForPredicateBuilder(z => !z[columnName]);
                                }
                            }
                            catch { }
                        }
                        if (orCondiction != null)
                        {
                            predicate = predicate.Or(orCondiction);
                        }
                    }
                }
            }

            Expression<Func<T, bool>> andCondiction = null;
            foreach (var field in dataTablesRequest.Columns)
            {
                if (field.Search != null && field.Search.Value != null)
                {
                    string columnName = field.Data;
                    if (columnName != null)
                    {
                        Type columnType = query.DynamicType(z => z[columnName]);

                        if (columnType == typeof(string))
                        {
                            andCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName].ToLower().Contains(field.Search.Value));
                        }
                        else if (columnType == typeof(int) || columnType == typeof(long) || columnType == typeof(decimal))
                        {
                            andCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName, field.Search.Value, typeof(T)]);
                        }
                        else if (columnType == typeof(DateTime))
                        {
                            // orCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName, sSearch, typeof(T)]);
                        }
                        else if (columnType == typeof(bool))
                        {
                            try
                            {
                                var isTrue = bool.Parse(field.Search.Value);
                                if (isTrue)
                                {
                                    andCondiction = query.DynamicWhereForPredicateBuilder(z => z[columnName]);
                                }
                                else
                                {
                                    andCondiction = query.DynamicWhereForPredicateBuilder(z => !z[columnName]);
                                }
                            }
                            catch { }
                        }
                        if (andCondiction != null)
                        {
                            predicate = predicate.Or(andCondiction);
                        }
                    }
                }
            }
            if (predicate.ToString().Contains("Or") || predicate.ToString().Contains("And"))
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public static IQueryable Sort(this IQueryable query, DatatablesRequest dataTablesRequest)
        {
            for (int i = 0; i < dataTablesRequest.Order.Length; i++)
            {
                int columnNumber = dataTablesRequest.Order[i].Column;
                string columnName = dataTablesRequest.Columns[columnNumber].Data;
                if (columnName != null)
                {
                    var sortDirection = dataTablesRequest.Order[i].Dir;
                    if (sortDirection == DTOrderDir.ASC)
                    {
                        if (i == 0) { query = query.DynamicOrderBy(d => d[columnName]); }
                        else { query = query.DynamicThenBy(d => d[columnName]); }
                    }
                    else
                    {
                        if (i == 0) { query = query.DynamicOrderByDescending(d => d[columnName]); }
                        else { query = query.DynamicThenByDescending(d => d[columnName]); }
                    }
                }
            }
            return query;
        }
    }
}

