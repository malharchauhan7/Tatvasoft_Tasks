using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Entities;
using Authentication.Migrations;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository
{
    public class MissionSkillRepository : IMissionSkillRepository
    {
        private readonly AuthContext _authContext;

        public MissionSkillRepository(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<IEnumerable<MissionSkillDto>> GetAllMissionSkill()
        {
            try
            {
                var missionSkills = await _authContext.MissionSkills.Select(missionSkill => new MissionSkillDto
                {
                    Id = missionSkill.Id,
                    SkillName = missionSkill.SkillName,
                    Status = missionSkill.Status,
                    // Add other properties as needed
                }).ToListAsync();

                return missionSkills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CreateMissionSkill(MissionSkillDto model)
        {
            var missionSkill = new MissionSkillDto
            {
                Id = model.Id,
                SkillName = model.SkillName,
                Status = model.Status,
                // Add other properties as needed
            };

            _authContext.MissionSkills.Add(missionSkill);
            await _authContext.SaveChangesAsync();

            return "MissionSkill Created Successfully";
        }

        public async Task<MissionSkillDto> GetMissionSkillById(int missionSkillId)
        {
            var missionSkill = await _authContext.MissionSkills.FindAsync(missionSkillId);
            if (missionSkill == null)
            {
                return null;
            }

            var missionSkillDto = new MissionSkillDto
            {
                Id = missionSkill.Id,
                SkillName = missionSkill.SkillName,
                Status = missionSkill.Status,
                // Add other properties as needed
            };

            return missionSkillDto;
        }

        public async Task<string> DeleteMissionSkill(int id)
        {
            var missionSkill = await _authContext.MissionSkills.FindAsync(id);
            if (missionSkill == null)
            {
                return "MissionSkill not found";
            }

            _authContext.MissionSkills.Remove(missionSkill);
            await _authContext.SaveChangesAsync();

            return "MissionSkill Deleted Successfully";
        }

        public async Task<string> UpdateMissionSkill(int id, MissionSkillDto model)
        {
            var missionSkill = await _authContext.MissionSkills.FindAsync(id);
            if (missionSkill == null)
            {
                return "MissionSkill not found";
            }

            missionSkill.SkillName = model.SkillName;
            missionSkill.Status = model.Status;
            // Update other properties as needed

            await _authContext.SaveChangesAsync();

            return "MissionSkill Updated Successfully";
        }

      
    }
}
