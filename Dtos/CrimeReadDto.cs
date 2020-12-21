using CrimesRestApi.Models;
using Newtonsoft.Json;


namespace CrimesRestApi.Dtos
{
    public class CrimeReadDto
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "uuid")]
        public string UUID { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "date")]
        public long Date { get; set; }
        [JsonProperty(PropertyName = "solved")]
        public bool Solved { get; set; }
        [JsonProperty(PropertyName = "require_police")]
        public bool RequirePolice { get; set; }
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }
    }
}
