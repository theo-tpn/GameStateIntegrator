using System.Text.Json.Serialization;

namespace GameStateIntegrator.Entities
{
    public class Weapon
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("paintkit")]
        public string? Paintkit { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("ammo_clip")]
        public int AmmoClip { get; set; }

        [JsonPropertyName("ammo_clip_max")]
        public int AmmoClipMax { get; set; }

        [JsonPropertyName("ammo_reserve")]
        public int AmmoReserve { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }
    }

    public class Weapons
    {
        [JsonPropertyName("weapon_0")]
        public Weapon? Weapon0 { get; set; }

        [JsonPropertyName("weapon_1")]
        public Weapon? Weapon1 { get; set; }

        [JsonPropertyName("weapon_2")]
        public Weapon? Weapon2 { get; set; }

        [JsonPropertyName("weapon_3")]
        public Weapon? Weapon3 { get; set; }

        [JsonPropertyName("weapon_4")]
        public Weapon? Weapon4 { get; set; }

        [JsonPropertyName("weapon_5")]
        public Weapon? Weapon5 { get; set; }
    }
}
