using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WishList.BusinessLogic.Models
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
