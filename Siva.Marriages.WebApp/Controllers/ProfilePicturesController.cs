
namespace Siva.Marriages.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicturesController : ControllerBase
    {
        private ProfilePicturesOperations profilePicturesOperations;
        public ProfilePicturesController(ProfilePicturesOperations profilePicturesOperations)
        {
            this.profilePicturesOperations = profilePicturesOperations;
        }

        // GET api/<ProfilePicturesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return File(profilePicturesOperations.GetPictureById(id), "application/octet-stream");
        }

        // POST api/<ProfilePicturesController>
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(Guid profileId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    await profilePicturesOperations.AddPictureToProfile(profileId, formFile.FileName.Split('.').Last(), formFile.OpenReadStream());
                }
            }
            return Ok("Uploaded!");
        }
                
        // DELETE api/<ProfilePicturesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await profilePicturesOperations.RemovePictureFromProfile(id);
            return Ok("Deleted!");
        }
    }
}
