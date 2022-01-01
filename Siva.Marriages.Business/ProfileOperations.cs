using Siva.Marriages.Business.DB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business
{
    public class ProfileOperations
    {
        private PGSqlDbContext dbContext;
        private GDriveProvider gDriveProvider;
        public ProfileOperations(PGSqlDbContext dbContext, GDriveProvider gDriveProvider)
        {
            this.dbContext = dbContext;
            this.gDriveProvider = gDriveProvider;
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            var profileBag = new ConcurrentBag<Profile>();
            await dbContext.Profiles.ForEachAsync(async p => {
                profileBag.Add(new Profile()
                {
                    Id = p.Id,
                    Data = await JsonSerializer.Deserialize<ProfileData>(p.json)
                });
            });
            return profileBag;
        }
    }
}
