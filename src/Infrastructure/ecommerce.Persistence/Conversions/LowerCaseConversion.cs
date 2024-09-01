using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ecommerce.Persistence.Conversions
{
    public class LowerCaseConversion : ValueConverter<string, string>
    {
        public LowerCaseConversion()
            : base(
                  app => app.ToLower(),
                  db => db)
        { }
    }
}
