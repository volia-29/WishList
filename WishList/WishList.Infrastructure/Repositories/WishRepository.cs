using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;

namespace WishList.Infrastructure.Repositories
{
    public class WishRepository
    {
        private readonly UserRepository userRepository;
        private readonly WishListContext _context;

        public WishRepository(UserRepository userRepository, WishListContext context)
        {
            this.userRepository = userRepository;
            _context = context;
        }

        public UserRepository UserRepository => userRepository;

        public async Task<IEnumerable<Wish>> GetAllWishesAsync()
        {
            var wishes = await _context.Wishes.ToListAsync();

            return wishes;
        }
    }
}
