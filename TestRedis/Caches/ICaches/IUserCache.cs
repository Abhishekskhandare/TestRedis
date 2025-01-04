using TestRedis.EFModel;
using TestRedis.ClientResponse;

namespace TestRedis.Caches
{
    public interface IUserCache
    {
        public Response AddUser(User user);
        public string GetAllUsers();
        public string GetUserByEmailId(string email);
    }
}
