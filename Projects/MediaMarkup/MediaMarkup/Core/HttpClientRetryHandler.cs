using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace MediaMarkup.Core
{
    public class HttpClientRetryHandler : DelegatingHandler
    {
        private List<int> RetryStatusCodes { get; set; }

        public HttpClientRetryHandler(HttpMessageHandler innerHandler, List<int> retryStatusCodes) : base(innerHandler)
        {
            RetryStatusCodes = retryStatusCodes;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Policy
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .OrResult<HttpResponseMessage>(x => RetryStatusCodes.Contains((int)x.StatusCode) )
            .WaitAndRetryAsync(5, retryCount => TimeSpan.FromSeconds(Math.Pow(3, retryCount)))
            .ExecuteAsync(() => base.SendAsync(request, cancellationToken));
    }
}