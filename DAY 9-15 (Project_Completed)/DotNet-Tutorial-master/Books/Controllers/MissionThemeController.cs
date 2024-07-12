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
    public class MissionThemeController : ControllerBase
    {
        private readonly BooksContext _context;

        public MissionThemeController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/MissionTheme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionTheme>>> GetMissionThemes()
        {
            return await _context.MissionThemes.ToListAsync();
        }

        // GET: api/MissionTheme/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionTheme>> GetMissionTheme(int id)
        {
            var missionTheme = await _context.MissionThemes.FindAsync(id);

            if (missionTheme == null)
            {
                return NotFound(new { Message = "Mission theme not found" });
            }

            return missionTheme;
        }

        // POST: api/MissionTheme
        [HttpPost]
        public async Task<ActionResult<MissionTheme>> PostMissionTheme(MissionTheme missionTheme)
        {
            _context.MissionThemes.Add(missionTheme);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMissionTheme), new { id = missionTheme.Id }, missionTheme);
        }

        // PUT: api/MissionTheme/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissionTheme(int id, MissionTheme missionTheme)
        {
            if (id != missionTheme.Id)
            {
                return BadRequest(new { Message = "ID mismatch" });
            }

            _context.Entry(missionTheme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionThemeExists(id))
                {
                    return NotFound(new { Message = "Mission theme not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Result = 1, Data = "Mission theme updated successfully" });
        }

        // DELETE: api/MissionTheme/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissionTheme(int id)
        {
            var missionTheme = await _context.MissionThemes.FindAsync(id);
            if (missionTheme == null)
            {
                return NotFound(new { Message = "Mission theme not found" });
            }

            _context.MissionThemes.Remove(missionTheme);
            await _context.SaveChangesAsync();

            return Ok(new { Data = "Mission theme deleted successfully" });
        }

        private bool MissionThemeExists(int id)
        {
            return _context.MissionThemes.Any(e => e.Id == id);
        }
    }
}
