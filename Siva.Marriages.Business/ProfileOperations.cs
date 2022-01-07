using Microsoft.AspNetCore.Mvc;
using Siva.Marriages.Business.DB.Models;
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
        public ProfileOperations(PGSqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CandidateProfile>> GetProfilesAsync()
        {
            var profileBag = new ConcurrentBag<CandidateProfile>();
            await dbContext.Profiles.ForEachAsync(p =>
            {
                profileBag.Add(new CandidateProfile()
                {
                    Id = p.Id,
                    Data = JsonSerializer.Deserialize<ProfileData>(p.Data) ?? new(),
                    PicturesId = JsonSerializer.Deserialize<List<string>>(p.Pictures) ?? new()
                });
            });
            return profileBag;
        }

        public async Task<CandidateProfile> GetProfileAsync(Guid id)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            return new CandidateProfile()
            {
                Id = dbProfile.Id,
                Data = JsonSerializer.Deserialize<ProfileData>(dbProfile.Data) ?? new(),
                PicturesId = JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new()
            };
        }

        public async Task<Guid> AddProfileAsync(ProfileData value)
        {
            var profileId = Guid.NewGuid();
            dbContext.Add(new Profile() { Id = profileId, Data = JsonSerializer.Serialize(value), Pictures = JsonSerializer.Serialize(new List<string>()) });
            await dbContext.SaveChangesAsync();
            return profileId;
        }

        public async Task UpdateProfileAsync(Guid id, ProfileData value)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            dbProfile.Data = JsonSerializer.Serialize(value);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProfileAsync(Guid id, GDriveProvider gDriveProvider)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            try
            {
                Parallel.ForEach(JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new(), p =>
                {
                    gDriveProvider.DeleteFileAsync(p).Wait();
                });
            }
            catch
            {

            }
            dbContext.Profiles.Remove(dbProfile);
            await dbContext.SaveChangesAsync();
        }
    }
}
