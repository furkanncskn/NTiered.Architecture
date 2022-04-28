using System;
using System.Linq;
using System.Reflection;

namespace Entity.Attribute
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue> (
                this Type type, 
                Func<TAttribute, TValue> valueSelector
            ) where TAttribute : System.Attribute
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }

            return default(TValue);
        }

        public static bool IsThereAnAttribute(this PropertyInfo property, Type type)
        {
            object[] attrs = property.GetCustomAttributes(true);

            foreach (object attr in attrs)
            {
                if (attr.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
