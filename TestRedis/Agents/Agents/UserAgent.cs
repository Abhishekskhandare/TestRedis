using TestRedis.Agents;
using TestRedis.Caches;

namespace TestRedis.Agents
{
    public class UserAgent : IUserAgent
    {
        private readonly IUserCache _userCache;
        public UserAgent(IUserCache userCache)
        {
            _userCache = userCache;
        }
        public string GetAllUsers()
        {
            return _userCache.GetAllUsers();
        }
    }
}
