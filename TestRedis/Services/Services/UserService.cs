using System.Linq;
using TestRedis.EFModel;
using TestRedis.ClientResponse;
using TestRedis.Services;
using Autofac.Core;
using Newtonsoft.Json;

namespace TestRedis.Services
{
    public class UserService : IUserService
    {
        private readonly FigmentDbContext _db = new FigmentDbContext();
        public UserService()
        {
        }

        public Response AddUser(User user)
        {
            Response Response = new Response();
            if (IsUserExist(user.Email))
            {
                Response.Status = false;
                Response.Message = "User is already exist";
            }
            else
            {
                Response.Status = true;
                try
                {
                    user.IsActive = true;
                    user.CreatedDate = DateTime.Now;
                    user.UpdatedDate = DateTime.Now;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    Response.Message = "User saved successfully!";
                }
                catch (Exception ex)
                {
                    Response.Status = false;
                    Response.Message = ex.Message;
                }
            }
            return Response;
        }

        public List<User> GetAllUsers(string key)
        {
            List<User> users = new List<User>();
            users = _db.Users.ToList();
            return users;
        }

        public User GetUserByEmailId(string email)
        {
            User user = new User();
            user = _db.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }
        #region private Methods
        private bool IsUserExist(string email)
        {
            return _db.Users.Any(x => x.Email == email);
        }
        #endregion
    }
}
