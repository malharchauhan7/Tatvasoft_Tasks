using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.DataContext;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly BooksContext _context;

        public MissionsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            return await _context.Missions.ToListAsync();
        }

        // GET: api/Missions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(int id, MissionDto missionDto)
        {
            if (id != missionDto.Id)
            {
                return BadRequest();
            }

            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            // Update only the properties present in the DTO
            mission.CountryId = missionDto.CountryId;
            mission.CityId = missionDto.CityId;
            mission.MissionDescription = missionDto.MissionDescription;
            mission.TotalSheets = missionDto.TotalSheets;
            mission.StartDate = missionDto.StartDate;
            mission.EndDate = missionDto.EndDate;
            mission.MissionImages = missionDto.MissionImages;
            mission.MissionSkillId = missionDto.MissionSkillId;
            mission.MissionTitle = mission.MissionTitle ?? "Default Mission Title"; // Set a default value if null
            mission.MissionOrganisationName = mission.MissionOrganisationName; // Leave as is
            mission.MissionOrganisationDetail = mission.MissionOrganisationDetail; // Leave as is
            mission.MissionType = mission.MissionType; // Leave as is
            mission.RegistrationDeadLine = mission.RegistrationDeadLine; // Leave as is
            mission.MissionThemeId = mission.MissionThemeId ?? "Default Theme"; // Set a default value if null
            mission.MissionDocuments = mission.MissionDocuments; // Leave as is
            mission.MissionAvilability = mission.MissionAvilability; // Leave as is
            mission.MissionVideoUrl = mission.MissionVideoUrl; // Leave as is

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> PostMission([FromBody] MissionDto missionDto)
        {
            Mission mission = new Mission
            {
                CountryId = missionDto.CountryId,
                CityId = missionDto.CityId,
                MissionDescription = missionDto.MissionDescription,
                TotalSheets = missionDto.TotalSheets,
                StartDate = missionDto.StartDate,
                EndDate = missionDto.EndDate,
                MissionImages = missionDto.MissionImages,
                MissionSkillId = missionDto.MissionSkillId,
                MissionTitle = missionDto.MissionTitle, // Set a default value for MissionTitle
                MissionOrganisationName = null, // Set to null or provide a default value
                MissionOrganisationDetail = null, // Set to null or provide a default value
                MissionType = null, // Set to null or provide a default value
                RegistrationDeadLine = null, // Set to null or provide a default value
                MissionThemeId = missionDto.MissionSkillId, // Set a default value or provide a better value
                MissionDocuments = null, // Set to null or provide a default value
                MissionAvilability = null, // Set to null or provide a default value
                MissionVideoUrl = null // Set to null or provide a default value
            };

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMission", new { id = mission.Id }, mission);
        }

        // DELETE: api/Missions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound(new { message = "Mission not found" });
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mission successfully deleted" });
        }


        private bool MissionExists(int id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }

    public class MissionDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string MissionDescription { get; set; }
        public int? TotalSheets { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? MissionImages { get; set; } // Ensure this is a string
        public string? MissionSkillId { get; set; }
        public string? MissionThemeId { get; set; } // Changed to string to match your frontend
        public string MissionTitle { get; set; }
        public string? MissionOrganisationName { get; set; }
        public string? MissionOrganisationDetail { get; set; }
        public string? CountryName { get; set; }
        public string? CityName { get; set; }
    }

}
