using API.Entities;
using API.Models;

namespace API.Mappers
{
    public class DtoMapper
    {
        public static HighscoreDto MapHighscoreDto(Highscore highscore)
        {
            return new HighscoreDto
            { 
                Id = highscore.Id,
                Name = highscore.Name,
                Time = highscore.Time,
            };
        }
    }
}
