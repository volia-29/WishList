using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Models;

namespace WishList.Infrastructure.Data
{
    public class WishListContext : DbContext
    {
        public WishListContext(DbContextOptions<WishListContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public DbSet<WishFulfillment> WishFulfillments { get; set; }
    }
}
