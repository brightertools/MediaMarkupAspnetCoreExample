using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using MediaMarkup.Api.Models;
using MediaMarkup.Core;

namespace MediaMarkup.Api
{
    public class Users : IUsers
    {
        public HttpClient ApiClient { get; set; }

        public Users(HttpClient apiClient)
        {
            ApiClient = apiClient;
        }

        public async Task<UserInfo> GeByIdt(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserCreateResult> Create(UserCreateParameters userCreateParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<UserUpdateResult> Update(UserUpdateParameters userUpdateParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
