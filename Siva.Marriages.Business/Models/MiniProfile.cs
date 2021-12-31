using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class SiblingsProfile : BaseProfile
    {
        public bool? IsMarried { get; set; } = null;
        public Elder? Elder { get; set; } = null;

    }

    public class BaseProfile
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public List<int> PhoneNumbers { get; set; } = new List<int>();
        public Profession? Profession { get; set; } = null;
        public Education? Education { get; set; } = null;
        public string? OtherDetails { get; set; } = null;
    }
}
