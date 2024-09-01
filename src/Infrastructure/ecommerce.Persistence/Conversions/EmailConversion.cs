using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ecommerce.Persistence.Conversions
{
    public class EmailConversion : ValueConverter<string, string>
    {
        public EmailConversion() 
            : base(
                  app => app.ToLower(),
                  db => db)
        { }
    }
}
