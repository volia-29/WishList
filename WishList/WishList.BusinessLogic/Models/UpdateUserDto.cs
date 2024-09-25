using System.ComponentModel.DataAnnotations;

namespace WishList.BusinessLogic.Models
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }
    }
}
