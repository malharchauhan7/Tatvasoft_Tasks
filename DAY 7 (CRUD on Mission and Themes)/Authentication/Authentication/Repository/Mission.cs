using Authentication.Entities;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository
{
    public class Mission:IMission

    {
        private readonly AuthContext _authContext;

        public Mission(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<List<MissionViewModel>> GetMissionsWithDetails()
        {
            try
            {
              
                var missionsWithDetails = await _authContext.Missions.Select(mission => new MissionViewModel
                {
                    MissionId = mission.MissionId,
                    MissionTitle = mission.Title,
                    MissionDescription = mission.Description,
                    // CityName = _authContext.Cities.FirstOrDefault(c => c.CityId == mission.CityId).CityName,
                    //CountryName = _authContext.Countries.FirstOrDefault(c => c.CountryId == mission.CountryId).CountryName,
                    StartDate = mission.StartDate.ToString(),
                    EndDate = mission.EndDate.ToString(),
                    Deadline = mission.Deadline.ToString(),
                   
                    SeatsLeft = mission.SeatsLeft,
                    Challenge = mission.Challenge,
                    
                }).ToListAsync();

                return missionsWithDetails;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<string> CreateMission(MissionDto model)
        {
            var mission = new MissionDto
            {
                Title = model.Title,
                Introduction = model.Introduction,
                Description = model.Description,
                CityId = model.CityId,
                CountryId = model.CountryId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Deadline = model.Deadline,
                TotalSeats = model.TotalSeats,
                SeatsLeft = model.SeatsLeft,
                Challenge = model.Challenge,
                ThemeId = model.ThemeId,
            };

            _authContext.Missions.Add(mission);
            await _authContext.SaveChangesAsync();

            return "Mission Created Succesfully";
        }

        public async Task<MissionViewModel> GetMissionDetailsById(int missionId)
        {
            var missionWithDetails = await _authContext.Missions
                .Where(mission => mission.MissionId == missionId)
                .Select(mission => new MissionViewModel
                {
                    MissionId = mission.MissionId,
                    MissionTitle = mission.Title,
                    MissionDescription = mission.Description,
                   // CityName = _authContext.Cities.FirstOrDefault(c => c.CityId == mission.CityId).CityName,
                   // CountryName = _authContext.Countries.FirstOrDefault(c => c.CountryId == mission.CountryId).CountryName,
                    StartDate = mission.StartDate.ToString(),
                    EndDate = mission.EndDate.ToString(),
                    Deadline = mission.Deadline.ToString(),
                    SeatsLeft = mission.SeatsLeft,
                 //   OrganizationName = _authContext.Organizations.FirstOrDefault(r => r.OrganizationId == mission.OrganizationId).OrganizationName,
                 ////   Rating = _authContext.Organizations.FirstOrDefault(r => r.OrganizationId == mission.OrganizationId).Rating,
                  //  ImageURL = _authContext.MissionImage.FirstOrDefault(i => i.MissionId == mission.MissionId).ImageURL,
                 //   ThemeName = _authContext.MissionThemes.FirstOrDefault(t => t.ThemeId == mission.ThemeId).ThemeName,
                    Challenge = mission.Challenge,
                    MissionType = mission.MissionType,
                    MissionObject = mission.MissionObject,
                    MissionAchieved = mission.MissionAchieved,
                    Availability = mission.Availability,

            
                })
                .FirstOrDefaultAsync();

            return missionWithDetails;
        }

        public async Task<string> DeleteMission(int id)
       {
            var mission = await _authContext.Missions.FindAsync(id);
            if (mission == null)
            {
                return "Mission not found";
            }

            _authContext.Missions.Remove(mission);
           await _authContext.SaveChangesAsync();

           return "Mission Deleted Successfully";
        }

        public async Task<string> UpdateMission(int id, MissionDto model)
        {
            var mission = await _authContext.Missions.FindAsync(id);
            if (mission == null)
            {
                return "Mission not found";
            }

           
            if (!string.IsNullOrEmpty(model.Title))
            {
                mission.Title = model.Title;
            }

            if (!string.IsNullOrEmpty(model.Introduction))
            {
                mission.Introduction = model.Introduction;
            }

            if (!string.IsNullOrEmpty(model.Description))
            {
                mission.Description = model.Description;
            }

            if (model.CityId != 0)
            {
                mission.CityId = model.CityId;
            }

            if (model.CountryId != 0)
            {
                mission.CountryId = model.CountryId;
            }

            if (model.StartDate != default(DateTime))
            {
                mission.StartDate = model.StartDate;
            }

            if (model.EndDate != default(DateTime))
            {
                mission.EndDate = model.EndDate;
            }

            if (model.Deadline != default(DateTime))
            {
                mission.Deadline = model.Deadline;
            }

            if (model.TotalSeats != 0)
            {
                mission.TotalSeats = model.TotalSeats;
            }

            if (model.SeatsLeft != 0)
            {
                mission.SeatsLeft = model.SeatsLeft;
            }

            if (!string.IsNullOrEmpty(model.Challenge))
            {
                mission.Challenge = model.Challenge;
            }

            if (model.ThemeId != 0)
            {
                mission.ThemeId = model.ThemeId;
            }

            await _authContext.SaveChangesAsync();

            return "Mission Updated Successfully";
        }


    }
}
