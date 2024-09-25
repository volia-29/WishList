namespace WishList.Services.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
