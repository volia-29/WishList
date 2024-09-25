using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Infrastructure.Repositories;

namespace WishList.App.Controller
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UsersController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(CreateUserDto user)
        {
            await userRepository.AddAsync(new User()
            {
                Name = user.Name
            });
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await userRepository.GetAllUsersAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Ok(await userRepository.DeleteUser(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto user)
        {
            await userRepository.UpdateUser(id, user.Name);
            return Ok();
        }
    }
}