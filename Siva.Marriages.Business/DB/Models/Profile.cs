using System;
using System.Collections.Generic;

namespace Siva.Marriages.Business.DB.Models
{
    public partial class Profile
    {
        public Profile()
        {
            ProfilePictures = new HashSet<ProfilePicture>();
        }

        public Guid Id { get; set; }
        public string Json { get; set; } = null!;

        public virtual ICollection<ProfilePicture> ProfilePictures { get; set; }
    }
}
