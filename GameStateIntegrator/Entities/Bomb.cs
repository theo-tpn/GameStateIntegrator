using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Bomb
    {
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("player")]
        public long Player { get; set; }

        [JsonPropertyName("countdown")]
        public string? Countdown { get; set; }
    }
}
