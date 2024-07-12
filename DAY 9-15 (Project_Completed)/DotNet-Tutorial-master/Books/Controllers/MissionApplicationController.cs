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
    public class MissionApplicationController : ControllerBase
    {
        private readonly BooksContext _context;

        public MissionApplicationController(BooksContext context)
        {
            _context = context;
        }

        // POST: api/MissionApplication/Apply
        [HttpPost("Apply")]
        public async Task<ActionResult> ApplyMission([FromBody] MissionApplicationDto missionApplicationDto)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    var mission = await _context.Missions
                        .FirstOrDefaultAsync(m => m.Id == missionApplicationDto.MissionId);

                    if (mission != null)
                    {
                        if (mission.TotalSheets >= missionApplicationDto.Sheet)
                        {
                            var newApplication = new MissionApplication
                            {
                                MissionId = missionApplicationDto.MissionId,
                                UserId = missionApplicationDto.UserId,
                                AppliedDate = missionApplicationDto.AppliedDate,
                                Status = missionApplicationDto.Status,
                                Sheet = missionApplicationDto.Sheet,
                            };

                            _context.MissionApplications.Add(newApplication);
                            await _context.SaveChangesAsync();

                            mission.TotalSheets -= missionApplicationDto.Sheet;
                            _context.Entry(mission).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            await transaction.CommitAsync();
                            return Ok(new { success = true, message = "Mission applied successfully." });
                        }
                        else
                        {
                            return Ok(new { success = false, message = "Mission is full." });
                        }
                    }
                    else
                    {
                        return Ok(new { success = false, message = "Mission not found." });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while applying for the mission." });
            }
        }


        // GET: api/MissionApplication
        [HttpGet]
        public async Task<ActionResult<List<MissionApplicationDto>>> MissionApplicationList()
        {
            List<MissionApplicationDto> missionApplicationList;
            try
            {
                missionApplicationList = await _context.MissionApplications
                    .Join(_context.Missions,
                          ma => ma.MissionId,
                          m => m.Id,
                          (ma, m) => new { ma, m })
                    .Join(_context.Users,
                          mm => mm.ma.UserId,
                          u => u.Id,
                          (mm, u) => new MissionApplicationDto
                          {
                              Id = mm.ma.Id,
                              MissionId = mm.ma.MissionId,
                              MissionTitle = mm.m.MissionTitle,
                              UserId = u.Id,
                              UserName = u.FirstName + " " + u.LastName,
                              AppliedDate = mm.ma.AppliedDate,
                              Status = mm.ma.Status,
                              Sheet = mm.ma.Sheet
                          })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return missionApplicationList;
        }

        // DELETE: api/MissionApplication/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMissionApplication(int id)
        {
            try
            {
                var missionApplication = await _context.MissionApplications.FirstOrDefaultAsync(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    _context.MissionApplications.Remove(missionApplication);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "Record deleted successfully" });
                }
                else
                {
                    return NotFound(new { success = false, message = "Record not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the record" });
            }
        }

        // PUT: api/MissionApplication/Approve/{id}
        [HttpPut("Approve/{id}")]
        public async Task<ActionResult> ApproveMissionApplication(int id)
        {
            try
            {
                var missionApplication = await _context.MissionApplications.FirstOrDefaultAsync(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = true;
                    _context.Entry(missionApplication).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "Mission application approved" });
                }
                else
                {
                    return NotFound(new { success = false, message = "Mission application not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while approving the mission application" });
            }
        }


        // PUT: api/MissionApplication/Decline/{id}
        [HttpPut("Decline/{id}")]
        public async Task<ActionResult<string>> DeclineMissionApplication(int id)
        {
            try
            {
                var missionApplication = await _context.MissionApplications.FirstOrDefaultAsync(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = false;
                    _context.Entry(missionApplication).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return "Mission is declined";
                }
                else
                {
                    return "Mission application not found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class MissionApplicationDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public string? MissionTitle { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; }
        public int Sheet { get; set; }
    }
}
