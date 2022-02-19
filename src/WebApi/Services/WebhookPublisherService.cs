using API.Mappers;
using API.Repository;
using System.Text;
using System.Text.Json;

namespace API.Services
{
    public class WebhookPublisherService
    {
        private IWebhookRepository _iWebhookRepository;
        private IHighScoreRepository _iHighScoreRepository;
        private IHttpClientFactory _ihttpClientFactory;

        public WebhookPublisherService(IWebhookRepository iWebhookRepository, IHighScoreRepository iHighScoreRepository, IHttpClientFactory ihttpClientFactory)
        {
            _ihttpClientFactory = ihttpClientFactory;
            _iHighScoreRepository = iHighScoreRepository;
            _iWebhookRepository = iWebhookRepository;
        }

        public async Task PublishToSubscribersAsync()
        {
            var highscors = await _iHighScoreRepository.GetAllHighscoresAsync();
            string jsonString = JsonSerializer.Serialize(highscors.Select(h => DtoMapper.MapHighscoreDto(h)));
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var client = _ihttpClientFactory.CreateClient();

            foreach (var subscriber in _iWebhookRepository.GetSubscribers())
            {
                HttpResponseMessage response = await client.PostAsync(subscriber.WebhookUri, payload);
                string responseJson = await response.Content.ReadAsStringAsync();
            }
        }

        
    }
}
