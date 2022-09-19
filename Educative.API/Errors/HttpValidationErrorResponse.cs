namespace Educative.API.Errors
{
    public class HttpValidationErrorResponse : HttpErrorReponse
    {
        public HttpValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}