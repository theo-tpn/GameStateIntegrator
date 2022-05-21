using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class PhaseCountdown
    {
        [JsonPropertyName("phase")]
        public string? Phase { get; set; }

        [JsonPropertyName("phase_ends_in")]
        public string? PhaseEndsIn { get; set; }
    }
}
