using System.Threading.Tasks;
using MediaMarkup.Api.Models;

namespace MediaMarkup.Api
{
    public interface IApprovals
    {
        /// <summary>
        /// Gets the list of approvals for the specified parameters, <see cref="ApprovalListRequestParameters"/>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns><see cref="ApprovalListResult"/></returns>
        Task<ApprovalListResult> GetList(ApprovalListRequestParameters parameters);

        /// <summary>
        /// Upload file to create a new Approval
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="approvalCreateParameters"></param>
        /// <returns></returns>
        Task<ApprovalCreateResult> Create(string filePath, ApprovalCreateParameters approvalCreateParameters);

        /// <summary>
        /// Upload file to create a new Approval
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fileContent"></param>
        /// <param name="approvalCreateParameters"></param>
        /// <returns></returns>
        Task<ApprovalCreateResult> Create(string filename, byte[] fileContent, ApprovalCreateParameters approvalCreateParameters);

        /// <summary>
        /// Create a new version for an existing approval
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="filename"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        Task<string> CreateNewVersion(string approvalId, string filename, byte[] fileContent);

        /// <summary>
        /// Gets the personal url for the specified approvalId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="approvalId"></param>
        /// <param name="version"></param>
        /// <param name="compareVersion"></param>
        /// <returns></returns>
        Task<string> GetPersonalUrl(string userId, string approvalId, int? version = null, int? compareVersion = null);
    }
}