using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Infrastructure.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
        public ICollection<Wish> Wishes { get; set; } = new List<Wish>(0);
        public string? ProfilePicture { get; set; }
    }
}
