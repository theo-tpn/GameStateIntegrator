using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class PlayerMatchStats
    {
        [JsonPropertyName("kills")]
        public int Kills { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("mvps")]
        public int Mvps { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
