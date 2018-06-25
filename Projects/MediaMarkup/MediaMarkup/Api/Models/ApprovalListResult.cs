using System.Collections.Generic;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// Approval Create Result
    /// </summary>
    public class ApprovalListResult
    {
        /// <summary>
        /// Approval Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Viewerer Url
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public List<ApprovalListItem> Approvals { get; set; }
    }
}
