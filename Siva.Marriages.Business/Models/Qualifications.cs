using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class Profession
    {
        [JsonPropertyName("designation")]
        public string? Designation { get; set; }
        [JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }
        [JsonPropertyName("salary")]
        public string? Salary { get; set; }
        [JsonPropertyName("place")]
        public string? Place { get; set; }
    }

    public class Education
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("institute")]
        public string? Institute { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
    }

    public class ContactDetails
    {
        [JsonPropertyName("number1")]
        public string? Number1 { get; set; }
        [JsonPropertyName("number2")]
        public string? Number2 { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
