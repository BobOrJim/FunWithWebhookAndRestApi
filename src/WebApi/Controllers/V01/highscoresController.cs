using API.Entities;
using API.Mappers;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [ApiController]
    [Route("/api/v01/[controller]")]
    public class highscoresController : ControllerBase
    {
        private IHighScoreRepository _iHighScoreRepository;
        private WebhookPublisherService _webhookPublisherService;

        public highscoresController(IHighScoreRepository iHighScoreRepository, WebhookPublisherService webhookPublisherService)
        {
            _webhookPublisherService = webhookPublisherService;
            _iHighScoreRepository = iHighScoreRepository;
        }

        [HttpGet]
        [Route("", Name = "GetAllHighscoresAsync")]
        public async Task<IActionResult> GetAllHighscoresAsync()
        {
            var highscors = await _iHighScoreRepository.GetAllHighscoresAsync();
            if (highscors == null)
            {
                return NoContent();
            }
            return Ok(highscors.Select(h => DtoMapper.MapHighscoreDto(h)));
        }

        [HttpPost]
        [Route("", Name = "AddHighscoreAsync")]
        public async Task<IActionResult> AddHighscoreAsync([FromBody] Highscore highscore)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _iHighScoreRepository.AddHighscoreAsync(highscore);
            }
            catch (Exception e)
            {
                if (e.GetType().Name == "DbUpdateException")
                {
                    return StatusCode(500, new {message = "Duplicate Id's not allowed"});
                }
                else
                {
                    return StatusCode(500);
                }
            }
            var getHighscore = await _iHighScoreRepository.GetHighscoreByIdAsync(highscore.Id);

            await _webhookPublisherService.PublishToSubscribersAsync();
            return CreatedAtRoute(nameof(GetHighscoreByIdAsync), new { id = getHighscore.Id }, getHighscore);
            //return CreatedAtRoute("GetHighscoreByIdAsync", new {id = getHighscore.Id}, getHighscore);
        }

        [HttpGet]
        [Route("{id:Guid}", Name = "GetHighscoreByIdAsync")]
        public async Task<IActionResult> GetHighscoreByIdAsync(Guid id)
        {
            var highscore = await _iHighScoreRepository.GetHighscoreByIdAsync(id);
            if (highscore == null)
            {
                return NotFound();
            }
            return Ok(DtoMapper.MapHighscoreDto(highscore));
        }

        [HttpGet]
        [Route("{name}", Name = "GetHighscoreByNameAsync")]
        public async Task<IActionResult> GetHighscoreByNameAsync(string name)
        {
            var highscors = await _iHighScoreRepository.GetHighscoresByNameAsync(name);
            if (highscors == null || highscors.Count() == 0)
            {
                return NotFound();
            }
            return Ok(highscors.Select(h => DtoMapper.MapHighscoreDto(h)));
        }

        [HttpDelete]
        [Route("", Name = "DeleteAllHighscores")]
        public async Task<IActionResult> DeleteAllHighscores()
        {
            int postsDeleted = await _iHighScoreRepository.DeleteAllHighscoresAsync();
            await _webhookPublisherService.PublishToSubscribersAsync();
            return Ok(new {Message = $"{postsDeleted} posts deleted"});
        }
    }
}