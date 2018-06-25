using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    public class ApprovalFileUploadRequest
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
