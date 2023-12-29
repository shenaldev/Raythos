using Raythos.DTOs;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IUserInterface
    {
        ICollection<User> GetUsers(int skip, int take);

        User GetUser(long id);

        User GetUser(string email);

        User CreateUser(User user);

        bool UpdateUser(long id, UpdateUserDto user);

        bool DeleteUser(long id);

        bool IsUserExists(string email);

        bool IsUserExists(long id);

        int GetTotalUsers();

        long GetUserID(string email);
    }
}
