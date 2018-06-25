using System.Threading.Tasks;
using MediaMarkup.Api;

namespace MediaMarkup
{
    public interface IApiClient
    {
        /// <summary>
        /// Access Token
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Gets an authentication token and initializes the API clients
        /// </summary>
        /// <returns></returns>
        Task<string> InitializeAsync();

        /// <summary>
        /// Test endpoint to check if client authentication token is valid
        /// </summary>
        /// <returns></returns>
        Task<bool> Authenticated();

        /// <summary>
        /// Approvls API Client
        /// </summary>
        IApprovals Approvals { get; }
    }
}