using ListerAPI.Models;

namespace ListerAPI.Services
{
    public class UserService : IUserService
    {

        public UserService() 
        {
            users = new List<User>() { new User { UserName = "admin" } };
        }

        public List<User> users;

        public bool Authenticate(string username)
        {
            return users.FindAll(u => u.UserName.Equals(username)).Count > 0;
        }

        public List<User> GetUsers()
        {
            return users;
        }
    }
}
