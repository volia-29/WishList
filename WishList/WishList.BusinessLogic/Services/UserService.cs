using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;
using WishList.Services.Models;

namespace WishList.Services.Services
{
    public class UserService : IUserService
    {
        private readonly WishListContext _context;
        private readonly PasswordHasher passwordHasher;

        public UserService(WishListContext context)
        {
            _context = context;
            passwordHasher = new PasswordHasher();
        }

        public async Task AddAsync(CreateUserDto user)
        {
            var newUser = new User()
            {
                Name = user.Name,
                Password = new PasswordHasher().HashPassword(user.Password)
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> DeleteUser(int id)
        {
            var user = await GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException()
                {
                    Message = "User not found",
                };
            }

            await _context.Wishes.Where(wish => wish.UserId == id).ExecuteDeleteAsync();
            var result = await _context.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

            return result;
        }

        public async Task UpdateUser(int id, string newName)
        {
            var user = await GetByIdAsync(id);
            user.Name = newName;
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Name == loginRequest.UserName);
            
            if (user == null)
            {
                throw new AppException() { StatusCode = HttpStatusCode.Unauthorized, Message = "Invalid login or password" };
            }

            if (passwordHasher.VerifyHashedPassword(user.Password, loginRequest.Password) == PasswordVerificationResult.Failed)
            {
                throw new AppException() { StatusCode = HttpStatusCode.Unauthorized, Message = "Invalid login or password" };
            }

            return user;
        }
    }
}
