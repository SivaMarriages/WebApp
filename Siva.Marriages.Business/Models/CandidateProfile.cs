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

    public class ProfileData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("surname")]
        public string Surname { get; set; } = string.Empty;
        [JsonPropertyName("motherMaidenname")]
        public string? MotherMaidenname { get; set; }
        [JsonPropertyName("nativePlace")]
        public string? NativePlace { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;
        [JsonPropertyName("contactDetails")]
        public ContactDetails? ContactDetails { get; set; }
        [JsonPropertyName("birthDetails")]
        public BirthDetails BirthDetails { get; set; } = new();
        [JsonPropertyName("profession")]
        public Profession? Profession { get; set; }
        [JsonPropertyName("education")]
        public Education? Education { get; set; }
        [JsonPropertyName("height")]
        public string? Height { get; set; }
        [JsonPropertyName("otherDetails")]
        public string? OtherDetails { get; set; } = string.Empty;
        [JsonPropertyName("father")]
        public string? Father { get; set; } = string.Empty;
        [JsonPropertyName("mother")]
        public string? Mother { get; set; } = string.Empty;
        [JsonPropertyName("siblings")]
        public List<SiblingsProfile> Siblings { get; set; } = new List<SiblingsProfile>();
    }

    public class SiblingsProfile
    {
        [JsonPropertyName("details")]
        public string Details { get; set; } = string.Empty;
    }
}
