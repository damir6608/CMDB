using System.Linq.Expressions;
using System;
using System.Reflection;

namespace Shovel.WebAPI.DataAccess.Extensions
{
    public static class ReflectionExtensions
    {
        public static object? SetPropValue(this object? obj, object? value, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo? info = type.GetProperty(part, BindingFlags.Public
                                            | BindingFlags.Instance
                                            | BindingFlags.IgnoreCase);
                if (info == null) { return null; }

                info.SetValue(obj, value);
            }
            return obj;
        }

        public static T? SetPropValue<T>(this object obj, object value, string name)
        {
            object? retval = SetPropValue(obj, value, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static Expression<Func<T, bool>> CreateContainsExpression<T>(this string propertyName, string value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            var qwe = member.Member.ToString();
            var constant = Expression.Constant(value);
            var body = Expression.Call(member, "Contains", Type.EmptyTypes, constant, Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
