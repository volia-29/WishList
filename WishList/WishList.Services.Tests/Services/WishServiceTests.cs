using Microsoft.EntityFrameworkCore;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;
using WishList.Services.Services;

namespace WishList.Services.Tests.Services
{
    public class WishServiceTests
    {
        [Theory]
        [InlineData(0, true, 1)]
        [InlineData(1, false, 0)]
        public async Task ChooseWishAsync_ChoosingWishes(int defaultState, bool action, int expectedResult)
        {
            // Arrange
            var context = GetContext();
            var service = new WishService(context);
            var wish = new Wish()
            {
                Id = 1,
                Description = "test wish",
                IsSelected = defaultState,
            };
            var user1 = new User()
            {
                Id = 1,
                Name = "Test",
                Password = "password",
                Wishes = new List<Wish>() { wish },
            };
            var user2 = new User()
            {
                Id = 2,
                Name = "Test 2",
                Password = "password",
            };

            context.Wishes.Add(wish);
            context.Users.Add(user1);
            context.Users.Add(user2);

            if (defaultState == 1)
            {
                context.WishFulfillments.Add(new WishFulfillment()
                {
                    Wish = wish,
                    WishGranter = user2,
                });
            }

            context.SaveChanges();

            // Act
            await service.ChooseWishAsync(user2, new ChoseWishDto() { Select = action, WishId = 1});

            // Assert
            Assert.Equal(context.Wishes.Single().IsSelected, expectedResult);
        }

        private WishListContext GetContext()
        {
            var options = new DbContextOptionsBuilder<WishListContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new WishListContext(options);
        }
    }
}
