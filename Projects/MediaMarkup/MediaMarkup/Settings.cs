namespace MediaMarkup
{
    public class Settings
    {
        /// <summary>
        /// Client Id supplied for tenant via admin site
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Secret Key supplied for tenant via admin site
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// API Base Url for all API calls
        /// </summary>
        public string ApiBaseUrl { get; set; }
    }
}