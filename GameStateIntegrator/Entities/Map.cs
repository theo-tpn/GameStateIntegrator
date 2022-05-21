using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Map
    {
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("phase")]
        public string? Phase { get; set; }

        [JsonPropertyName("round")]
        public int Round { get; set; }

        [JsonPropertyName("team_ct")]
        public TeamStats? TeamCt { get; set; }

        [JsonPropertyName("team_t")]
        public TeamStats? TeamT { get; set; }

        [JsonPropertyName("num_matches_to_win_series")]
        public int NumMatchesToWinSeries { get; set; }

        [JsonPropertyName("current_spectators")]
        public int CurrentSpectators { get; set; }

        [JsonPropertyName("souvenirs_total")]
        public int SouvenirsTotal { get; set; }
    }
}
