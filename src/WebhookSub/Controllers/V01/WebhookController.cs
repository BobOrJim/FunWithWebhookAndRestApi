using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebhookSub.Models;

namespace WebhookSub.Controllers.V01
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private IHttpClientFactory _ihttpClientFactory;

        public WebhookController(IHttpClientFactory ihttpClientFactory)
        {
            _ihttpClientFactory = ihttpClientFactory;
        }


        [HttpGet]
        [Route("StartWebhookSubscription", Name = "StartWebhookSub")]
        public async Task<IActionResult> StartWebhookSub()
        {
            await SubscribeAsync();
            return Ok("You have started the WebhookSub test project, it has registered itself as a subscriber");
        }

        private async Task SubscribeAsync()
        {
            WebhookSubscription webhookSubscription = new() {WebhookUri = @"https://localhost:7194/Webhook/UpdateHighscores"};

            string jsonString = JsonSerializer.Serialize(webhookSubscription);
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var client = _ihttpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PostAsync("https://localhost:7288/api/v01/Webhook/AddSubscriber", payload);
            string responseJson = await response.Content.ReadAsStringAsync();
        }


        [HttpPost]
        [Route("UpdateHighscores", Name = "UpdateHighscoresAsync")]
        public async Task<IActionResult> UpdateHighscoresAsync([FromBody] List<Highscore> highscores)
        {
            if (!ModelState.IsValid)
                return BadRequest("Yo, your model is not cool dude");

            //Do something fun, with the data.
            //Perhaps a server broadcast thing with signalR and SPA.
            return Ok("Thanks bro, i love subscribing to your stuff, cheers");
        }

    }
}