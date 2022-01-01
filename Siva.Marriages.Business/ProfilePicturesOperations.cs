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

        public Stream GetPictureById(string pictureId)
        {
            var stream = new MemoryStream();
            gDriveProvider.GetFileContent(pictureId, stream);
            return stream;
        }

        public async Task AddPictureToProfile(Guid profileId, string fileExtension, Stream content)
        {
            var dbProfile = await dbContext.Profiles.CountAsync(p => p.Id == profileId);
            if (dbProfile == 0)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Profile Not Found!" };
            var existingPhotosCount = await dbContext.ProfilePictures.CountAsync(pp => pp.ProfileId == profileId);
            var fileName = $"{profileId}_{existingPhotosCount}.{fileExtension}";
            var driveFile = await gDriveProvider.CreateFileAsync(fileExtension, content);
            dbContext.Add(new ProfilePicture() { Id = driveFile.Id, ProfileId = profileId });
            await dbContext.SaveChangesAsync();
        }

        public async Task RemovePictureFromProfile(string pictureId)
        {
            var pictureDetails = await dbContext.ProfilePictures.FirstOrDefaultAsync(pp => pp.Id == pictureId);
            if (pictureDetails == default)
                throw new AppDataException() { StatusCode = StatusCodes.Status404NotFound, Reason = "Picture Not Found!" };
            await gDriveProvider.DeleteFileAsync(pictureId);
            dbContext.ProfilePictures.Remove(pictureDetails);
            await dbContext.SaveChangesAsync();
        }
    }
}
