using System.Threading.Tasks;
using MediaMarkup.Api;

namespace MediaMarkup
{
    public interface IApiClient
    {
        /// <summary>
        /// Access token to be stored and re-used by application
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
        /// Approvals API Client
        /// </summary>
        IApprovals Approvals { get; }

        /// <summary>
        /// Users API Client
        /// </summary>
        IUsers Users { get; }
    }
}