using TestRedis.EFModel;
using TestRedis.ClientResponse;

namespace TestRedis.Services
{
    public interface IUserService
    {
        public Response AddUser(User user);
        public List<User> GetAllUsers(string key);
        public User GetUserByEmailId(string email);

    }
}
