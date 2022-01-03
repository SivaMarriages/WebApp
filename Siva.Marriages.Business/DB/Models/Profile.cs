using System;
using System.Collections.Generic;

namespace Siva.Marriages.Business.DB.Models
{
    public partial class Profile
    {
        public Guid Id { get; set; }
        public string Data { get; set; } = null!;
        public string Pictures { get; set; } = null!;
    }
}
