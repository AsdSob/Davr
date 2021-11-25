using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Davr.Vash.Services
{
    public static class ToExpressionExtension
    {
        /// <summary>
        /// Filter array to "Where" Expression 
        /// </summary>
        /// <typeparam name="TModel">Entity type</typeparam>
        /// <param name="filters"> Filters array</param>
        /// <returns>returns Expression   (Where(retrun)) </returns>
        public static Expression<Func<TModel, bool>> FiltersToExpression<TModel>(this FieldFilter[] filters)
        {
            if (filters == null || !filters.Any())
                return null;

            var expression = GetFilterToExpression<TModel>(filters);
            return (Expression<Func<TModel, bool>>) expression;
        }

        //Convert Get response filter to expression
        private static Expression GetFilterToExpression<TModel>(FieldFilter[] filters)
        {
            var expParameter = Expression.Parameter(typeof(TModel), "x"); // x =>
            Expression expFinal = null;

            for (int i = 0; i < filters.Length; i++)
            {
                var expProperty = Expression.Property(expParameter, filters[i].f.ToLower());
                var expValue = GetConstantByMemberType(expProperty, filters[i].v);
                var oper = filters[i].v.Substring(1, filters[i].v.IndexOf(']') - 1);

                var expTemp = oper switch
                {
                    "eq" => Expression.Equal(expProperty, expValue),
                    ">" => Expression.GreaterThan(expProperty, expValue),
                    "<" => Expression.LessThan(expProperty, expValue),
                    "null" => GetNullExpression(expProperty),
                    "lk" => GetStringExpression(expProperty, expValue, "Contains"),
                    "st" => GetStringExpression(expProperty, expValue, "StartsWith"),
                    _ => null
                };

                expFinal = i == 0 ? expTemp : Expression.And(expFinal, expTemp);
            }
            
            // Lambda Expression
            return Expression.Lambda<Func<TModel, bool>>(expFinal, expParameter);
        }
        
        private static Expression GetStringExpression(MemberExpression member, ConstantExpression value, string oper)
        {
            var propertyInfo = value.Value.GetType();
            MethodInfo method2 = propertyInfo.GetMethod(oper, new Type[] { propertyInfo });
            var containsMethodExpression = Expression.Call(member, method2, value);

            return containsMethodExpression;
        }

        private static Type GetExpressionMemberType(MemberExpression member)
        {
            var propertyInfo = (PropertyInfo)member.Member;
            var propertyType = propertyInfo.PropertyType;

            return propertyType;
        }

        private static Expression GetNullExpression(MemberExpression member)
        {
            var value = GetConstantByMemberType(member, "");

            var x = Expression.Equal(member, value);
            return x;
        }


        private static ConstantExpression GetConstantByMemberType(MemberExpression expProperty, string fValue)
        {
            var strValue = $"{fValue.Substring(fValue.IndexOf(']') + 1)}";
            var propertyType = GetExpressionMemberType(expProperty);

            ConstantExpression constant = null;

            if (propertyType == typeof(string))
            {
                constant = Expression.Constant(strValue, typeof(string));
            }

            else if(propertyType == typeof(bool))
            {
                var value = strValue.Trim().ToLower();

                if (value == "true" || value == "1")
                    constant = Expression.Constant(true, typeof(bool));

                else if (value == "false" || value == "0" || String.IsNullOrEmpty(value)) 
                    constant = Expression.Constant(false, typeof(bool));
            }

            else if (propertyType == typeof(int))
            {
                var valueStr = Regex.Replace(strValue, "[^0-9]", "");
                var value = 0;

                if (!String.IsNullOrEmpty(valueStr))
                {
                    value = Convert.ToInt32(valueStr);
                }

                constant = Expression.Constant(value, typeof(int));
            }

            else if (propertyType == typeof(double))
            {
                var valueStr = Regex.Replace(strValue, "[^0-9.,]", "");
                double value = 0;

                if (!String.IsNullOrEmpty(valueStr))
                {
                    value = Convert.ToDouble(valueStr);
                }

                constant = Expression.Constant(value, typeof(double));
            }

            return constant;
        }


        /// <summary>
        /// Create sorting expression(OrderBy) for TModel 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="orderFiled"></param>
        /// <returns></returns>
        public static Expression<Func<TModel, object>> ToOrderExpression<TModel>(this string orderFiled)
        {
            if (String.IsNullOrEmpty(orderFiled)) return null;

            var expParameter = Expression.Parameter(typeof(TModel), "x");
            var expProperty = Expression.Property(expParameter, orderFiled.ToLower());

            var expConvert = Expression.Convert(expProperty, typeof(object));
            var expLambda = Expression.Lambda<Func<TModel, object>>(expConvert, expParameter);

            return expLambda;
        }

        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty, ListSortDirection sortOrder)
        //{
        //    var type = typeof(T);
        //    var property = type.GetProperty(sortProperty);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //    var typeArguments = new Type[] { type, property.PropertyType };
        //    var methodName = sortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
        //    var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression, Expression.Quote(orderByExp));
        //    return source.Provider.CreateQuery<T>(resultExp);
        //}
    }
}
