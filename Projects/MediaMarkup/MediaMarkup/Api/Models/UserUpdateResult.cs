using System.Collections.Generic;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// User Update Result
    /// </summary>
    public class UserUpdateResult
    {
        /// <summary>
        /// Indicates if the creation has errors
        /// </summary>
        public bool HasErrors { get; set; }

        /// <summary>
        /// Error Messages
        /// </summary>
        public List<string> ErrorMessages { get; set; }
    }
}
