namespace WishList.Infrastructure.Models
{
    public class WishFulfillment
    {
        public int Id { get; set; }
        public User WishGranter { get; set; }
        public Wish Wish { get; set; }
    }
}
