using System;
using System.Net;
using System.Security.Authentication;

namespace MediaMarkup
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode httpStatusCode, string message = null)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                {
                    throw new AuthenticationException();
                }
                default:
                {
                    throw new Exception($"{(int)httpStatusCode}, {httpStatusCode}, {message}");
                }
            }
        }
    }
}
