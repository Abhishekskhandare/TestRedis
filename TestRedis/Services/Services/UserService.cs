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

        public Response UpdateUser(User user)
        {
            Response Response = new Response();
            if (IsUserExist(user.Email))
            {
                User existinguser = _db.Users.FirstOrDefault(x => x.Email == user.Email);
                if (existinguser != null)
                {
                    Response.Message = (existinguser.IsActive == true) ? "user Updated Successfully"
                                       : "User were deleted previously, by updating you are gonna active previous user with updated information.";
                    existinguser = mapOldUserToNewUser(user, existinguser);
                    _db.Users.Update(existinguser);
                    _db.SaveChanges();
                    Response.Status = true;
                }
                else
                {
                    Response.Status = false;
                    Response.Message = "User not found";
                }
            }
            else
            {
                Response = AddUser(user);
                if(Response.Status == true)
                Response.Message = "User not found, instead of update  we added that user now.";
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
        private User mapOldUserToNewUser(User user, User existinguser)
        {
            existinguser.UpdatedDate = DateTime.Now;
            existinguser.DateOfBirth = user.DateOfBirth;
            existinguser.FirstName = user.FirstName;
            existinguser.LastName = user.LastName;
            existinguser.Gender = user.Gender;
            existinguser.PhoneNumber = user.PhoneNumber;
            existinguser.IsActive = true;
            return existinguser;
        }

        #endregion
    }
}
