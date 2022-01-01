using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business.Models
{
    public class DataJson
    {
        public DriveFile PicturesFolder { get; set; } = new DriveFile();
        public List<CandidateProfile> Profiles { get; set; } = new List<CandidateProfile>();
    }
}
