using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Round
    {
        [JsonPropertyName("phase")]
        public string? Phase { get; set; }

        [JsonPropertyName("win_team")]
        public string? WinTeam { get; set; }

        [JsonPropertyName("bomb")]
        public string? Bomb { get; set; }
    }
}
