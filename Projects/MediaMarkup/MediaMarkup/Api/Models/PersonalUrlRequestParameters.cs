using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    public class PersonalUrlRequestParameters
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("approvalId")]
        public string ApprovalId { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("compareVersion")]
        public int CompareVersion { get; set; }
    }
}
