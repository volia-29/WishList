namespace WishList.Services.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
        {
            StatusCode = 404;
            Message = "Not found";
        }
    }
}
