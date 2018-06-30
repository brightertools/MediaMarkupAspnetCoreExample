using System.Net;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// ErrorResult
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
