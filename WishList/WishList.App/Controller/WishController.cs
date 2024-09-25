using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Infrastructure.Repositories;

namespace WishList.App.Controller
{
    [Route("wish")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public WishController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateWishAsync(CreateWishDto wish)
        {
            await userRepository.AddWishAsync(wish.UserId, new Wish()
            {
                Description = wish.Description,

            });
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserWishesAsync(int userId)
        {
            return Ok(await userRepository.GetUserWishesAsync(userId));
        }
    }
}
