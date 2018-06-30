using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// UseRetryLogic to retry api calls if the status code is present in RetryStatusCodes
        /// </summary>
        public bool? UseRetryLogic { get; set; }

        /// <summary>
        /// Retry logic will be applied to the RetryStatusCodes supplied. (command separated string)
        /// </summary>
        public string RetryStatusCodes { get; set; }

        /// <summary>
        /// RetryStatusCodes as List
        /// </summary>
        public List<int> RetryStatusCodesList => RetryStatusCodes?.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList() ?? new List<int>();
    }
}