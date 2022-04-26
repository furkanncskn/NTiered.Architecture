using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Attribute
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue> (this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : System.Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

            if (att != null)
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
                    return true;
            }

            return false;
        }
    }
}
