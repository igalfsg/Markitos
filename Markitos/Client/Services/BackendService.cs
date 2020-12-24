using Markitos.Shared.Models;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Markitos.Client.Services
{
    public class BackendService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        public BackendService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
               HttpStatusCode.RequestTimeout, // 408
               HttpStatusCode.InternalServerError, // 500
               HttpStatusCode.BadGateway, // 502
               HttpStatusCode.ServiceUnavailable, // 503
               HttpStatusCode.GatewayTimeout // 504
            };
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrInner<TaskCanceledException>()
                .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                  .WaitAndRetryAsync(new[]
                  {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(8)
                  });
        }

        public async Task<APIResultModel> SendDelete(string url)
        {
            APIResultModel apiResult = new APIResultModel();
            HttpResponseMessage responseMessage;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            try
            {
                responseMessage = await _retryPolicy.ExecuteAsync(async () =>
                          await SendMessageAsync(requestMessage));
                apiResult.Message = await responseMessage.Content.ReadAsStringAsync();
                apiResult.Success = responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                if (ex.Message.Contains("One or more errors"))
                {
                    apiResult.Message = ex.InnerException.Message;
                }
                else if (ex.Message.Equals("The request message was already sent. Cannot send the same request message multiple times."))
                {
                    apiResult.Message = "Error contacting Server Please try again later";
                }
                else
                {
                    apiResult.Message = ex.Message;
                }
            }
            return apiResult;
        }

        public async Task<APIResultModel> PostToBackend(string url, string jsonPayload)
        {
            APIResultModel apiResult = new APIResultModel();
            HttpResponseMessage responseMessage;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            try
            {
                requestMessage.Content = new StringContent(jsonPayload,
                    Encoding.UTF8, "application/json");
                responseMessage = await _retryPolicy.ExecuteAsync(async () =>
                          await SendMessageAsync(requestMessage));
                apiResult.Message = await responseMessage.Content.ReadAsStringAsync();
                apiResult.Success = responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                if (ex.Message.Contains("One or more errors"))
                {
                    apiResult.Message = ex.InnerException.Message;
                }
                else if (ex.Message.Equals("The request message was already sent. Cannot send the same request message multiple times."))
                {
                    apiResult.Message = "Error contacting Server Please try again later";
                }
                else
                {
                    apiResult.Message = ex.Message;
                }
            }
            return apiResult;
        }

        public async Task<APIResultModel> CallGetApiAsync(string url)
        {
            APIResultModel apiResult = new APIResultModel();
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url is empty or null", nameof(url));
            }
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            try
            {
                HttpResponseMessage response;
                response = await _retryPolicy.ExecuteAsync(async () =>
                         await SendMessageAsync(requestMessage)
                    );
                apiResult.Success = response.IsSuccessStatusCode;
                apiResult.Message = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = "Error contacting server, please try again later";
            }
            return apiResult;
        }

        private async Task<HttpResponseMessage> SendMessageAsync(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response;
            response = await _httpClient.SendAsync(requestMessage);
            return response;
        }
    }
}
