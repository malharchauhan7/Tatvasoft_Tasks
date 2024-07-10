using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Authentication.Entities;
using Authentication.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController : ControllerBase
    {
        private readonly IMissionSkillRepository _missionSkillRepository;

        public MissionSkillController(IMissionSkillRepository missionSkillRepository)
        {
            _missionSkillRepository = missionSkillRepository;
        }

        [HttpGet("GetAllMissionSkill")]
        public async Task<ActionResult<IEnumerable<MissionSkillDto>>> GetAllMissionSkill()
        {
            return Ok(await _missionSkillRepository.GetAllMissionSkill());
        }

        [HttpGet("GetMissionSkillById/{id}")]
        public async Task<ActionResult<MissionSkillDto>> GetMissionSkillById(int id)
        {
            var missionSkill = await _missionSkillRepository.GetMissionSkillById(id);

            if (missionSkill == null)
            {
                return NotFound();
            }

            return Ok(missionSkill);
        }

        [HttpPost("CreateMissionSkill")]
        public async Task<ActionResult<MissionSkillDto>> PostMissionSkill(MissionSkillDto missionSkill)
        {
            await _missionSkillRepository.CreateMissionSkill(missionSkill);
            return CreatedAtAction("GetMissionSkill", new { id = missionSkill.Id }, missionSkill);
        }

        [HttpPut("UpdateMissionSkill/{id}")]
        public async Task<IActionResult> PutMissionSkill(int id, MissionSkillDto missionSkill)
        {
            if (id != missionSkill.Id)
            {
                return BadRequest();
            }

            await _missionSkillRepository.UpdateMissionSkill(missionSkill);
            return NoContent();
        }

        [HttpDelete("DeleteMissionSkill/{id}")]
        public async Task<IActionResult> DeleteMissionSkill(int id)
        {
            await _missionSkillRepository.DeleteMissionSkill(id);
            return NoContent();
        }
    }
}
