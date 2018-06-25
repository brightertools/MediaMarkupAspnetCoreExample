using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// User Info
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// User First Name
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// User Last Name
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// User EmailAddress
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}