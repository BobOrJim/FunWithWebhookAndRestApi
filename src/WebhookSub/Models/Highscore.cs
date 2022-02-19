using System.ComponentModel.DataAnnotations;

namespace WebhookSub.Models
{
    public class Highscore
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public long Time { get; init; }
    }
}
