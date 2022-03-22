using Siva.Marriages.WebApp.Helpers;

namespace Siva.Marriages.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicturesController : ControllerBase
    {
        private ProfileOperations profileOperations;
        private ProfilePicturesOperations profilePicturesOperations;
        public ProfilePicturesController(ProfileOperations profileOperations, ProfilePicturesOperations profilePicturesOperations)
        {
            this.profileOperations = profileOperations;
            this.profilePicturesOperations = profilePicturesOperations;
        }

        // GET api/<ProfilePicturesController>/5
        [HttpGet("{photoId}")]
        public IActionResult Get(string photoId)
        {
            return File(profilePicturesOperations.GetPictureById(photoId), "image/jpeg");
        }

        [HttpGet("{profileId}/{photoId}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid profileId, string photoId)
        {
            try
            {
                return Ok(await profilePicturesOperations.MakePrimaryPhoto(profileId, photoId));
            }
            catch (AppDataException appExcep)
            {
                return StatusCode(appExcep.StatusCode, appExcep.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ProfilePicturesController>
        [HttpPost("{profileId}")]
        [Authorize]
        public async Task<IActionResult> Post(Guid profileId)
        {
            var files = Request.Form.Files;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    await profilePicturesOperations.AddPictureToProfile(profileId, formFile.FileName.Split('.').Last(), formFile.OpenReadStream());
                }
            }
            return Ok();
        }

        // DELETE api/<ProfilePicturesController>/5/1
        [HttpDelete("{profileId}/{photoId}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid profileId, string photoId)
        {
            await profilePicturesOperations.RemovePictureFromProfile(profileId, photoId);
            return Ok();
        }
    }
}
