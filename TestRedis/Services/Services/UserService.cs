using TestRedis.Services;

namespace TestRedis.Services
{
    public class UserService : IUserService
    {
        public string GetAllUsers(string key)
        {
            return "abhishek, shailendra, ganesh, maroti";
        }
    }
}
