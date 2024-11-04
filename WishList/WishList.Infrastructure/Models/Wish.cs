namespace WishList.Infrastructure.Models
{
    public class Wish
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IsSelected { get; set; }
        public int? UserId { get; set; }
    }
}
