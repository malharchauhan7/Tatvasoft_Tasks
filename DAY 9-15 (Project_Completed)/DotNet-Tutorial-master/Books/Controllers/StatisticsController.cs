using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Books.DataContext;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly BooksContext _context;

        public StatisticsController(BooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = new
            {
                UserCount = await _context.Users.CountAsync(),
                MissionCount = await _context.Missions.CountAsync(),
                SkillCount = await _context.MissionSkills.CountAsync(),
                ThemeCount = await _context.MissionThemes.CountAsync(),
                CountryCount = await _context.Countries.CountAsync(),
                CityCount = await _context.Cities.CountAsync(),
                ApplicationCount = await _context.MissionApplications.CountAsync(),
            };

            return Ok(statistics);
        }
    }
}