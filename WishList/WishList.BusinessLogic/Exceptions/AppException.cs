using System.Net;

namespace WishList.Services.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
