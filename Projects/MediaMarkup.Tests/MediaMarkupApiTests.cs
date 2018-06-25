using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MediaMarkup.Api.Models;
using MediaMarkup.Tests.TestOrdering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace MediaMarkup.Tests
{
    [TestCaseOrderer("MediaMarkup.Tests.TestOrdering.PriorityOrderer", "MediaMarkup.Tests")]
    public class MediaMarkupApiTests : IClassFixture<TestContextFixture>
    {
        

        private TestContextFixture Context;

        public MediaMarkupApiTests(TestContextFixture fixture)
        {
            Context = fixture;
        }

        [Fact, TestPriority(1)]
        public async Task GetAccessToken()
        {
            Context.ApiClient = new ApiClient(Context.Settings);

            Context.AccessToken = await Context.ApiClient.InitializeAsync();

            //_settings = _serviceProvider.GetService<IOptions<Settings>>().Value;

            //_apiClient = new ApiClient(_settings);

            //AccessToken = await _apiClient.InitializeAsync();

            Assert.True(!string.IsNullOrWhiteSpace(Context.AccessToken));
        }

        [Fact, TestPriority(2)]
        public async Task CheckAuthentication()
        {
            var result = await Context.ApiClient.Authenticated();

            Assert.True(result);
        }

        [Fact, TestPriority(3)]
        public async Task TestApprovalCreateReadUpdateDelete()
        {
            var parameters = new ApprovalListRequestParameters();
            var approvalListResult = await Context.ApiClient.Approvals.GetList(parameters);

            var approvalCount = approvalListResult.TotalCount;

            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var testFile = Path.Combine(baseDir, "Files", "PDFTestFile2Pages.pdf");
            

            // Upload Approval
            var createResult = await Context.ApiClient.Approvals.Create(testFile, new ApprovalCreateParameters{ Name = "TestApproval"});

            Assert.True(!string.IsNullOrWhiteSpace(createResult.Id));


        }

        [Fact, TestPriority(100)]
        public void CreateApproval()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var testFile = Path.Combine(baseDir, "Files", "PDFTestFile2Pages.pdf");
            var file = File.OpenRead(testFile);
            
        }
    }
}
