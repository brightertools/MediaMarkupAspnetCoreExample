using System;
using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    public class ApprovalCreateParameters
    {
        /// <summary>
        /// ApprovalsCreateParameters
        /// </summary>
        public ApprovalCreateParameters()
        {
            OwnerUserId = string.Empty;
            Name = string.Empty;
            NumberOfDecisionsRequired = 1;
        }

        /// <summary>
        /// The owner User id asociated with the approval
        /// </summary>
        [JsonProperty("ownerUserId")]
        public string OwnerUserId { get; set; }

        /// <summary>
        /// Name / Description of the apprpoval
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Number of decisions required, 0 = all users in initial approval group
        /// </summary>
        [JsonProperty("numberOfDecisionsRequired")]
        public int? NumberOfDecisionsRequired { get; set; }

        [JsonProperty("deadline")]
        public DateTime? Deadline { get; set; }
    }
}
