using System.Reflection;

namespace ecommerce.Persistence.Utility
{
    public static partial class ReflectionHelper
    {
        /// <summary>
        /// Gets the value of the property
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="obj">Object to search for the property</param>
        /// <param name="propertyName">Name of the property to search</param>
        /// <returns>Returns the value of the property if it is found, otherwise throws an exception</returns>
        /// <exception cref="ArgumentException"></exception>
        public static object? GetValueofProperty(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo? propInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propInfo == null)
                throw new ArgumentException($"{propertyName} is not found in {type.Name}");

            return propInfo.GetValue(obj);
        }
    }
}
