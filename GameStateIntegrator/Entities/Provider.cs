using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Provider
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("appid")]
        public int Appid { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("steamid")]
        public string? Steamid { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
