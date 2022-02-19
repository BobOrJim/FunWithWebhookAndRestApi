using API.Entities;

namespace API.Models
{
    public class HighscoreDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public long Time { get; init; }
    }
}
