using TestRedis.Agents;
using TestRedis.Caches;
using TestRedis.EFModel;
using TestRedis.ClientResponse;
using TestRedis.Services;

namespace TestRedis.Agents
{
    public class UserAgent : IUserAgent
    {
        private readonly IUserCache _userCache;
        private readonly IUserService _userService;
        public UserAgent(IUserCache userCache, IUserService userService)
        {
            _userCache = userCache;
            _userService = userService;
        }

        public Response AddUser(UserResponse userResponce)
        {
            User user = UserResponseToModel(userResponce);
            Response responce = new Response();
            responce = CheckValidDataForUser(user);
            if (responce.Status == true && !string.IsNullOrEmpty(responce.Message))
                return responce;
            else
                return _userCache.AddUser(user);
        }

    

        public string GetAllUsers()
        {
            return _userCache.GetAllUsers();
        }


        public string GetUserByEmailId(string email)
        {
            return _userCache.GetUserByEmailId(email);
        }



        #region private methods
        private User UserResponseToModel(UserResponse userResponce)
        {
            User user = new User();
            user.FirstName = userResponce.FirstName;
            user.LastName = userResponce.LastName;
            user.Email = userResponce.Email;
            user.Gender = userResponce.Gender;
            user.DateOfBirth = DateOnly.Parse(userResponce.DateOfBirth);

            return user;
        }
        private Response CheckValidDataForUser(User user)
        {
            Response responce = new Response();
            responce.Status = true;
            responce.Message = string.Empty;

            responce.Message = (user is null) ? "input is empty"
                              : (string.IsNullOrEmpty(user.Email)) ? "email is required"
                              : responce.Message;

            if (!string.IsNullOrEmpty(responce.Message))
            {
                responce.Status = false;
            }

            return responce;
        }

        #endregion

    }
}
