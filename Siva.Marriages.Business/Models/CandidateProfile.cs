using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class CandidateProfile
    {
        public Guid Id { get; set; }
        public ProfileData Data { get; set; } = new();
        public List<string> PicturesId { get; set; } = new List<string>();
    }

    public class ProfileData : BaseProfile
    {
        [JsonPropertyName("surname")]
        public string Surname { get; set; } = string.Empty;
        [JsonPropertyName("motherMaidenname")]
        public string? MotherMaidenname { get; set; }
        [JsonPropertyName("birthDetails")]
        public BirthDetails BirthDetails { get; set; } = new();
        [JsonPropertyName("nativePlace")]
        public string? NativePlace { get; set; }
        [JsonPropertyName("father")]
        public BaseProfile Father { get; set; } = new();
        [JsonPropertyName("mother")]
        public BaseProfile Mother { get; set; } = new();
        [JsonPropertyName("siblings")]
        public List<SiblingsProfile> Siblings { get; set; } = new List<SiblingsProfile>();
        [JsonPropertyName("height")]
        public string? Height { get; set; }
    }
}
