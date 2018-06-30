using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MediaMarkup.Api;
using MediaMarkup.Api.Models;
using MediaMarkup.Tests.TestOrdering;
using Xunit;
using Xunit.Abstractions;

namespace MediaMarkup.Tests
{
    [TestCaseOrderer("MediaMarkup.Tests.TestOrdering.PriorityOrderer", "MediaMarkup.Tests")]
    public class MediaMarkupApiTests : IClassFixture<TestContextFixture>
    {
        private readonly TestContextFixture _context;

        public MediaMarkupApiTests(TestContextFixture fixture)
        {
            _context = fixture;
        }

        [Fact, TestPriority(1)]
        public async Task GetAccessToken()
        {
            _context.ApiClient = new ApiClient(_context.Settings);

            _context.AccessToken = await _context.ApiClient.InitializeAsync();

            Assert.True(!string.IsNullOrWhiteSpace(_context.AccessToken));
        }

        [Fact, TestPriority(2)]
        public async Task CheckAuthentication()
        {
            var result = await _context.ApiClient.Authenticated();

            Assert.True(result);
        }

        [Fact, TestPriority(3)]
        public async Task TestUserApiMethods()
        {
            // Create test user
            var userCreateParameters = new UserCreateParameters
            {
                FirstName = "TestUserApiMethods",
                LastName = "TestUserApiMethods",
                EmailAddress = "TestUserApiMethods@brightertools.com",
                UserRole = UserRole.Administrator,
                Password = "",
                WebLoginEnabled = false
            };
            var userCreated = await _context.ApiClient.Users.Create(userCreateParameters);

            Assert.True(userCreated != null);

            // Get the user by id
            var retrievedUserById = await _context.ApiClient.Users.GeById(userCreated.Id, true);
            
            Assert.True(retrievedUserById != null && userCreated.Id == retrievedUserById.Id);

            // Update the user
            var userUpdateParameters = new UserUpdateParameters
            {
                Id = retrievedUserById.Id,
                FirstName = "updated",
                LastName = "updated",
                EmailAddress = userCreated.EmailAddress,
                UserRole = UserRole.Administrator,
                WebLoginEnabled = true
            };
            var updatedUser = await _context.ApiClient.Users.Update(userUpdateParameters);

            // Get the user by email
            var retrievedUserByEmail = await _context.ApiClient.Users.GetByEmail(userCreated.EmailAddress);
            Assert.True(retrievedUserById != null && userCreated.Id == retrievedUserByEmail.Id);

            // Check the user is updated
            Assert.True(retrievedUserByEmail.FirstName == "updated");

            // Delete the user
            await _context.ApiClient.Users.Delete(userCreated.Id);
            
            // Get the user by if to see if user exists
            var userExists = await _context.ApiClient.Users.GeById(userCreated.Id);
            
            Assert.True(userExists == null);
        }

        [Fact, TestPriority(4)]
        public async Task TestApprovalCreateReadUpdateDelete()
        {
            var parameters = new ApprovalListRequestParameters();
            var approvalListResult = await _context.ApiClient.Approvals.GetList(parameters);

            var approvalCount = approvalListResult.TotalCount;

            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var testFile = Path.Combine(baseDir, "Files", "PDFTestFile2Pages.pdf");
            

            // Upload Approval
            var createResult = await _context.ApiClient.Approvals.Create(testFile, new ApprovalCreateParameters{ Name = "TestApproval"});

            Assert.True(!string.IsNullOrWhiteSpace(createResult.Id));


        }

        [Fact, TestPriority(100)]
        public void CreateApproval()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var testFile = Path.Combine(baseDir, "Files", "PDFTestFile2Pages.pdf");
            var file = File.OpenRead(testFile);
            
        }

        [Fact, TestPriority(1000)]
        public async Task Cleanup()
        {
            // delete test approval owner user

            // delete test approval reviewer user

            Assert.True(true);
        }
    }
}
