using System.Text.Json.Serialization;

namespace BlazorCodeChallenge.Models
{
    public class NominatimLocation
    {
        [JsonPropertyName("place_id")]
        public long PlaceId { get; set; }

        [JsonPropertyName("lat")]
        public string Lat { get; set; } = "";

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = "";

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = "";
    }
}