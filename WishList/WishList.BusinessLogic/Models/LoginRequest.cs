using System.ComponentModel;

namespace WishList.Services.Models
{
    public class LoginRequest
    {
        [DisplayName("User name")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
