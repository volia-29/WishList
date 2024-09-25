using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Infrastructure.Repositories;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserRepository userRepository;
        private readonly WishRepository wishRepository;

        public WishesController(UserRepository userRepository, WishRepository wishRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.wishRepository = wishRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWishesAsync()
        {
            //this.HttpContext.Request.
            //configuration["test-url"]
            return Ok(await wishRepository.GetAllWishesAsync());
        }
    }
}
