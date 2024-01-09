using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get All Users With Pagination
        public async Task<ICollection<User>> GetUsers(int skip, int take = 15)
        {
            return await _context.Users.Skip(skip).Take(take).OrderBy(u => u.Id).ToListAsync();
        }

        //Get User By Id
        public async Task<User?> GetUser(long id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        //Get User By Email
        public async Task<User?> GetUser(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        //Add New User
        public async Task<User> CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //Update User
        public async Task<User?> UpdateUser(long id, UpdateUserDto user)
        {
            var userData = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (userData == null)
            {
                return null;
            }

            userData.FName = user.FName;
            userData.LName = user.LName;
            userData.ContactNo = user.ContactNo;
            userData.IsAdmin = user.IsAdmin;
            userData.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return userData;
        }

        //Delete User
        public async Task<bool> DeleteUser(long id)
        {
            var userData = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (userData == null)
            {
                return false;
            }

            _context.Users.Remove(userData);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        //Check If User Exists
        public async Task<bool> IsUserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserExists(long id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        //Get Total Users
        public async Task<int> GetTotalUsers()
        {
            return await _context.Users.CountAsync();
        }

        //Get User ID
        public async Task<long> GetUserID(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }
    }
}
