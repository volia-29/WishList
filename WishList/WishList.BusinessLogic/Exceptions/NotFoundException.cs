using System.Net;

namespace WishList.Services.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
        {
            StatusCode = HttpStatusCode.NotFound;
            Message = "Not found";
        }
    }
}
