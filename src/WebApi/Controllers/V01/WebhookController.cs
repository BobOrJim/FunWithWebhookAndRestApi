using System.Diagnostics;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [ApiController]
    [Route("/api/v01/[controller]")]
    public class WebhookController : ControllerBase
    {
        private IWebhookRepository _iWebhookRepository;

        public WebhookController(IWebhookRepository iWebhookRepository)
        {
            _iWebhookRepository = iWebhookRepository;
        }

        [HttpPost]
        [Route("AddSubscriber", Name = "AddSubscriberAsync")]
        public async Task<IActionResult> AddSubscriberAsync([FromBody] WebhookSubscription webhookSubscription)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _iWebhookRepository.AddSubscriber(webhookSubscription);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error in AddSubscriberAsync. Error is {e.Message}");
                await Task.CompletedTask;
                return StatusCode(500);
            }
            await Task.CompletedTask;
            return Created(nameof(AddSubscriberAsync), webhookSubscription);
        }

    }
}