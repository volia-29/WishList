using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;

namespace WishList.Infrastructure.Repositories
{
    public class UserRepository
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
    }
}
