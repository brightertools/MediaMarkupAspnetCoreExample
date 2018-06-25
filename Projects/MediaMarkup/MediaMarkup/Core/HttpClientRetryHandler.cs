using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace MediaMarkup.Core
{
    public class HttpClientRetryHandler : DelegatingHandler
    {
        public HttpClientRetryHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) => Policy
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .OrResult<HttpResponseMessage>(x => !x.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryCount => TimeSpan.FromSeconds(Math.Pow(3, retryCount)))
            .ExecuteAsync(() => base.SendAsync(request, cancellationToken));
    }
}
