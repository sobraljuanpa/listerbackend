using ListerAPI.Models;

namespace ListerAPI.Services
{
    public interface IUserService
    {
        bool Authenticate(string username);
        List<User> GetUsers();

    }
}
