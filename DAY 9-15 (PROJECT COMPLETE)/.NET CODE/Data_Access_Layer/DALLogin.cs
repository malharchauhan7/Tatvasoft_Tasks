using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace Data_Access_Layer
{
    //[Authorize(Roles = "admin")]
    public class DALLogin
    {
        private readonly AppDbContext _cIDbContext;
        public DALLogin(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        /*public User LoginUser(User user)
        {
            User userObj = new User();
            try
            {
                    var query = from u in _cIDbContext.User
                                where u.EmailAddress == user.EmailAddress && u.IsDeleted == false
                                select new
                                {
                                    u.Id,
                                    u.FirstName,
                                    u.LastName,
                                    u.PhoneNumber,
                                    u.EmailAddress,
                                    u.UserType,
                                    u.Password,
                                    UserImage = u.UserImage
                                };

                    var userData = query.FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.Password == user.Password)
                        {
                            userObj.Id = userData.Id;
                            userObj.FirstName = userData.FirstName;
                            userObj.LastName = userData.LastName;
                            userObj.PhoneNumber = userData.PhoneNumber;
                            userObj.EmailAddress = userData.EmailAddress;
                            userObj.UserType = userData.UserType;
                            userObj.UserImage = userData.UserImage;
                            userObj.Message = "Login Successfully";
                        }
                        else
                        {
                            userObj.Message = "Incorrect Password.";
                        }
                    }
                    else
                    {
                        userObj.Message = "Email Address Not Found.";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userObj;
        }*/
        public User LoginUser(LoginUserModel user)
        {
            User userObj = new User();
            try
            {
                    var query = from u in _cIDbContext.User
                                join ud in _cIDbContext.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                from userDetail in userDetailGroup.DefaultIfEmpty()
                                where u.EmailAddress == user.EmailAddress && u.IsDeleted == false
                                select new User
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    PhoneNumber = u.PhoneNumber,
                                    EmailAddress = u.EmailAddress,
                                    UserType = u.UserType,
                                    Password = u.Password,
                                    UserImage = userDetail.UserImage == null ? "" : userDetail.UserImage
                                };

                    var userData = query.FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.Password == user.Password)
                        {
                            userObj.Id = userData.Id;
                            userObj.FirstName = userData.FirstName;
                            userObj.LastName = userData.LastName;
                            userObj.PhoneNumber = userData.PhoneNumber;
                            userObj.EmailAddress = userData.EmailAddress;
                            userObj.UserType = userData.UserType;
                            userObj.UserImage = userData.UserImage;
                            userObj.Message = "Login Successfully";
                        }
                        else
                        {
                            userObj.Message = "Incorrect Password.";
                        }
                    }
                    else
                    {
                        userObj.Message = "Email Address Not Found.";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userObj;
        }

        public string Register(AddUserModel user)
        {
            string result = string.Empty;
            try
            {
                bool emailExists = _cIDbContext.User.Any(u => u.EmailAddress == user.EmailAddress && !u.IsDeleted); // any returns boolean value
                if (!emailExists)
                {
                    string maxEmployeeIdStr = _cIDbContext.UserDetail.Max(ud => ud.EmployeeId);
                    int maxEmployeeId = 0;
                    if (!string.IsNullOrEmpty(maxEmployeeIdStr))
                    {
                        if(int.TryParse(maxEmployeeIdStr, out int parseEmployeeId))
                        {
                            maxEmployeeId = parseEmployeeId;
                        }
                        else
                        {
                            throw new Exception("Error while converting string to int.");
                        }
                    }
                    int newEmployeeId = maxEmployeeId + 1;

                    var newUser = new User
                    {
                        
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    _cIDbContext.User.Add(newUser);
                    _cIDbContext.SaveChanges();
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true,
                        CreatedDate = DateTime.UtcNow,
                    };
                    _cIDbContext.UserDetail.Add(newUserDetail);
                    _cIDbContext.SaveChanges();
                    result = "User Registered Successfully!";
                    
                }
                else
                {
                    throw new Exception("Email Already Exists");
                }
            }catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            return result;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                User user = await _cIDbContext.User.FirstAsync(x => x.Id == id && !x.IsDeleted);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<string> UpdateUserAsync(UpdateUserModel user)
        {
            try
            {
                string result = "";

                using (var transaction = await _cIDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var updatedUserDetail = await _cIDbContext.UserDetail.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        if (updatedUserDetail != null)
                        {
                            updatedUserDetail.Name = user.FirstName;
                            updatedUserDetail.Surname = user.LastName;
                            updatedUserDetail.ModifiedDate = DateTime.UtcNow;

                            // idk if we need to update notMapped columns, will see
                            updatedUserDetail.FirstName = user.FirstName;
                            updatedUserDetail.LastName = user.LastName;
                            updatedUserDetail.EmailAddress = user.EmailAddress;
                            updatedUserDetail.PhoneNumber = user.PhoneNumber;

                        }
                        var updatedUser = await _cIDbContext.User.FirstOrDefaultAsync(x => x.Id == user.Id);
                        if (updatedUser != null)
                        {
                            updatedUser.FirstName = user.FirstName;
                            updatedUser.LastName = user.LastName;
                            updatedUser.PhoneNumber = user.PhoneNumber;
                            updatedUser.EmailAddress = user.EmailAddress;
                            updatedUser.Password = user.Password;
                            updatedUser.ModifiedDate = DateTime.UtcNow;
                        }
                        await _cIDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "User Updated Successfully!";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in updating User", ex);
            }

        }

        public async Task<string> LoginUserProfileUpdate(UserDetail userDetail)
        {
            string result = "";
            try
            {

                using (var transaction = await _cIDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingUserDetail = await _cIDbContext.UserDetail.FirstOrDefaultAsync(u => u.Id == userDetail.Id && !u.IsDeleted);
                        if (existingUserDetail != null)
                        {
                            existingUserDetail.Name = userDetail.Name;
                            existingUserDetail.Surname = userDetail.Surname;
                            existingUserDetail.EmployeeId = userDetail.EmployeeId;
                            existingUserDetail.Manager = userDetail.Manager;
                            existingUserDetail.Title = userDetail.Title;
                            existingUserDetail.Department = userDetail.Department;
                            existingUserDetail.MyProfile = userDetail.MyProfile;
                            existingUserDetail.WhyIVolunteer = userDetail.WhyIVolunteer;
                            existingUserDetail.CountryId = userDetail.CountryId;
                            existingUserDetail.CityId = userDetail.CityId;
                            existingUserDetail.Avilability = userDetail.Avilability;
                            existingUserDetail.LinkdInUrl = userDetail.LinkdInUrl;
                            existingUserDetail.MySkills = userDetail.MySkills;
                            existingUserDetail.UserImage = userDetail.UserImage;
                            existingUserDetail.Status = userDetail.Status;
                            existingUserDetail.ModifiedDate = DateTime.UtcNow;
                            existingUserDetail.FirstName = userDetail.Name;
                            existingUserDetail.LastName = userDetail.Surname;
                        }
                        else
                        {
                            result = "Account Details not found";
                        }
                        var updatedUser = await _cIDbContext.User.FirstOrDefaultAsync(x => x.Id == userDetail.UserId && !x.IsDeleted);
                        if (updatedUser != null)
                        {
                            updatedUser.FirstName = userDetail.Name;
                            updatedUser.LastName = userDetail.Surname;
                            updatedUser.ModifiedDate = DateTime.UtcNow;
                            updatedUser.UserFullName = userDetail.Name + userDetail.Surname;
                        }
                        else
                        {
                            result = "User not found";
                        }
                        await _cIDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "Account Update Successfully!";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
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


        public async Task<UserDetail> GetUserProfileDetailById(int userId)
        {
            try
            {
                UserDetail userDetail = await _cIDbContext.UserDetail.FirstAsync(x => x.UserId == userId && !x.IsDeleted);
                if (userDetail == null)
                {
                    throw new Exception("User not found");
                }
                return userDetail;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<string> ChangePassword(ChangePassModel changePass)
        {
            string result = "";
            try
            {
                var existingUser = await _cIDbContext.User.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == changePass.UserId);
                if (existingUser != null)
                {
                    if(changePass.NewPassword != changePass.ConfirmPassword)
                    {
                        throw new Exception("password and confirm password not match");
                    }
                    else if(existingUser.Password == changePass.OldPassword)
                    {
                        existingUser.Password = changePass.NewPassword;
                        await _cIDbContext.SaveChangesAsync();
                        result = "Password changed successfully!";
                    }
                    else
                    {
                        throw new Exception("incorrect password!");
                    }
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
