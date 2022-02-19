using System.Text;
using System.Text.Json;
using Game.Models;

namespace ReactionGame.Http
{
    public class UniversalHttpClient
    {
        private static HttpClient _httpClient = null; //Only one HttpClient for the whole application. If im working with a web program, i would use IHttpClientFactory

        public UniversalHttpClient()
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, int timeoutMS)
        {
            using (var cts = new CancellationTokenSource(timeoutMS))
            {
                try
                {
                    return await _httpClient.SendAsync(request, cts.Token);
                }
                catch (OperationCanceledException oc) //handle timeout
                {
                    throw new TimeoutException("Highscore API timeout, application will exit");
                }
                catch (Exception e)
                {
                    throw new HttpRequestException(e.Message);
                }
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, Highscore highscore, int timeoutMS)
        {
            using (var cts = new CancellationTokenSource(timeoutMS))
            {
                try
                {
                    var stringContent = new StringContent(JsonSerializer.Serialize(highscore), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(uri, stringContent, cts.Token);
                    return response;
                }
                catch (OperationCanceledException oc) //handle timeout
                {
                    throw new TimeoutException("Highscore API timeout, application will exit");
                }
                catch (Exception e)
                {
                    throw new HttpRequestException(e.Message);
                }
            }
        }
    }
}
