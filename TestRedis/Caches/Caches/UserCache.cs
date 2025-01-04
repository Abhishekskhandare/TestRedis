using TestRedis.Caches;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using TestRedis.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace TestRedis.Caches
{
    public class UserCache : IUserCache
    {
        private readonly IUserService _service;
        private readonly IDistributedCache _cache;

        public UserCache(IUserService service, IDistributedCache cache)
        {
            _service = service;
            _cache = cache;
        }
        
        public string GetAllUsers()
        {
            string key = "allUsers";
            string? allUsers = _cache.GetString(key);
            if (string.IsNullOrEmpty(allUsers))
            {
                allUsers = _service.GetAllUsers(key);
                if (!string.IsNullOrEmpty(allUsers))
                {
                    _cache.SetString(key, allUsers);
                }
            }
            return allUsers;
        }

        //public string AddUser(string user)
        //{
        //    string key = "allUsers";
        //    string? allUsers = GetAllUsers();
        //    allUsers += allUsers+ user;
        //    if (string.IsNullOrEmpty(allUsers))
        //    {
        //        allUsers = _service.GetAllUsers(key);
        //        if (!string.IsNullOrEmpty(allUsers))
        //        {
        //            _cache.SetString(key, allUsers);
        //        }
        //    }
        //    return allUsers;
        //}

    }
}
