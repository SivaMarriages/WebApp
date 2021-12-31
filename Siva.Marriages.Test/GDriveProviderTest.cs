using Siva.Marriages.Business;
using Siva.Marriages.Business.Models;
using System.IO;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace Siva.Marriages.Test
{
    public class GDriveProviderTest
    {
        private readonly GDriveProvider gDriveProvider;
        public GDriveProviderTest()
        {
            string secrets = "{\"installed\": {\"client_id\": \"716239568028-u3vtmuahreifgni1cjrsu4p35rlvo424.apps.googleusercontent.com\",\"project_id\": \"drivepoc-330206\",\"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\"token_uri\": \"https://oauth2.googleapis.com/token\",\"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\"client_secret\": \"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\",\"redirect_uris\": [ \"urn:ietf:wg:oauth:2.0:oob\", \"http://localhost\" ]}}";
            var dic = new System.Collections.Generic.Dictionary<string, string>();
            dic.Add("serviceAccountJson", secrets);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(dic).Build();
            
            gDriveProvider = new GDriveProvider(configuration);
        }

        [Fact]
        public void GetJsonFile()
        {

            //string id = "1RAbo2za22FcYo1xAceQuTd5ufP9oR9WmrCpAFSpxQfsHupBVPg";
            string id = "1x583yqNXgRUORmk-yQ75tkjcIFqi9aes";
            var stream = new MemoryStream();
            gDriveProvider.GetFileContent(id, stream);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            string json = Encoding.Unicode.GetString(bytes);
            var driveFile = Newtonsoft.Json.JsonConvert.DeserializeObject<DriveFile>(json);
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CreateFolder()
        {
            //child folder id = "1hEK-M0ClNR0sbIfOlV-4KVps94Ycy2pc"
            var file = gDriveProvider.CreateFolderAsync("testChildFolder", "1TKL_pD3IopIXgy3KVWkLnSt16zCUSYop").Result;
            Assert.False(string.IsNullOrEmpty(file.Id));
        }

        [Fact]
        public void UpdateFile()
        {
            var id = "1x583yqNXgRUORmk-yQ75tkjcIFqi9aes";

            var stream = new MemoryStream(Encoding.Unicode.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new DriveFile() { Name = "Kiran Kumar Valluri 🙂", Id = "🙁😠😡😞😟😣😖" })));
            var driveFile = gDriveProvider.UpdateFileAsync(new DriveFile() { Name = "config.json", Id = id }, stream).Result;
            Assert.Equal(id, driveFile.Id);
        }

        [Fact]
        public void CreateFile()
        {
            var stream = new MemoryStream(Encoding.Unicode.GetBytes("🙂"));
            var driveFile = gDriveProvider.CreateFileAsync("config.json", stream, "1TKL_pD3IopIXgy3KVWkLnSt16zCUSYop").Result;
            Assert.False(string.IsNullOrEmpty(driveFile.Id));
        }

        [Fact]
        public void GetFileIDsByName()
        {
            var list = gDriveProvider.GetFileIdByNameAsync("data.json").Result;
            list = gDriveProvider.GetFileIdByNameAsync("testFolder").Result;
            Assert.Single(list);
        }
    }
}