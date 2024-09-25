using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.BusinessLogic.Models
{
    public class CreateWishDto
    {
        public int UserId { get; set; }
        public string Description { get; set; }
    }
}
