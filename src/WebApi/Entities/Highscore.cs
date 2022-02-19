using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Highscore
    {
        [Required]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Name { get; init; }

        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public long Time { get; init; }
    }
}
