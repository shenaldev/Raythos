using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IUserInterface
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(string email);
    }
}
