using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Services.Models;

namespace WishList.Services.Interfaces
{
    public interface IWishService
    {
        Task AddWishAsync(int userId, CreateWishDto wish);

        Task<int> DeleteWishAsync(int id);

        Task<List<Wish>> GetAllUserWishesAsync(int userId);

        Task<List<GetWishDto>> GetAvailableUserWishesAsync(int userId);

        Task ChooseWishAsync(User user, ChoseWishDto choseWish);
    }
}
