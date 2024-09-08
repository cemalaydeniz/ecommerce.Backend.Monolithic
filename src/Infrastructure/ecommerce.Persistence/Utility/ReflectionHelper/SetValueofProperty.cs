using System.Reflection;

namespace ecommerce.Persistence.Utility
{
    public static partial class ReflectionHelper
    {
        /// <summary>
        /// Set a new value for the property, whether the property's setter is private or not
        /// </summary>
        /// <param name="obj">Object to search for the property</param>
        /// <param name="propertyName">Name of the property to search</param>
        /// <param name="newValue">New value of the property to set</param>
        /// <returns>Returns TRUE if the value is changed, otherwise FALSE</returns>
        public static bool SetValueofProperty(object obj, string propertyName, object? newValue)
        {
            if (obj == null || string.IsNullOrEmpty(propertyName))
                return false;

            Type type = obj.GetType();
            var propInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propInfo == null)
                return false;

            var setter = propInfo.GetSetMethod(true);
            if (setter != null)
            {
                setter.Invoke(obj, new object?[] { newValue });
                return true;
            }
            else if (propInfo.DeclaringType != null)
            {
                var fieldInfo = propInfo.DeclaringType.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
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
