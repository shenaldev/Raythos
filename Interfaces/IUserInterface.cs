using Raythos.DTOs;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IUserInterface
    {
        ICollection<User> GetUsers(int skip, int take);

        User GetUser(long id);

        User CreateUser(User user);

        bool UpdateUser(long id, UpdateUserDto user);

        bool IsUserExists(string email);
        bool IsUserExists(long id);

        int GetTotalUsers();
    }
}
