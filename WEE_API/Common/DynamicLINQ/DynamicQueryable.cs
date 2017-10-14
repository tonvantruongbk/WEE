using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace WEE_WEB_API.Common.DynamicLINQ
{
    public static partial class DynamicQueryable
    {
        private static bool Any(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.Execute<bool>(Expression.Call(typeof(Queryable),
                "Any",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static int Count(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.Execute<int>(Expression.Call(typeof(Queryable),
                "Count",
                new[] { GetElementType(source) },
                source.Expression, Expression.Quote(expression)));
        }

        private static IQueryable Where(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                "Where",
                new[] { GetElementType(source) },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable OrderBy(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                "OrderBy",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable ThenBy(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                "ThenBy",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable OrderByDescending(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                "OrderByDescending",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable ThenByDescending(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                "ThenByDescending",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable<T> Where<T>(IQueryable<T> source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable),
                "Where",
                new[] { GetElementType(source) },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static dynamic First(IQueryable<dynamic> source, LambdaExpression expression)
        {
            return source.Provider.Execute<dynamic>(Expression.Call(typeof(Queryable),
                "First",
                new[] { GetElementType(source) },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static dynamic FirstOrDefault(IQueryable<dynamic> source, LambdaExpression expression)
        {
            return source.Provider.Execute<dynamic>(Expression.Call(typeof(Queryable),
                "FirstOrDefault",
                new[] { GetElementType(source) },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable<TResult> Select<TResult>(IQueryable source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery<TResult>(Expression.Call(typeof(Queryable),
                "Select",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable<dynamic> OrderBy(IQueryable<dynamic> source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery<dynamic>(Expression.Call(typeof(Queryable),
                "OrderBy",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static IQueryable<dynamic> OrderByDescending(IQueryable<dynamic> source, LambdaExpression expression)
        {
            return source.Provider.CreateQuery<dynamic>(Expression.Call(typeof(Queryable),
                "OrderByDescending",
                new[] { GetElementType(source), expression.Body.Type },
                source.Expression,
                Expression.Quote(expression)));
        }

        private static LambdaExpression GetSelectorExpression<TResult>(IQueryable source, Func<dynamic, TResult> selector)
        {
            ParameterExpression parameterExpression = Expression.Parameter(GetElementType(source), selector.Method.GetParameters()[0].Name);
            TResult result = selector(new DynamicExpressionBuilder(parameterExpression));

            if (result == null)
            {
                throw new ArgumentException("Unable to translate expression");
            }

            Expression body = null;
            var properties = typeof(TResult).GetProperties();
            if (properties.Any() && !typeof(TResult).IsPrimitive && typeof(TResult) != typeof(string))
            {
                var members = from property in properties
                              let builder = (DynamicExpressionBuilder)property.GetValue(result, null)
                              select new
                              {
                                  Expression = Expression.Convert(builder.Expression, property.PropertyType),
                                  Member = property
                              };

                body = Expression.New(typeof(TResult).GetConstructors()[0],
                    members.Select(a => a.Expression),
                    members.Select(a => a.Member));
            }
            else
            {
                body = (result as DynamicExpressionBuilder).Expression;
            }

            return Expression.Lambda(body, parameterExpression);
        }

        private static LambdaExpression GetExpression(IQueryable source, Func<dynamic, dynamic> expressionBuilder)
        {
            ParameterExpression parameterExpression = Expression.Parameter(GetElementType(source), expressionBuilder.Method.GetParameters()[0].Name);
            DynamicExpressionBuilder dynamicExpression = expressionBuilder(new DynamicExpressionBuilder(parameterExpression));
            Expression body = dynamicExpression.Expression;
            return Expression.Lambda(body, parameterExpression);
        }

        // Walk until we get to the first non object element type
        private static Type GetElementType(IQueryable source)
        {
            Expression expr = source.Expression;
            Type elementType = source.ElementType;
            while (expr.NodeType == ExpressionType.Call &&
                   elementType == typeof(object))
            {
                var call = (MethodCallExpression)expr;
                expr = call.Arguments.First();
                elementType = expr.Type.GetGenericArguments().First();
            }

            return elementType;
        }
    }

    /// <summary>
    /// Public non-generic methods
    /// </summary>
    public static partial class DynamicQueryable
    {
        public static bool DynamicAny(this IQueryable source, Func<dynamic, dynamic> predicate)
        {
            return Any(source, GetExpression(source, predicate));
        }

        public static int DynamicCount(this IQueryable source, Func<dynamic, dynamic> predicate)
        {
            return Count(source, GetExpression(source, predicate));
        }

        public static IQueryable DynamicOrderBy(this IQueryable source, Func<dynamic, dynamic> selector)
        {
            return OrderBy(source, GetExpression(source, selector));
        }

        public static IQueryable DynamicThenBy(this IQueryable source, Func<dynamic, dynamic> selector)
        {
            return ThenBy(source, GetExpression(source, selector));
        }

        public static IQueryable DynamicOrderByDescending(this IQueryable source, Func<dynamic, dynamic> selector)
        {
            return OrderByDescending(source, GetExpression(source, selector));
        }

        public static IQueryable DynamicThenByDescending(this IQueryable source, Func<dynamic, dynamic> selector)
        {
            return ThenByDescending(source, GetExpression(source, selector));
        }

        public static IQueryable<TResult> DynamicSelect<TResult>(this IQueryable source, Func<dynamic, TResult> selector)
        {
            return Select<TResult>(source, GetSelectorExpression(source, selector));
        }
        public static IQueryable<TResult> DynamicSelect<TResult>(this IQueryable source, Func<dynamic, dynamic> selector)
        {
            return Select<TResult>(source, GetSelectorExpression(source, selector));
        }

        public static IQueryable SelectDynamic(this IQueryable source, IEnumerable<string> fieldNames)
        {
            Dictionary<string, PropertyInfo> sourceProperties = fieldNames.ToDictionary(name => name, name => source.ElementType.GetProperty(name));
            Type dynamicType = LinqRuntimeTypeBuilder.GetDynamicType(sourceProperties.Values);

            ParameterExpression sourceItem = Expression.Parameter(source.ElementType, "t");
            IEnumerable<MemberBinding> bindings = dynamicType.GetFields().Select(p => Expression.Bind(p, Expression.Property(sourceItem, sourceProperties[p.Name]))).OfType<MemberBinding>();

            Expression selector = Expression.Lambda(Expression.MemberInit(
                Expression.New(dynamicType.GetConstructor(Type.EmptyTypes)), bindings), sourceItem);

            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Select", new[] { source.ElementType, dynamicType },
                Expression.Constant(source), selector));
        }

        public static Expression CreateNewStatement<T>(this IQueryable source, string fields)
        {
            var sourceType = source.ElementType;
            // input parameter "o"
            var xParameter = Expression.Parameter(sourceType, "o");
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

                        var value1 = Expression.Parameter(sourceType, value);
                        var body = Expression.Bind(   mi ,
                            value1);
                        return body;
                        // set value "Field1 = o.Field1"
                        //return Expression.Bind(mi, xOriginal);
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
    public static class LinqRuntimeTypeBuilder
    {
        private static AssemblyName assemblyName = new AssemblyName { Name = "DynamicLinqTypes" };
        private static ModuleBuilder moduleBuilder;
        private static Dictionary<string, Type> builtTypes = new Dictionary<string, Type>();

        static LinqRuntimeTypeBuilder()
        {
            moduleBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule(assemblyName.Name);
        }

        private static string GetTypeKey(Dictionary<string, Type> fields)
        {
            //TODO: optimize the type caching -- if fields are simply reordered, that doesn't mean that they're actually different types, so this needs to be smarter
            string key = string.Empty;
            foreach (var field in fields)
                key += field.Key + ";" + field.Value.Name + ";";

            return key;
        }

        public static Type GetDynamicType(Dictionary<string, Type> fields)
        {
            if (null == fields)
                throw new ArgumentNullException(nameof(fields));
            if (0 == fields.Count)
                throw new ArgumentOutOfRangeException(nameof(fields), "fields must have at least 1 field definition");

            try
            {
                Monitor.Enter(builtTypes);
                string className = GetTypeKey(fields);

                if (builtTypes.ContainsKey(className))
                    return builtTypes[className];

                TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

                foreach (var field in fields)
                    typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);

                builtTypes[className] = typeBuilder.CreateType();

                return builtTypes[className];
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Monitor.Exit(builtTypes);
            }
            return null;
        }


        private static string GetTypeKey(IEnumerable<PropertyInfo> fields)
        {
            return GetTypeKey(fields.ToDictionary(f => f.Name, f => f.PropertyType));
        }

        public static Type GetDynamicType(IEnumerable<PropertyInfo> fields)
        {
            return GetDynamicType(fields.ToDictionary(f => f.Name, f => f.PropertyType));
        }
    }


    /// <summary>
    /// Public generic methods (IQ&gt;dynamic&lt;)
    /// </summary>
    public static partial class DynamicQueryable
    {
        public static Type DynamicType<T>(this IQueryable<T> source, Func<dynamic, dynamic> predicate)
        {
            return GetExpression(source, predicate).ReturnType;
        }

        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> source, Func<dynamic, dynamic> predicate)
        {
            return Where(source, GetExpression(source, predicate));
        }
        public static Expression<Func<T, bool>> DynamicWhereForPredicateBuilder<T>(this IQueryable<T> source, Func<dynamic, dynamic> predicate)
        {
            return GetFuncTbool<T>(source, predicate);
        }


        private static Expression<Func<T, bool>> GetFuncTbool<T>(IQueryable source, Func<dynamic, dynamic> expressionBuilder)
        {
            ParameterExpression parameterExpression = Expression.Parameter(GetElementType(source), expressionBuilder.Method.GetParameters()[0].Name);
            DynamicExpressionBuilder dynamicExpression = expressionBuilder(new DynamicExpressionBuilder(parameterExpression));

            Expression body = dynamicExpression.Expression;
            return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        }


        public static IQueryable<dynamic> DynamicOrderBy(this IQueryable<dynamic> source, Func<dynamic, dynamic> selector)
        {
            return OrderBy(source, GetExpression(source, selector));
        }

        public static IQueryable<dynamic> DynamicOrderByDescending(this IQueryable<dynamic> source, Func<dynamic, dynamic> selector)
        {
            return OrderByDescending(source, GetExpression(source, selector));
        }

        public static dynamic DynamicFirst(this IQueryable<dynamic> source, Func<dynamic, dynamic> predicate)
        {
            return First(source, GetExpression(source, predicate));
        }

        public static dynamic DynamicFirstOrDefault(this IQueryable<dynamic> source, Func<dynamic, dynamic> predicate)
        {
            return FirstOrDefault(source, GetExpression(source, predicate));
        }
    }
}
