using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class BirthDetails
    {
        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; } = string.Empty;
        [JsonPropertyName("timeOfBirth")]
        public string TimeOfBirth { get; set; } = string.Empty;
        [JsonPropertyName("placeOfBirth")]
        public string PlaceOfBirth { get; set; } = string.Empty;
        [JsonPropertyName("rasi")]
        public string Rasi { get; set; } = string.Empty;
        [JsonPropertyName("nakshatra")]
        public string Nakshatra { get; set; } = string.Empty;
    }
}
