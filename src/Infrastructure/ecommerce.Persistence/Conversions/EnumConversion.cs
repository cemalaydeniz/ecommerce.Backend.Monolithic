using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ecommerce.Persistence.Conversions
{
    public class EnumConversion<T> : ValueConverter<T, string>
        where T : Enum
    {
        public EnumConversion()
            : base(
                  app => app.ToString(),
                  db => (T)Enum.Parse(typeof(T), db))
        { }
    }
}
