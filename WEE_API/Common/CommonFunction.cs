using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WEE_API.Common
{
    public static class CommonFunction
    {
        public static Expression<Func<T, object>> CreateNewStatement<T>(string fields)
        {
            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");

            // new statement "new T()"
            var xNew = Expression.New(typeof(T));

            // create initializers
            var bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o =>
                    {
                        var label = o.Split('|')[0];
                        var value = o.Split('|')[1];
                        // property "Field1"
                        var mi = typeof(T).GetProperty(label);

                        // original value "o.Field1"
                        var xOriginal = Expression.Property(xParameter, value);

                        // set value "Field1 = o.Field1"
                        return Expression.Bind(mi, xOriginal);
                    }
                );

            // initialization "new T { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new T { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<T, object>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda;
        }

    }
}