using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.DataContext;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientMissionController : ControllerBase
    {
        private readonly BooksContext _context;

        public ClientMissionController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/ClientMission/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ClientMissionDto>>> GetClientMissions(int userId)
        {
            List<ClientMissionDto> clientSideMissionList;

            try
            {
                clientSideMissionList = await _context.Missions
                     .Include(m => m.Country)
            .Include(m => m.City)
            .Select(m => new ClientMissionDto
                
                    {
                        Id = m.Id,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        MissionOrganisationName = m.MissionOrganisationName,
                        CountryId = m.CountryId,
                        CountryName = m.Country.CountryName, // Assuming Country entity has CountryName property
                        CityId = m.CityId,
                        CityName = m.City.CityName, // Assuming City entity has CityName property
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionType = m.MissionType,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionSkillId = m.MissionSkillId,
                        MissionImages = m.MissionImages,
                        MissionDocuments = m.MissionDocuments,
                        MissionAvilability = m.MissionAvilability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionThemeName = m.MissionThemeId, // Placeholder for actual theme name
                        MissionSkillName = m.MissionSkillId, // Placeholder for actual skill names
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus = _context.MissionApplications.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                        MissionApproveStatus = _context.MissionApplications.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus = "0", // Placeholder for actual favorite status logic
                        Rating = 0 // Placeholder for actual rating logic
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return clientSideMissionList;
        }
    }

    public class ClientMissionDto
    {
        public int Id { get; set; }
        public string MissionTitle { get; set; }
        public string MissionDescription { get; set; }
        public string? MissionOrganisationDetail { get; set; }
        public string? MissionOrganisationName { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? MissionType { get; set; }
        public int? TotalSheets { get; set; }
        public DateTime? RegistrationDeadLine { get; set; }
        public string? MissionThemeId { get; set; }
        public string? MissionSkillId { get; set; }
        public string? MissionImages { get; set; }
        public string? MissionDocuments { get; set; }
        public string? MissionAvilability { get; set; }
        public string? MissionVideoUrl { get; set; }
        public string? MissionThemeName { get; set; }
        public string? MissionSkillName { get; set; }
        public string MissionStatus { get; set; }
        public string MissionApplyStatus { get; set; }
        public string MissionApproveStatus { get; set; }
        public string MissionDateStatus { get; set; }
        public string MissionDeadLineStatus { get; set; }
        public string MissionFavouriteStatus { get; set; }
        public int Rating { get; set; }
    }
}
