using MediaMarkup.Api;
using Microsoft.Extensions.DependencyInjection;

namespace MediaMarkup
{
    public static class ServicesExtensions
    {
        public static void AddMediaMarkup(this IServiceCollection services)
        {
            services.AddScoped<IApprovals, Approvals>();
            services.AddScoped<IApiClient, ApiClient>();
        }
    }
}
