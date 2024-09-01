using System.Reflection;

namespace ecommerce.Persistence.Utility
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Assigns a new value to the property that has a private setter
        /// </summary>
        /// <param name="obj">Property to change</param>
        /// <param name="nameofProperty">Name of the property</param>
        /// <param name="newValue">New value of the property</param>
        /// <returns>Returns TRUE if the value is changed, otherwise FALSE</returns>
        public static bool AssignValueToPrivateSetterProperty(object obj, string nameofProperty, object newValue)
        {
            if (obj == null || string.IsNullOrEmpty(nameofProperty))
                return false;

            Type type = obj.GetType();
            var propInfo = type.GetProperty(nameofProperty, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propInfo == null)
                return false;

            var setter = propInfo.GetSetMethod(true);
            if (setter != null)
            {
                setter.Invoke(obj, new object[] { newValue });
                return true;
            }
            else if (propInfo.DeclaringType != null)
            {
                var fieldInfo = propInfo.DeclaringType.GetField($"<{nameofProperty}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(obj, newValue);
                    return true;
                }
            }

            return false;
        }
    }
}
