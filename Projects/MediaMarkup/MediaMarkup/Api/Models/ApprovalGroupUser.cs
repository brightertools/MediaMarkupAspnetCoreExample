
namespace MediaMarkup.Api.Models
{
    public class ApprovalUser
    {
        /// <summary>
        /// ApprovalsUser for Parent Approval
        /// </summary>
        public ApprovalUser()
        {
            Administrator = false;
            CommentsEnabled = false;
            AllowDecision = false;
            AllowDownload = false;
        }

        public string UserId { get; set; }

        public bool Administrator { get; set; }

        public bool CommentsEnabled { get; set; }

        public bool AllowDecision { get; set; }

        public bool AllowDownload { get; set; }
    }
}
