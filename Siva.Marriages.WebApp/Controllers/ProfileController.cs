
using Microsoft.AspNetCore.Authorization;

namespace Siva.Marriages.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileOperations operations;
        public ProfileController(ProfileOperations operations)
        {
            this.operations = operations;
        }

        // GET: api/<ProfileController>
        [HttpGet]
        public async Task<IEnumerable<CandidateProfile>> Get()
        {
            return await operations.GetProfilesAsync();
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await operations.GetProfileAsync(id));
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

        // POST api/<ProfileController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfileData value)
        {
            await operations.AddProfileAsync(value);
            return Ok();
        }

        // PUT api/<ProfileController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProfileData value)
        {
            try
            {
                await operations.UpdateProfileAsync(id, value);
                return Ok();
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

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await operations.DeleteProfileAsync(id);
                return Ok();
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
    }
}
