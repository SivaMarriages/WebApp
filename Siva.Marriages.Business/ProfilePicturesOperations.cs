using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business
{
    public class ProfilePicturesOperations
    {
        private readonly GDriveProvider gDriveProvider;
        private readonly PGSqlDbContext dbContext;
        public ProfilePicturesOperations(GDriveProvider gDriveProvider, PGSqlDbContext dbContext)
        {
            this.gDriveProvider = gDriveProvider;
            this.dbContext = dbContext;
        }

        public Stream GetPictureById(string photoId)
        {
            var stream = new MemoryStream();
            gDriveProvider.GetFileContent(photoId, stream);
            return stream;
        }

        public async Task<List<string>> GetAllPhotoIds(Guid profileId)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            return JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new();
        }

        public async Task<List<string>> MakePrimaryPhoto(Guid profileId, string photoId)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            var existingPhotos = JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new();
            if (existingPhotos.Count == 0)
            {
                return new();
            }

            if (!existingPhotos.Remove(photoId))
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Picture Not Found!" };
            existingPhotos.Insert(0, photoId);
            dbProfile.Pictures = JsonSerializer.Serialize(existingPhotos);
            await dbContext.SaveChangesAsync();
            return existingPhotos;
        }

        public async Task AddPictureToProfile(Guid profileId, string fileExtension, Stream content)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            var existingPhotos = JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new();
            var fileName = $"{profileId}_{existingPhotos.Count}.{fileExtension}";
            var driveFile = await gDriveProvider.CreateFileAsync(fileName, content);
            existingPhotos.Add(driveFile.Id);
            dbProfile.Pictures = JsonSerializer.Serialize(existingPhotos);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemovePictureFromProfile(Guid profileId, string photoId)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
            if (dbProfile == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            var picturesList = JsonSerializer.Deserialize<List<string>>(dbProfile.Pictures) ?? new();
            if (!picturesList.Remove(photoId))
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Picture Not Found!" };
            dbProfile.Pictures = JsonSerializer.Serialize(picturesList);
            await dbContext.SaveChangesAsync();
            await gDriveProvider.DeleteFileAsync(photoId);
        }
    }
}
