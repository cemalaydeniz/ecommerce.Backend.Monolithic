using System.Text;

namespace ecommerce.Test.Utility
{
    public static class StringGenerator
    {
        private static readonly char[] AsciiChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        public static string Generate(int length = 5)
        {
            if (length <= 0)
                return string.Empty;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; ++i)
            {
                stringBuilder.Append(AsciiChars[Random.Shared.Next(0, AsciiChars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
