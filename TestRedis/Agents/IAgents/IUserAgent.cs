using TestRedis.ClientResponse;
using TestRedis.EFModel;

namespace TestRedis.Agents;

public interface IUserAgent
{
    public Response AddUser(UserResponse user);
    public string GetAllUsers();
    public string GetUserByEmailId(string email);
}
