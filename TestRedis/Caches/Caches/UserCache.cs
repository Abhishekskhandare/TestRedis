using TestRedis.Caches;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using TestRedis.Services;
using Microsoft.Extensions.Caching.Distributed;
using TestRedis.EFModel;
using Newtonsoft.Json;
using TestRedis.ClientResponse;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

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
            try
            {
                string json = _cache.GetString(key) ?? string.Empty;
                if (string.IsNullOrEmpty(json))
                {
                    List<User> users = new List<User>();
                    users = _service.GetAllUsers(key);
                    if (!(users is null || users.Count == 0))
                    {
                        json = JsonConvert.SerializeObject(users, Formatting.Indented);
                    }
                    if (!string.IsNullOrEmpty(json))
                    {
                        _cache.SetString(key, json);
                    }
                }
                return json;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public Response AddUser(User user)
        {
            string key = "allUsers";
            Response response = new Response();
            response = _service.AddUser(user);
            if(response.Status == true && response.Message != string.Empty)
            {
                _cache.Remove(key);
            }
            return response;
        }

        public Response UpdateUser(User user)
        {
            string key = "allUsers";
            Response response = new Response();
            response = _service.UpdateUser(user);
            if (response.Status == true && response.Message != string.Empty)
            {
                _cache.Remove(key);
            }
            return response;
        }

        public string GetUserByEmailId(string email)
        {
            string key = $"UserDetail{email}";
            try
            {
                string json = _cache.GetString(key) ?? string.Empty;
                if (string.IsNullOrEmpty(json))
                {
                    User user = _service.GetUserByEmailId(email);
                    if (user is not null)
                    {
                        json = JsonConvert.SerializeObject(user, Formatting.Indented);
                    }
                    if (!string.IsNullOrEmpty(json))
                    {
                        _cache.SetString(key, json);
                    }
                }
                return json;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Response DeleteUserByEmailId(string email)
        {
            string key = "allUsers";
            string EmailKey = $"UserDetail{email}";
            Response response = new Response();
            response = _service.DeleteUserByEmailId(email);
            if (response.Status == true && response.Message != string.Empty)
            {
                _cache.Remove(key);
                _cache.Remove(EmailKey);
            }
            return response;
        }
    }
}
