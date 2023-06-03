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
    }
}
