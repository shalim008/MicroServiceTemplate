namespace MasterDataManagement.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message??GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode {get;set;}
        public string Message {get;set;}

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                204 => "No records found.",
                400 => "A bad request, you have made",
                401 => "You are not authorized",
                404 => "Resource found, it was not",
                500 => "Server side error.",
                _ => null
            };
        }
    }
}