using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;

namespace CMA.APIGetway.Bff.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        private readonly AsyncRetryPolicy<IActionResult> _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        private readonly AsyncPolicyWrap<IActionResult> _policy;
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

            // Retry
            _retryPolicy = Policy<IActionResult>
                            .Handle<Exception>()
                            .RetryAsync(2);

            // Circuit Breaker
            _circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

            _policy = Policy<IActionResult>
                        .Handle<Exception>()
                        .FallbackAsync(Content("Sorry, we are currently experiencing issues. Please try again later"))
                        .WrapAsync(_retryPolicy)
                        .WrapAsync(_circuitBreakerPolicy);
        }

        [HttpGet]
        public async Task<IActionResult> Books()
            => await ProxyTo("http://localhost:5045/api/v1/Book");

        [HttpGet]
        public async Task<IActionResult> Authors()
            => await ProxyTo("http://localhost:5221/api/v1/Author");

        private async Task<IActionResult> ProxyTo(string url)
                => await _policy.ExecuteAsync(async () => Content(await _httpClient.GetStringAsync(url)));
    }
}