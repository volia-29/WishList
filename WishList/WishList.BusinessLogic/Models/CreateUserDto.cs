using System.ComponentModel.DataAnnotations;

namespace WishList.BusinessLogic.Models
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
