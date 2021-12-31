using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class Profession
    {
        public string? Name { get; set; } = null;
        public string? Salary { get; set; } = null;
        public string? Place { get; set; } = null;
    }

    public class Education
    {
        public string? Name { get; set; }
        public string? Institute { get; set; }
        public string? Location { get; set; }
        public int? PassOutYear { get; set; }
    }
}
