using ecommerce.Domain.Entities.Authentication;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class UserRoleGenerator
    {
        public static UserRole Generate()
        {
            return new UserRole()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Users = new HashSet<User>()
            };
        }
    }
}
