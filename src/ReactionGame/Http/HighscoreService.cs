using Game.Models;
using System.Text.Json;

namespace ReactionGame.Http
{
    public class HighscoreService
    {
        private UniversalHttpClient _client = new();
        private string requestUri = "https://localhost:7288/api/v01/highscores";

        public async Task<List<Highscore>> GetHighscoresFromHighcoreApiAsync(int defaultTimeoutMS = 10000)
        {
            List<Highscore> highscoreList = new();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await _client.SendAsync(request, defaultTimeoutMS);
                var jsonString = await GetValidatedJsonString(response);
                highscoreList = JsonSerializer.Deserialize<List<Highscore>>(jsonString) ?? new List<Highscore>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception {e.Message}");
            }
            return highscoreList;
        }


        public async Task PostHighscoreToHighcoreApiAsync(Highscore highscore, int defaultTimeoutMS = 5000)
        {
            try
            {
                var response = await _client.PostAsync(requestUri, highscore, defaultTimeoutMS);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception {e.Message}");
            }
        }


        private async Task<string> GetValidatedJsonString(HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) //only status 200 is a valid response
                return jsonString;
            throw new HttpRequestException("Response did not recieve status 200, Json response: " + jsonString);
        }
    }
}
