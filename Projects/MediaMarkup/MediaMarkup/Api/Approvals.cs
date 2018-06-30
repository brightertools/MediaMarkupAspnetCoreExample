using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using MediaMarkup.Api.Models;
using MediaMarkup.Core;

namespace MediaMarkup.Api
{
    public class Approvals : IApprovals
    {
        public HttpClient ApiClient { get; set; }

        public Approvals(HttpClient apiClient)
        {
            ApiClient = apiClient;
        }

        /// <summary>
        /// Gets the list of approvals for the specified parameters, <see cref="ApprovalListRequestParameters"/>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns><see cref="ApprovalListResult"/></returns>
        public async Task<ApprovalListResult> GetList(ApprovalListRequestParameters parameters)
        {
            var response = await ApiClient.PostAsJsonAsync("Approvals/GetList/", parameters);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJsonAsync<ApprovalListResult>();
            }

            throw new ApiException("Approvals.GetList", response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApprovalCreateResult> Create(string filePath, ApprovalCreateParameters approvalCreateParameters)
        {
            var filename = Path.GetFileName(filePath);

            var fileContent = File.ReadAllBytes(filePath);

            return await Create(filename, fileContent, approvalCreateParameters);
        }

        /// <summary>
        /// Creates an approval, uploading the supplied media and for the specified approval parameters
        /// </summary>
        /// <returns></returns>
        public async Task<ApprovalCreateResult> Create(string filename, byte[] fileContent, ApprovalCreateParameters approvalCreateParameters)
        {
            var fileExtension = Path.GetExtension(filename);

            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentException("Approvals.Create: File must have an extension.");
            }

            if (fileContent.Length <= 0)
            {
                throw new ArgumentException("Approvals.Create: File length muct be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(approvalCreateParameters.OwnerUserId))
            {
                throw new ArgumentException("Approvals.Create: Owner not specified");
            }

            if (string.IsNullOrWhiteSpace(approvalCreateParameters.Name))
            {
                throw new ArgumentException("Approvals.Create: Approval name not specified");
            }

            using (var formData = new MultipartFormDataContent())
            {
                var fileUploadParametersJson = Newtonsoft.Json.JsonConvert.SerializeObject(approvalCreateParameters);
                var stringContent = new StringContent(fileUploadParametersJson);
                stringContent.Headers.Add("Content-Disposition", "form-data; name=\"ApprovalCreateParameters\"");
                formData.Add(stringContent, "ApprovalCreateParameters");

                var filePostData = new ByteArrayContent(fileContent);
                filePostData.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = HttpUtility.UrlEncode(filename)
                };
                formData.Add(filePostData);

                var response = await ApiClient.PostAsync("Approvals/Create/", formData);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsJsonAsync<ApprovalCreateResult>();
                }

                throw new ApiException("Approvals.Create", response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<string> CreateNewVersion(string approvalId, string filename, byte[] fileContent)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPersonalUrl(string userId, string approvalId, int? version = null, int? compareVersion = null)
        {
            var response = await ApiClient.PostAsJsonAsync("Approvals/GetPersonalUrl/", new Models.PersonalUrlRequestParameters
            {
                UserId = userId,
                ApprovalId = approvalId
            });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"{response.StatusCode},{response.ReasonPhrase}");
        }
    }
}
