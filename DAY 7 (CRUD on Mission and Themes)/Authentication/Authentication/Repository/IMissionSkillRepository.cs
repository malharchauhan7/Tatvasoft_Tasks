using Authentication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Authentication.Repository
{
    public interface IMissionSkillRepository
    {

        Task<IEnumerable<MissionSkillDto>> GetAllMissionSkill();
        Task<MissionSkillDto> GetMissionSkillById(int id);
        Task<MissionSkillDto> CreateMissionSkill(MissionSkillDto missionSkill);
        Task<MissionSkillDto> UpdateMissionSkill(MissionSkillDto missionSkill);
        Task DeleteMissionSkill(int id);
    }
}
