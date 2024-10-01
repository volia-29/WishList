using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Services.Interfaces;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(CreateUserDto user)
        {
            await userService.AddAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await userService.GetAllUsersAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Ok(await userService.DeleteUser(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto user)
        {
            await userService.UpdateUser(id, user.Name);
            return Ok();
        }
    }
}