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
    public class MissionSkillController : ControllerBase
    {
        private readonly BooksContext _context;

        public MissionSkillController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/MissionSkill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionSkill>>> GetMissionSkills()
        {
            return await _context.MissionSkills.ToListAsync();
        }

        // GET: api/MissionSkill/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionSkill>> GetMissionSkill(int id)
        {
            var missionSkill = await _context.MissionSkills.FindAsync(id);

            if (missionSkill == null)
            {
                return NotFound(new { Message = "Mission skill not found" });
            }

            return missionSkill;
        }

        // POST: api/MissionSkill
        [HttpPost]
        public async Task<ActionResult<MissionSkill>> PostMissionSkill(MissionSkill missionSkill)
        {
            _context.MissionSkills.Add(missionSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMissionSkill), new { id = missionSkill.Id }, missionSkill);
        }

        // PUT: api/MissionSkill/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissionSkill(int id, MissionSkill missionSkill)
        {
            if (id != missionSkill.Id)
            {
                return BadRequest(new { Message = "ID mismatch" });
            }

            _context.Entry(missionSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionSkillExists(id))
                {
                    return NotFound(new { Message = "Mission skill not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Result = 1, Data = "Mission skill updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissionSkill(int id)
        {
            var missionSkill = await _context.MissionSkills.FindAsync(id);
            if (missionSkill == null)
            {
                return NotFound(new { Message = "Mission skill not found" });
            }

            _context.MissionSkills.Remove(missionSkill);
            await _context.SaveChangesAsync();

            // Return a success message
            return Ok(new { Data = "Mission skill deleted successfully" });
        }

        private bool MissionSkillExists(int id)
        {
            return _context.MissionSkills.Any(e => e.Id == id);
        }
    }
}
