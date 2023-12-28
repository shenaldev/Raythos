using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get All Users With Pagination
        public ICollection<User> GetUsers(int skip, int take = 15)
        {
            return _context.Users.Skip(skip).Take(take).OrderBy(u => u.Id).ToList();
        }

        //Get User By Id
        public User GetUser(long id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        //Add New User
        public User CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        //Update User
        public bool UpdateUser(long id, UpdateUserDto user)
        {
            User? userData = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userData == null)
            {
                return false;
            }

            userData.FName = user.FName;
            userData.LName = user.LName;
            userData.ContactNo = user.ContactNo;
            userData.IsAdmin = user.IsAdmin;
            userData.UpdatedAt = DateTime.Now;

            var saved = _context.SaveChanges();
            return saved > 0;
        }

        //Delete User
        public bool DeleteUser(long id)
        {
            User? userData = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userData == null)
            {
                return false;
            }

            _context.Users.Remove(userData);
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        //Check If User Exists
        public bool IsUserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsUserExists(long id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        //Get Total Users
        public int GetTotalUsers()
        {
            return _context.Users.Count();
        }
    }
}
