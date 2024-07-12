using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DALCommon
    {
        private readonly AppDbContext _cIDbContext;

        public DALCommon(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _cIDbContext.Country.ToListAsync();
        }

        public async Task<List<City>> GetCitiesAsync(int id)
        {
            return await _cIDbContext.City.Where(x => x.CountryId == id).ToListAsync();
        }



        public async Task<string> GetUserSkill(int userId)
        {
           string skillList = String.Empty;
            try
            {
                UserDetail userDetail = await _cIDbContext.UserDetail.FirstAsync(x => x.UserId == userId && !x.IsDeleted);
                if (userDetail != null)
                {
                    if (userDetail.MySkills != null && userDetail.MySkills.Length > 0)
                    {
                        skillList = userDetail.MySkills;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

            return skillList;
        }
    }
}
