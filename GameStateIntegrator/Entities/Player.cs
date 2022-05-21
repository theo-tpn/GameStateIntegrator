using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Player
    {
        [JsonPropertyName("steamid")]
        public string? Steamid { get; set; }

        [JsonPropertyName("clan")]
        public string? Clan { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("observer_slot")]
        public int ObserverSlot { get; set; }

        [JsonPropertyName("team")]
        public string? Team { get; set; }

        [JsonPropertyName("activity")]
        public string? Activity { get; set; }

        [JsonPropertyName("player_state")]
        public PlayerState? State { get; set; }

        [JsonPropertyName("match_stats")]
        public PlayerMatchStats? Stats { get; set; }

        [JsonPropertyName("weapons")]
        public Weapons? Weapons { get; set; }

        [JsonPropertyName("spectarget")]
        public string? Spectarget { get; set; }

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("forward")]
        public string? Forward { get; set; }
    }
}
