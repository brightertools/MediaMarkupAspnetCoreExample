using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// User Update Password Parameters
    /// </summary>
    public class UserUpdatePasswordParameters
    {
        /// <summary>
        /// User Update Password Parameters
        /// </summary>
        public UserUpdatePasswordParameters()
        {
            Id = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// User Id of the User to update (required)
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Password.
        /// Cannot be empty.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}