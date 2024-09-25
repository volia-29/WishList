using Microsoft.EntityFrameworkCore;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;

namespace WishList.Services.Services
{
    public class UserService : IUserService
    {
        private readonly WishListContext _context;

        public UserService(WishListContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CreateUserDto user)
        {
            var newUser = new User()
            {
                Name = user.Name,
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
    }
}
