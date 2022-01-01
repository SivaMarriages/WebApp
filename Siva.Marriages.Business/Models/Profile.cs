using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public ProfileData? Data { get; set; }
    }

    public class ProfileData : BaseProfile
    {
        public string? Surname { get; set; } = null;
        public string? MotherMaidenname { get; set; } = null;
        public BirthDetails? DateOfBirth { get; set; } = null;
        public string? NativePlace { get; set; } = null;
        public BaseProfile? Father { get; set; } = null;
        public BaseProfile? Mother { get; set; } = null;
        public List<SiblingsProfile> Siblings { get; set; } = new List<SiblingsProfile>();
        public decimal? Height { get; set; } = null;
        public List<DriveFile> Photos { get; set; } = new List<DriveFile>();
    }
}
