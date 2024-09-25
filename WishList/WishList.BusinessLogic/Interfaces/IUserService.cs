using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;

namespace WishList.Services.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(CreateUserDto user);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetByIdAsync(int id);

        Task<int> DeleteUser(int id);

        Task UpdateUser(int id, string newName);
    }
}
