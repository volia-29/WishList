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
    }
}