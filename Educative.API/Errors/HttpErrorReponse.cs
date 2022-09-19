namespace Educative.API.Errors

{
    public class HttpErrorReponse
    {
        public HttpErrorReponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            
            
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Oops.. I think you may have entered something wrong!",
                401 => "Oops.. We cannot authorize your access!",
                404 => "Oops... We cannot find what you are looking for!",
                500 => "Oh noo.. Something went wrong with the server!",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        
        
    }
}