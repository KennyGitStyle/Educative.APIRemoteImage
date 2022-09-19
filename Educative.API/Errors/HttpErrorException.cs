namespace Educative.API.Errors
{
    public class HttpErrorException : HttpErrorReponse
    {
        public HttpErrorException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}