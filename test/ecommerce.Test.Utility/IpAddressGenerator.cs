using System.Net;

namespace ecommerce.Test.Utility
{
    public static class IpAddressGenerator
    {
        public static IPAddress Generate()
        {
            byte[] bytes = new byte[4];
            Random.Shared.NextBytes(bytes);
            return new IPAddress(bytes);
        }
    }
}
