using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Controllers
{
    public class ApprovalsController : Controller
    {
        private MediaMarkup.Settings MediaMarkupSettings { get; set; }

        public ApprovalsController(IOptions<MediaMarkup.Settings> mediaMarkupSettings)
        {
            MediaMarkupSettings = mediaMarkupSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Viewer()
        {
            return View();
        }

        /// <summary>
        /// Use something like this for your own Personal/Magic link URLs or via your own pages, or emails
        /// Note: Do not create Personal Urls for every item in a list page, doing this may exceed the rate limit of the API and the urls may expire before use.
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="version"></param>
        /// <param name="compareVersion"></param>
        /// <returns></returns>
        // [Authorize] This should be authorized by your own application, which will redirect via login as required
        public async Task<IActionResult> OpenViewer(string approvalId, int? version, int? compareVersion)
        {
            approvalId = "1";

            // Create the Media Markup APi Client (we can supply a previous access token here)
            var client = new MediaMarkup.ApiClient(MediaMarkupSettings);

            // Get Access Token for later API calls if required and initilaize the API clients
            var accessToken = await client.InitializeAsync();

            // Get Personal Url from MediaMarkup API and redirect

            // The Personal Url is short lived (3 miniutes), therefore must be requested directly before redirection
            var personalApprovalUrl = await client.Approvals.GetPersonalUrl("auth0|59e8699db0137762a81d6dc4", approvalId, version, compareVersion);

            // Redirect to MediaMarkup Page for approval
            return Redirect(personalApprovalUrl);

            // Alternatively Host Media Markup Page within an iFrame using this example
            // return RedirectToAction("Viewer", "Approvals", new { url = personalApprovalUrl })
        }

        // This page must handle the action supplied
        public IActionResult Redirect(string action, string approvalId, int? version, int? compareVersion)
        {
            
            switch (action)
            {
                case "token-expired":
                {
                        // If logged in to application
                        //if (User.Identity.IsAuthenticated)
                        //{
                        // Get New PersonalUrl

                        //}

                    return View(new {Message = "token expired"});
                }
                case "approval-not-found":
                {
                    return View(new {Message = "Approval not found (expired or deleted)"});
                }
                case "invalid-token":
                {
                    return View(new { Message = "Invalid token" });
                }
                default:
                {
                    return View(new { Message = "Unknown Error" });
                }
            }
        }
    }
}
