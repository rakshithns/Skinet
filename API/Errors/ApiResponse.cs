using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode();
        }

        private string GetDefaultMessageForStatusCode()
        {
            return StatusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, You are not",
                404 => "Resource Found, it was not",
                500 => "Errors are the path to dark side. Error leads to Anger. Anger leads to Hate. Hates lead to career change.",
                _=> null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}