using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class BirthDetails
    {
        public DateOnly DateOfBirth { get; set; }
        public TimeOnly? TimeOfBirth { get; set; } = null;
        public string? PlaceOfBirth { get; set; } = null;
        public Zodiac? Rasi { get; set; } = null;
        public Nakshatra? Nakshatra { get; set; } = null;
    }
}
