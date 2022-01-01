
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siva.Marriages.Business;
using Siva.Marriages.Business.DB;
using Siva.Marriages.Business.Models;
using System.Text.Json;

namespace Siva.Marriages.WebApp.Controllers
{
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly PGSqlDbContext dbContext;
        private readonly ProfileOperations operations;
        public ProfileController(PGSqlDbContext dbContext, ProfileOperations operations)
        {
            this.dbContext = dbContext;
            this.operations = operations;
        }

        // GET: api/<ProfileController>
        [HttpGet("api/[controller]")]
        public async Task<IEnumerable<Profile>> Get()
        {
            return await operations.GetProfiles();
        }

        // GET api/<ProfileController>/5
        [HttpGet("api/[controller]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if(dbProfile == default)
            {
                return NotFound("Profile Not Found!");
            }
            return Ok(new Profile() { Id = dbProfile.Id, Data = JsonSerializer.Deserialize<ProfileData>(dbProfile.Json) });
        }

        // POST api/<ProfileController>
        [HttpPost("api/[controller]")]
        public async Task<IActionResult> Post([FromBody] ProfileData value)
        {
            dbContext.Add(new Business.DB.Models.Profile() { Id = Guid.NewGuid(), Json = JsonSerializer.Serialize(value)});
            await dbContext.SaveChangesAsync();
            return Ok("Saved!");
        }

        // PUT api/<ProfileController>/5
        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProfileData value)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (dbProfile == default)
            {
                return NotFound("Profile Not Found!");
            }
            dbProfile.Json = JsonSerializer.Serialize(value);
            await dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("api/[controller]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dbProfile = await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (dbProfile == default)
            {
                return NotFound("Profile Not Found!");
            }
            dbContext.Profiles.Remove(dbProfile);
            await dbContext.SaveChangesAsync();
            return Ok("Deleted!");
        }
    }
}
