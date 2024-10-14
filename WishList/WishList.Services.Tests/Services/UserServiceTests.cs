using Microsoft.EntityFrameworkCore;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Data;
using WishList.Services.Exceptions;
using WishList.Services.Models;
using WishList.Services.Services;

namespace WishList.Services.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task FindUserAsync_NonExistUser_ThrowsException()
        {
            // Arrange
            var context = GetContext();
            var service = new UserService(context);

            // Act
            var act = async () => await service.FindUserAsync(new LoginRequest());

            // Assert
            await Assert.ThrowsAsync<AppException>(act);
        }

        [Fact]
        public async Task FindUserAsync_InvalidPassword_ThrowsException()
        {
            // Arrange
            var context = GetContext();
            var service = new UserService(context);
            var user = new CreateUserDto()
            {
                Name = "Test",
                Password = "password"
            };
            await service.AddAsync(user);
            var loginRequest = new LoginRequest()
            {
                UserName = "Test",
                Password = "incorrect"
            };

            // Act
            var act = async () => await service.FindUserAsync(loginRequest);

            // Assert
            await Assert.ThrowsAsync<AppException>(act);
        }

        [Fact]
        public async Task FindUserAsync_Valid()
        {
            // Arrange
            var context = GetContext();
            var service = new UserService(context);
            var user = new CreateUserDto()
            {
                Name = "Test",
                Password = "password"
            };
            await service.AddAsync(user);
            var loginRequest = new LoginRequest()
            {
                UserName = "Test",
                Password = "password"
            };

            // Act
            var foundUser = await service.FindUserAsync(loginRequest);

            // Assert
            Assert.Equal(foundUser.Name, loginRequest.UserName);
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
