using Books.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly BooksContext _context;

        public UserProfileController(BooksContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfileById(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var userDetails = user.UserDetails.FirstOrDefault();

            var result = new
            {
                User = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.EmailAddress,
                    user.UserType,
                    user.Password,
                    user.ConfirmPassword,
                    user.Uid,
                    user.Message,
                    user.UserImage,
                    user.UserFullName
                },
                UserDetails = userDetails != null ? new
                {
                    userDetails.Id,
                    userDetails.UserId,
                    userDetails.Name,
                    userDetails.Surname,
                    userDetails.EmployeeId,
                    userDetails.Manager,
                    userDetails.Title,
                    userDetails.Department,
                    userDetails.MyProfile,
                    userDetails.WhyIVolunteer,
                    userDetails.CountryId,
                    userDetails.CityId,
                    userDetails.Availability,
                    userDetails.LinkdInUrl,
                    userDetails.MySkills,
                    userDetails.UserImage,
                    userDetails.Status
                } : null
            };

            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutProfileById(int userId, UserProfileDto userProfileDto)
        {
            var user = await _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Update User properties if they are not null
            if (!string.IsNullOrEmpty(userProfileDto.User.FirstName))
                user.FirstName = userProfileDto.User.FirstName;
            if (!string.IsNullOrEmpty(userProfileDto.User.LastName))
                user.LastName = userProfileDto.User.LastName;
            if (!string.IsNullOrEmpty(userProfileDto.User.PhoneNumber))
                user.PhoneNumber = userProfileDto.User.PhoneNumber;
            if (!string.IsNullOrEmpty(userProfileDto.User.EmailAddress))
                user.EmailAddress = userProfileDto.User.EmailAddress;
            if (!string.IsNullOrEmpty(userProfileDto.User.UserType))
                user.UserType = userProfileDto.User.UserType;
            if (!string.IsNullOrEmpty(userProfileDto.User.Password))
                user.Password = userProfileDto.User.Password;
            if (!string.IsNullOrEmpty(userProfileDto.User.ConfirmPassword))
                user.ConfirmPassword = userProfileDto.User.ConfirmPassword;
            if (!string.IsNullOrEmpty(userProfileDto.User.FirstName) && !string.IsNullOrEmpty(userProfileDto.User.LastName))
                user.UserFullName = $"{userProfileDto.User.FirstName} {userProfileDto.User.LastName}";

            var userDetail = user.UserDetails.FirstOrDefault();
            if (userDetail != null)
            {
                // Update UserDetail properties if they are not null
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Name))
                    userDetail.Name = userProfileDto.UserDetails.Name;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Surname))
                    userDetail.Surname = userProfileDto.UserDetails.Surname;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.EmployeeId))
                    userDetail.EmployeeId = userProfileDto.UserDetails.EmployeeId;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Manager))
                    userDetail.Manager = userProfileDto.UserDetails.Manager;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Title))
                    userDetail.Title = userProfileDto.UserDetails.Title;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Department))
                    userDetail.Department = userProfileDto.UserDetails.Department;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.MyProfile))
                    userDetail.MyProfile = userProfileDto.UserDetails.MyProfile;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.WhyIVolunteer))
                    userDetail.WhyIVolunteer = userProfileDto.UserDetails.WhyIVolunteer;
                if (userProfileDto.UserDetails.CountryId.HasValue)
                    userDetail.CountryId = userProfileDto.UserDetails.CountryId.Value;
                if (userProfileDto.UserDetails.CityId.HasValue)
                    userDetail.CityId = userProfileDto.UserDetails.CityId.Value;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.Availability))
                    userDetail.Availability = userProfileDto.UserDetails.Availability;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.LinkdInUrl))
                    userDetail.LinkdInUrl = userProfileDto.UserDetails.LinkdInUrl;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.MySkills))
                    userDetail.MySkills = userProfileDto.UserDetails.MySkills;
                if (!string.IsNullOrEmpty(userProfileDto.UserDetails.UserImage))
                    userDetail.UserImage = userProfileDto.UserDetails.UserImage;
                if (userProfileDto.UserDetails.Status.HasValue)
                    userDetail.Status = userProfileDto.UserDetails.Status.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound(new { Message = "User not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Profile updated successfully" });
        }


        private bool UserExists(int userId)
        {
            return _context.Users.Any(e => e.Id == userId);
        }

        // Other methods will be added here
    }
}

public class UserProfileDto
{
    public UserDto User { get; set; }
    public UserDetailsDto UserDetails { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? UserType { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}

public class UserDetailsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? EmployeeId { get; set; }
    public string? Manager { get; set; }
    public string? Title { get; set; }
    public string? Department { get; set; }
    public string? MyProfile { get; set; }
    public string? WhyIVolunteer { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public string? Availability { get; set; }
    public string? LinkdInUrl { get; set; }
    public string? MySkills { get; set; }
    public string? UserImage { get; set; }
    public bool? Status { get; set; }
}
