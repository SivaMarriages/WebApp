using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Siva.Marriages.Business.Models;

namespace Siva.Marriages.Business
{
    public class GDriveProvider
    {

        public readonly DriveService DriveService;
        private readonly string PicturesFolderId;

        public GDriveProvider(IConfiguration configuration)
        {
            var serviceAccountJson = configuration.GetValue<string>("SERVICE_ACCOUNT_JSON");
            if (string.IsNullOrEmpty(serviceAccountJson))
                throw new ArgumentException("Without Google account, App Can't be Started");
            var credential = GoogleCredential.FromJson(serviceAccountJson)
                .CreateScoped(DriveService.Scope.Drive);

            PicturesFolderId = configuration.GetValue<string>("PICTURES_FOLDER_ID")??string.Empty;
            // Create Drive API service.
            DriveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Siva Marriages",
            });
        }

        public async Task<DriveFile> CreateFileAsync(string fileName, Stream content, string parentFolderId = "")
        {
            var file = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName
            };

            if (!string.IsNullOrEmpty(parentFolderId))
                file.Parents = new List<string>() { parentFolderId };
            else if(!string.IsNullOrEmpty(PicturesFolderId))
                file.Parents = new List<string>() { PicturesFolderId };

            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string mediaType);

            var creteRequest = DriveService.Files.Create(file, content, string.IsNullOrEmpty(mediaType) ? "" : mediaType);
            creteRequest.Fields = "id";
            var progress = await creteRequest.UploadAsync();
            if (progress.Status == UploadStatus.Completed)
                return new DriveFile()
                {
                    Id = creteRequest.ResponseBody.Id,
                    Name = fileName,
                };

            if(progress.Exception != null)
                throw progress.Exception;

            throw new Exception($"Status : {progress.Status} {Environment.NewLine} Unable to Create file");
        }

        public async Task<DriveFile> UpdateFileAsync(DriveFile driveFile, Stream content)
        {
            var file = new Google.Apis.Drive.v3.Data.File()
            {
                Name = driveFile.Name
            };

            new FileExtensionContentTypeProvider().TryGetContentType(driveFile.Name, out string mediaType);

            var creteRequest = DriveService.Files.Update(file, driveFile.Id, content, string.IsNullOrEmpty(mediaType) ? "" : mediaType);
            creteRequest.Fields = "id";
            var progress = await creteRequest.UploadAsync();
            if (progress.Status == UploadStatus.Completed)
                return new DriveFile()
                {
                    Id = creteRequest.ResponseBody.Id,
                    Name = driveFile.Name,
                };

            if (progress.Exception != null)
                throw progress.Exception;

            throw new Exception($"Status : {progress.Status} {Environment.NewLine} Unable to Update file");
        }

        public async Task<DriveFile> CreateFolderAsync(string folderName, string parentFolderId = "")
        {
            var folder = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            if (!string.IsNullOrEmpty(parentFolderId))
                folder.Parents = new List<string>() { parentFolderId };

            var creteRequest = DriveService.Files.Create(folder);
            creteRequest.Fields = "id";
            folder = await creteRequest.ExecuteAsync();
            return new DriveFile()
            {
                Id = folder.Id,
                Name = folderName,
            };
        }

        public Task<IDownloadProgress> GetFileContentAsync(string Id, Stream stream)
        {
            return DriveService.Files.Get(Id).DownloadAsync(stream);
        }

        public void GetFileContent(string Id, Stream stream)
        {
            DriveService.Files.Get(Id).Download(stream);
            stream.Position = 0;
        }

        public async Task<IEnumerable<string>> GetFileIdByNameAsync(string Name)
        {
            var filesListReq = DriveService.Files.List();
            filesListReq.Fields = "files(id, name)";
            filesListReq.Q = $"name='{Name}'";
            var filesList = await filesListReq.ExecuteAsync();

            return filesList.Files.Select(f => f.Id);
        }

        public async Task DeleteFileAsync(string Id)
        {
            await DriveService.Files.Delete(Id).ExecuteAsync();
        }
    }
}
