using Moq;
using WishList.App.Controller;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Services.Interfaces;

namespace WishList.App.Tests.Controller
{
    public class WishControllerTests
    {
        [Fact]
        public async Task ChoiceOfWishAsync_CallWishService()
        {
            // Arrange
            var userModel = new User()
            {
                Id = 0
            };

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetByIdAsync(0)).Returns(Task.Run<User?>(() => userModel));

            var choseWish = new ChoseWishDto()
            {
                UserId = 0
            };

            var wishServiceMock = new Mock<IWishService>();
            wishServiceMock.Setup(mock => mock.ChooseWishAsync(userModel, choseWish));

            var controller = new WishController(userServiceMock.Object, wishServiceMock.Object);

            // Act
            await controller.ChoiceOfWishAsync(choseWish);

            // Assert
            wishServiceMock.Verify(mock => mock.ChooseWishAsync(userModel, choseWish), Times.Once());
        }
    }
}
