using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MediaMarkup.Api;
using MediaMarkup.Core;
using MediaMarkup.Api.Models;

namespace MediaMarkup
{
    public class ApiClient : IApiClient
    {
        /// <summary>
        /// Settings, passed in or loaded from appSettings
        /// </summary>
        private Settings ClientSettings { get; set; }

        /// <summary>
        /// HttpClient for making API calls
        /// </summary>
        private HttpClient HttpClient { get; set; }

        /// <summary>
        /// Access token to be stored and re-used by application
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Approvals Client
        /// </summary>
        public IApprovals Approvals { get; private set; }

        /// <summary>
        /// Creates the Media Markup Client
        /// </summary>
        /// <param name="clientSettings"></param>
        /// <param name="token"></param>
        public ApiClient(Settings clientSettings, string token = null)
        {
            AccessToken = token;

            ClientSettings = clientSettings;

            if (!ClientSettings.ApiBaseUrl.EndsWith("/"))
            {
                ClientSettings.ApiBaseUrl = $"{ClientSettings.ApiBaseUrl}/";
            }
        }

        /// <summary>
        /// Gets an authentication token and initializes the API clients
        /// </summary>
        /// <returns></returns>
        public async Task<string> InitializeAsync()
        {
            if (string.IsNullOrWhiteSpace(AccessToken) || !AccessTokenPayloadIsValid())
            {
                AccessToken = await GetAccessToken();
            }

            HttpClient = GetHttpClient();

            Approvals = new Approvals(HttpClient);

            return AccessToken;
        }

        /// <summary>
        /// Tests if client is authenticated using the current access token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Authenticated()
        {
            var response = await HttpClient.PostAsync($"{ClientSettings.ApiBaseUrl}Authentication/Authenticated/", null);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets the Http Client for making the API calls, uses the supplied token or gets a new one if payload is invlaid/expired
        /// </summary>
        /// <returns></returns>
        private HttpClient GetHttpClient()
        {
            var apiClient = new HttpClient(new HttpClientRetryHandler(new HttpClientHandler())) {BaseAddress = new Uri(ClientSettings.ApiBaseUrl)};

            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            
            return apiClient;
        }

        private async Task<string> GetAccessToken()
        {
            // Get new httpclient here for connection without authorization using client is and secret
            var apiClient = new HttpClient{ BaseAddress = new Uri(ClientSettings.ApiBaseUrl) };

            var response = await apiClient.PostAsJsonAsync($"{ClientSettings.ApiBaseUrl}Authentication/GetToken/", new AccessTokenRequestParameters
            {
                ClientId = ClientSettings.ClientId,
                SecretKey = ClientSettings.SecretKey
            });

            if (response.IsSuccessStatusCode)
            {
                AccessToken = await response.Content.ReadAsStringAsync();
                return AccessToken;
            }

            AccessToken = null;

            throw new Exception($"{(int)response.StatusCode}, {response.StatusCode}, {response.ReasonPhrase}");
        }

        /// <summary>
        /// Validates token payload, checks sub & exp (client id and expiry date)
        /// </summary>
        /// <returns></returns>
        private bool AccessTokenPayloadIsValid()
        {
            var jwtTokenData = Jwt.GetTokenData(AccessToken);

            if (!string.IsNullOrWhiteSpace(jwtTokenData?.Payload))
            {
                dynamic payload = JsonConvert.DeserializeObject(jwtTokenData.Payload);

                string subject = (payload["sub"] ?? "").ToString();
                string expiry = (payload["exp"] ?? "").ToString();

                if (!string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(expiry))
                {
                    var expiryDate = Jwt.UnixTimeStampToDateTime(expiry);

                    if (subject.IndexOf(ClientSettings.ClientId, StringComparison.OrdinalIgnoreCase) >= 0 || expiryDate > DateTime.UtcNow.AddMinutes(1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}