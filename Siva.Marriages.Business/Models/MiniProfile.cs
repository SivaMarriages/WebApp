using System.Text.Json.Serialization;

namespace Siva.Marriages.Business.Models
{
    public class SiblingsProfile : BaseProfile
    {
        [JsonPropertyName("maritalStatus")]
        public string? MaritalStatus { get; set; }
        [JsonPropertyName("elder")]
        public string? Elder { get; set; }

    }

    public class BaseProfile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;
        [JsonPropertyName("contactDetails")]
        public ContactDetails? ContactDetails { get; set; }
        [JsonPropertyName("profession")]
        public Profession? Profession { get; set; }
        [JsonPropertyName("education")]
        public Education? Education { get; set; }
        [JsonPropertyName("otherDetails")]
        public string? OtherDetails { get; set; } = string.Empty;
    }
}
