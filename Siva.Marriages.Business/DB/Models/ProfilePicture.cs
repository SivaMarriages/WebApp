using System;
using System.Collections.Generic;

namespace Siva.Marriages.Business.DB.Models
{
    public partial class ProfilePicture
    {
        public string Id { get; set; } = null!;
        public Guid ProfileId { get; set; }

        public virtual Profile Profile { get; set; } = null!;
    }
}
