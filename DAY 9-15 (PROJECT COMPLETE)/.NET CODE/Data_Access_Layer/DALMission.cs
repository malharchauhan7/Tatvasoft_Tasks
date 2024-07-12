using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DALMission
    {
        private readonly AppDbContext _cIDbContext;

        public DALMission(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<Missions> MissionList()
        {
            return _cIDbContext.Missions.Where(x => !x.IsDeleted).ToList();
        }



        public async Task<Missions> GetMissionByIdAsync(int id)
        {
            return await _cIDbContext.Missions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public string AddMission(AddMissionModel mission)
        {
            string result = "";
            try
            {
                Missions missions = new Missions()
                {
                    MissionTitle = mission.MissionTitle,
                    MissionDescription = mission.MissionDescription,
                    MissionOrganisationName = mission.MissionOrganisationName,
                    MissionOrganisationDetail = mission.MissionOrganisationDetail,
                    CountryId = mission.CountryId,
                    CityId = mission.CityId,
                    StartDate = mission.StartDate,
                    EndDate = mission.EndDate,
                    MissionType = mission.MissionType,
                    TotalSheets = mission.TotalSheets,
                    RegistrationDeadLine = mission.RegistrationDeadLine,
                    MissionThemeId = mission.MissionThemeId,
                    MissionSkillId = mission.MissionSkillId,
                    MissionImages = mission.MissionImages,
                    MissionDocuments = mission.MissionDocuments,
                    MissionAvilability = mission.MissionAvilability,
                    MissionVideoUrl = mission.MissionVideoUrl,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };
                _cIDbContext.Missions.Add(missions);
                _cIDbContext.SaveChanges();
                result = "Mission Added Successfully!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> UpdateMissionAsync(UpdateMissionModel mission)
        {
            string result = "";
            try
            {
                var existingMission = await _cIDbContext.Missions.Where(x => x.Id == mission.Id).FirstOrDefaultAsync();
                if (existingMission != null)
                {
                    existingMission.MissionTitle = mission.MissionTitle;
                    existingMission.MissionDescription = mission.MissionDescription;
                    existingMission.MissionOrganisationName = mission.MissionOrganisationName;
                    existingMission.MissionOrganisationDetail = mission.MissionOrganisationDetail;
                    existingMission.CountryId = mission.CountryId;
                    existingMission.CityId = mission.CityId;
                    existingMission.StartDate = mission.StartDate;
                    existingMission.EndDate = mission.EndDate;
                    existingMission.MissionType = mission.MissionType;
                    existingMission.TotalSheets = mission.TotalSheets;
                    existingMission.RegistrationDeadLine = mission.RegistrationDeadLine;
                    existingMission.MissionThemeId = mission.MissionThemeId;
                    existingMission.MissionSkillId = mission.MissionSkillId;
                    existingMission.MissionImages = mission.MissionImages;
                    existingMission.MissionDocuments = mission.MissionDocuments;
                    existingMission.MissionAvilability = mission.MissionAvilability;
                    existingMission.MissionVideoUrl = mission.MissionVideoUrl;
                    existingMission.ModifiedDate = DateTime.UtcNow;

                    await _cIDbContext.SaveChangesAsync();
                    result = "Mission Updated Successfully!";
                }
                else
                {
                    throw new Exception("Mission Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating mission.", ex);
            }
            return result;
        }

        public async Task<string> DeleteMissionAsync(int id)
        {
            string result = "";
            try
            {
                var existingMission = await _cIDbContext.Missions.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existingMission != null)
                {
                    existingMission.IsDeleted = true;
                    await _cIDbContext.SaveChangesAsync();
                    result = "Mission Deleted Successfully!";
                }
                else
                {
                    throw new Exception("Mission Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting mission.", ex);
            }
            return result;
        }








        public async Task<List<MissionSkill>> GetMissionSkillsAsync()
        {
            return await _cIDbContext.MissionSkill.Where(x => !x.IsDeleted && x.Status != "inactive").ToListAsync();
        }
        public async Task<List<MissionTheme>> GetMissionThemesAsync()
        {
            return await _cIDbContext.MissionTheme.Where(x => !x.IsDeleted && x.Status != "inactive").ToListAsync();
        }










        /*clientSideMissionList = _cIDbContext.Missions
            .Where(m => !m.IsDeleted)
            .OrderBy(m => m.CreatedDate)
            .Select(async m => new Missions
            {
                Id = m.Id,
                CountryId = m.CountryId,
                CountryName = await _cIDbContext.Country.FirstOrDefaultAsync(c => c.Id == m.CountryId).CountryName,
                CityId = m.CityId,
                CityName = await _cIDbContext.City.FirstOrDefaultAsync(c => c.Id == m.CityId).CityName,
                MissionTitle = m.MissionTitle,
                MissionDescription = m.MissionDescription,
                MissionOrganisationName = m.MissionOrganisationName,
                MissionOrganisationDetail = m.MissionOrganisationDetail,
                TotalSheets = m.TotalSheets,
                RegistrationDeadLine = m.RegistrationDeadLine,
                MissionThemeId = m.MissionThemeId,
                MissionThemeName = await _cIDbContext.MissionTheme.FirstOrDefaultAsync(ms => ms.Id == int.Parse(m.MissionThemeId.ToString())).,
                MissionImages = m.MissionImages,
                MissionDocuments = m.MissionDocuments,
                MissionSkillId = m.MissionSkillId,
                MissionSkillName =  string.Join(",", await _cIDbContext.MissionSkill.FirstOrDefaultAsync(ms => ms.Id == int.Parse(m.MissionSkillId.ToString())).SkillName),
                MissionAvilability = m.MissionAvilability,
                MissionVideoUrl = m.MissionVideoUrl,
                MissionType = m.MissionType,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                MissionApplyStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                MissionApproveStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status == true) ? "Approved" : "Applied",
                MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                MissionFavouriteStatus = _cIDbContext.MissionFavourites.Any(mf => mf.MissionId == m.Id && mf.UserId == userId) ? "1" : "0",
                Rating = _cIDbContext.MissionRating.FirstOrDefault(mr => mr.MissionId == m.Id && mr.UserId == userId).Rating ?? 0
            }).ToList();*/
        public async Task<List<Missions>> ClientSideMissionList(int userId)
        {
            List<Missions> clientSideMissionList = new List<Missions>();
            try
            {
                var missions = await _cIDbContext.Missions
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync(); // Fetch data asynchronously

                foreach (var m in missions)
                {
                    var country = await _cIDbContext.Country.FirstOrDefaultAsync(c => c.Id == m.CountryId);
                    var city = await _cIDbContext.City.FirstOrDefaultAsync(c => c.Id == m.CityId);

                    int.TryParse(m.MissionThemeId.ToString(), out int missionThemeId);
                    var missionTheme = await _cIDbContext.MissionTheme.FirstOrDefaultAsync(ms => ms.Id == missionThemeId);

                    /*int.TryParse(m.MissionSkillId.ToString(), out int missionSkillId);*/
                    var ids = m.MissionSkillId.Split(',').Select(int.Parse).ToList();
                    var skillNames = await _cIDbContext.MissionSkill
                        .Where(ms => ids.Contains(ms.Id))
                        .Select(ms => ms.SkillName)
                        .ToListAsync();

                    var missionRating = await _cIDbContext.MissionRating.FirstOrDefaultAsync(mr => mr.MissionId == m.Id && mr.UserId == userId);

                    clientSideMissionList.Add(new Missions
                    {
                        Id = m.Id,
                        CountryId = m.CountryId,
                        CountryName = country?.CountryName,
                        CityId = m.CityId,
                        CityName = city?.CityName,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationName = m.MissionOrganisationName,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionThemeName = missionTheme?.ThemeName,
                        MissionImages = m.MissionImages,
                        MissionDocuments = m.MissionDocuments,
                        MissionSkillId = m.MissionSkillId,
                        MissionSkillName = skillNames != null ? string.Join(",", skillNames) : string.Empty,
                        MissionAvilability = m.MissionAvilability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionType = m.MissionType,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus =  _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && !ma.IsDeleted) ? "Applied" : "Apply", 
                        MissionApproveStatus = await _cIDbContext.MissionApplication.AnyAsync(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status == true && !ma.IsDeleted) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus = await _cIDbContext.MissionFavourites.AnyAsync(mf => mf.MissionId == m.Id && mf.UserId == userId) ? "1" : "0",
                        Rating = missionRating?.Rating ?? 0,
                        CreatedDate = m.CreatedDate,
                        ModifiedDate = m.ModifiedDate,
                        IsDeleted = m.IsDeleted,

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientSideMissionList;
        }


        public async Task<string> ApplyMission(MissionApplication missionApplication)
        {
            string result = "";
            try
            {
                using(var transaction = _cIDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var mission = await _cIDbContext.Missions.FirstOrDefaultAsync(m => m.Id == missionApplication.MissionId && !m.IsDeleted);
                        if (mission != null)
                        {
                            if(mission.TotalSheets > 0)
                            {
                                var newApplication = new MissionApplication
                                {
                                    MissionId = missionApplication.MissionId,
                                    UserId = missionApplication.UserId,
                                    AppliedDate = DateTime.UtcNow,
                                    Status = missionApplication.Status,
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false
                                };
                                await _cIDbContext.MissionApplication.AddAsync(newApplication);
                                await _cIDbContext.SaveChangesAsync();


                                mission.TotalSheets -= 1;
                                await _cIDbContext.SaveChangesAsync();

                                result = "Mission Apply Successfully!";
                            }
                            else
                            {
                                result = "Mission Housefull";
                            }
                        }
                        else
                        {
                            result = "Mission Not Found";
                        }
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return result;
        }





        public async Task<List<MissionApplication>> GetMissionApplicationList()
        {
            /*return await _cIDbContext.MissionApplication.Join(_cIDbContext.Missions, ma => ma.MissionId, m => m.Id, (ma,m) => new MissionApplication
            {
                AppliedDate = ma.AppliedDate,
                MissionTitle = m.MissionTitle,
                
            }).Where(ma => !ma.IsDeleted).ToListAsync();*/

            var data = await _cIDbContext.MissionApplication
            .Join(
                _cIDbContext.Missions,
                ma => ma.MissionId,
                m => m.Id,
                (ma, m) => new { MissionApplication = ma, Mission = m }
            ).Join(
                _cIDbContext.UserDetail,
                prevJoined => prevJoined.MissionApplication.UserId,
                ud => ud.UserId,
                (prev, ud) => new {Prev = prev, UserDetail = ud}
            )
            .Where(joined => !joined.Prev.Mission.IsDeleted && !joined.Prev.MissionApplication.IsDeleted)
            .ToListAsync();
            var result = data.Select(joined => new MissionApplication
            {
                Id = joined.Prev.MissionApplication.Id,
                MissionId = joined.Prev.Mission.Id,
                MissionTitle = joined.Prev.Mission.MissionTitle,
                AppliedDate = joined.Prev.MissionApplication.AppliedDate,
                Status = joined.Prev.MissionApplication.Status,
                CreatedDate = joined.Prev.MissionApplication.CreatedDate,
                UserId = joined.Prev.MissionApplication.UserId,
                IsDeleted = joined.Prev.MissionApplication.IsDeleted,
                UserName = joined.UserDetail.Name + " " + joined.UserDetail.Surname
                // Add other necessary properties here
            }).ToList();

            return result;
        }


        public async Task<string> MissionApplicationApprove(MissionApplication missionApplication)
        {
            var result = "";
            try
            {
                var existingMissionApplication = await _cIDbContext.MissionApplication.FirstOrDefaultAsync(ma => ma.Id == missionApplication.Id);
                if(existingMissionApplication != null)
                {
                    existingMissionApplication.Status = true;
                    await _cIDbContext.SaveChangesAsync();
                    result = "Mission is Approved";
                }
                else
                {
                    result = "Mission Application Not Found";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<string> MissionApplicationDelete(int id)
        {
            var result = "";
            try
            {
                var existingMissionApplication = await _cIDbContext.MissionApplication.FirstOrDefaultAsync(ma => ma.Id == id);
                if (existingMissionApplication != null)
                {
                    existingMissionApplication.IsDeleted = true;
                    var mission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == existingMissionApplication.MissionId);
                    if (mission != null)
                    {
                        mission.TotalSheets += 1;
                    }
                    await _cIDbContext.SaveChangesAsync();
                    result = "Mission Application deleted for a user";
                }
                else
                {
                    result = "Mission Application Not Found";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<List<Missions>> MissionClientList(SortMissions sortMissions)
        {
            
            List<Missions> clientSideMissionList = new List<Missions>();
            try
            {
                var missionsQuery = _cIDbContext.Missions.Where(m => !m.IsDeleted);

                switch (sortMissions.SortestValue)
                {
                    case "Newest":
                        missionsQuery = missionsQuery.OrderByDescending(m => m.CreatedDate);
                        break;
                    case "Lowest available seats":
                        missionsQuery = missionsQuery.OrderBy(m => m.TotalSheets);
                        break;
                    case "Highest available seats":
                        missionsQuery = missionsQuery.OrderByDescending(m => m.TotalSheets);
                        break;
                    case "Registration deadline":
                        missionsQuery = missionsQuery.OrderBy(m => m.RegistrationDeadLine);
                        break;
                    case "Oldest":
                        missionsQuery = missionsQuery.OrderBy(m => m.CreatedDate);
                        break;
                    default:
                        // Default sorting if needed
                        missionsQuery = missionsQuery.OrderBy(m => m.CreatedDate);
                        break;
                }

                var missions = await missionsQuery.ToListAsync();

                foreach (var m in missions)
                {
                    var country = await _cIDbContext.Country.FirstOrDefaultAsync(c => c.Id == m.CountryId);
                    var city = await _cIDbContext.City.FirstOrDefaultAsync(c => c.Id == m.CityId);

                    int.TryParse(m.MissionThemeId.ToString(), out int missionThemeId);
                    var missionTheme = await _cIDbContext.MissionTheme.FirstOrDefaultAsync(ms => ms.Id == missionThemeId);

                    /*int.TryParse(m.MissionSkillId.ToString(), out int missionSkillId);*/
                    var ids = m.MissionSkillId.Split(',').Select(int.Parse).ToList();
                    var skillNames = await _cIDbContext.MissionSkill
                        .Where(ms => ids.Contains(ms.Id))
                        .Select(ms => ms.SkillName)
                        .ToListAsync();

                    var missionRating = await _cIDbContext.MissionRating.FirstOrDefaultAsync(mr => mr.MissionId == m.Id && mr.UserId == sortMissions.UserId);

                    clientSideMissionList.Add(new Missions
                    {
                        Id = m.Id,
                        CountryId = m.CountryId,
                        CountryName = country?.CountryName,
                        CityId = m.CityId,
                        CityName = city?.CityName,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationName = m.MissionOrganisationName,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionThemeName = missionTheme?.ThemeName,
                        MissionImages = m.MissionImages,
                        MissionDocuments = m.MissionDocuments,
                        MissionSkillId = m.MissionSkillId,
                        MissionSkillName = skillNames != null ? string.Join(",", skillNames) : string.Empty,
                        MissionAvilability = m.MissionAvilability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionType = m.MissionType,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus =  _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == sortMissions.UserId && !ma.IsDeleted) ? "Applied" : "Apply", 
                        MissionApproveStatus = await _cIDbContext.MissionApplication.AnyAsync(ma => ma.MissionId == m.Id && ma.UserId == sortMissions.UserId && ma.Status == true && !ma.IsDeleted) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus = await _cIDbContext.MissionFavourites.AnyAsync(mf => mf.MissionId == m.Id && mf.UserId == sortMissions.UserId) ? "1" : "0",
                        Rating = missionRating?.Rating ?? 0,
                        CreatedDate = m.CreatedDate,
                        ModifiedDate = m.ModifiedDate,
                        IsDeleted = m.IsDeleted,

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientSideMissionList;
        }
    }
}
