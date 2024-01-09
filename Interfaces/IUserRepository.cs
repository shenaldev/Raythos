using Raythos.DTOs;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers(int skip, int take);

        Task<User?> GetUser(long id);

        Task<User?> GetUser(string email);

        Task<User> CreateUser(User user);

        Task<User?> UpdateUser(long id, UpdateUserDto user);

        Task<bool> DeleteUser(long id);

        Task<bool> IsUserExists(string email);

        Task<bool> IsUserExists(long id);

        Task<int> GetTotalUsers();

        Task<long> GetUserID(string email);
    }
}
