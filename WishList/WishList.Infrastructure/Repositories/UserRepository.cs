using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;

namespace WishList.Infrastructure.Repositories
{
    public interface IUserRepository
    {

    }
    public class UserRepository : IUserRepository
    {
        private readonly WishListContext _context;

        public UserRepository(WishListContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.Include(nameof(User.Wishes)).ToListAsync();

            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddWishAsync(int userId, Wish wish)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            user.Wishes.Add(wish);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Wish>> GetUserWishesAsync(int userId)
        {
            var user = await _context.Users.Include(nameof(User.Wishes)).FirstAsync(x => x.Id == userId);
            return user.Wishes.ToList();
        }

        public async Task<int> DeleteUser(int id)
        {
            var userWishes = await GetUserWishesAsync(id);
            await _context.Wishes.Where(wish => userWishes.Contains(wish)).ExecuteDeleteAsync();
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
